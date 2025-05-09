using FMODUnity;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScriptUsageTimeline : MonoBehaviour
{
    class TimelineInfo // information that shows in fmod's ui
    {
        public int CurrentMusicBar = 0;
        public FMOD.StringWrapper LastMarker = new FMOD.StringWrapper();
    }

    TimelineInfo timelineInfo;
    GCHandle timelineHandle;

    public FMODUnity.EventReference EventName;

    public GameObject beatSpawnerObj;
    private BeatSpawner beatSpawner;

    public GameObject win;
    public Light2D globalLight;

    FMOD.Studio.EVENT_CALLBACK beatCallback;
    FMOD.Studio.EventInstance musicInstance;

    public GameObject cutscene;

#if UNITY_EDITOR
    void Reset()
    {
        EventName = FMODUnity.EventReference.Find("event:/BeatMap");
    }
#endif

    void Start()
    {
        timelineInfo = new TimelineInfo();

        beatSpawner = beatSpawnerObj.GetComponent<BeatSpawner>();
        

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(EventName);

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo);
        // Pass the object through the userdata of the instance
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        musicInstance.start();
    }

    void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
    }

    void OnGUI()
    {
        GUILayout.Box(String.Format("Current Bar = {0}, Last Marker = {1}", timelineInfo.CurrentMusicBar, (string)timelineInfo.LastMarker));
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    private FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr); // creates an eventinstance from the instanceptr which is passed to the callback by fmod when the event is triggered

        // Retrieve the user data
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr); // allows the function to modify the timelineinfoptr variable which stores the pointer to the timelineinfo 
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result); // error check !
        }
        else if (timelineInfoPtr != IntPtr.Zero) // if data exists 
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr); // the timeline pointer is changed to GChandle (a special handle in .NET used to "pin" and object in memory so the garbage collector does not move it
            // here, we unpin it to get access to get access to the actual timelineinfo object

            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;  // gives access to the timeline info instance for bar and marker data 

            switch (type) // checks the type of even that triggered the callback 
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT: // triggered when beat is played
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES)); // parameter ptr contains information about the beat event 
                        timelineInfo.CurrentMusicBar = parameter.bar; // bar tells you which bar of music is currently being played 
                        break;
                    }
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER: // triggered when marker is played 
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES)); // contains information about the pointer 
                        timelineInfo.LastMarker = parameter.name;
                        MarkerAction(parameter.name);
                        break;
                    }
                case FMOD.Studio.EVENT_CALLBACK_TYPE.DESTROYED: 
                    {
                        // Now the event has been destroyed, unpin the timeline memory so it can be garbage collected
                        timelineHandle.Free();
                        break;
                    }
            }
        }
        return FMOD.RESULT.OK;
    }

    void MarkerAction(string markerName)
    {
        switch(markerName)
        {
            case "SpawnBeat": //if the marker name is spawnbeat
                Debug.Log("spawn beat");
                beatSpawner.Spawn(); //beat spawner 
                break;
            case "Spawn": //if the marker name is spawnbeat
                Debug.Log("spawn beat");
                beatSpawner.Spawn(); //beat spawner 
                break;
            case "Cutscene": //if marker is cutscene
                Debug.Log("Cut scene trigger"); //trigger the cutscene
                cutscene.SetActive(true);
                break;
            case "Dark":
                globalLight.intensity = 0.03f;
                break;
            case "unDark":
                globalLight.intensity = 1f;
                break;
            case "Flash":
                beatSpawner.Pulse();
                Debug.Log("Pulse!");
                break;
            case "Stop":
                musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                musicInstance.release();
                break;
            case "Win":
                win.SetActive(true);
                break;
        }
    }

    public void PauseMusic(bool pause)
    {
        musicInstance.setPaused(pause);
    }
}