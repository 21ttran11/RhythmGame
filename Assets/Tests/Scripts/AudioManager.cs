using UnityEngine;
using FMODUnity;
using System;
using FMOD.Studio;

public class BackgroundMusic : MonoBehaviour
{
    private FMOD.Studio.EventInstance musicInstance;
    private int timelinePosition;
    private FMOD.Studio.EVENT_CALLBACK timelineCallback;

    public GameObject beatSpawnerObj; // gameobject that holds the gameobject in the scene with the beat spawner script
    private BeatSpawner beatSpawner; // variable of type beat spawner referencing the beat spawner script 

    public FMODUnity.EventReference EventName;

    void Start()
    {
        beatSpawner = beatSpawnerObj.GetComponent<BeatSpawner>(); // grab the beat spawner script component from the game object 

        musicInstance = RuntimeManager.CreateInstance(EventName); // path to the event in fmod 

        //Set up the timeline callback
        timelineCallback = new FMOD.Studio.EVENT_CALLBACK(TimelineMarkerCallback);
        musicInstance.setCallback(timelineCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER); // grabs the markers in the fmod files
        // Start playing the background music
        musicInstance.start(); 
    }

    private FMOD.RESULT TimelineMarkerCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        EventInstance instance = new EventInstance(instancePtr);

        if (type == FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER)
        {
            var marker = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)System.Runtime.InteropServices.Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));

            // Convert the name of the marker to a regular string
            string markerName = marker.name;
            Debug.Log("Marker Reached: " + markerName);

            if (markerName == "SpawnBeat")
            {
                if (beatSpawner != null)
                {
                    beatSpawner.Spawn();
                }
                else
                {
                    Debug.LogError("BeatSpawner not set!");
                }
            }
        }

        return FMOD.RESULT.OK;
    }

    void OnDestroy()
    {
        // Make sure to stop the music when this object is destroyed
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
}
