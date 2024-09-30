using UnityEngine;
using FMODUnity;
using System;
using FMOD.Studio;

public class BackgroundMusic : MonoBehaviour
{
    private FMOD.Studio.EventInstance musicInstance;
    private int timelinePosition;
    private FMOD.Studio.EVENT_CALLBACK timelineCallback;

    public GameObject beatSpawnerObj;
    private BeatSpawner beatSpawner;

    void Start()
    {
        beatSpawner = beatSpawnerObj.GetComponent<BeatSpawner>();

        // Replace this path with your event path from FMOD Studio
        musicInstance = RuntimeManager.CreateInstance("event:/Background");

        //Set up the timeline callback
        timelineCallback = new FMOD.Studio.EVENT_CALLBACK(TimelineMarkerCallback);
        musicInstance.setCallback(timelineCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        // Start playing the background music
        musicInstance.start();
    }

    private FMOD.RESULT TimelineMarkerCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        EventInstance instance = new EventInstance(instancePtr);

        if (type == FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER)
        {
            var marker = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)System.Runtime.InteropServices.Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
            Debug.Log("Marker Reached");

            if (beatSpawner != null)
            {
                beatSpawner.Spawn();
            }
            else
            {
                Debug.LogError("BeatSpawner not set!");
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
