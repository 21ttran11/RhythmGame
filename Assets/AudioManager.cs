using UnityEngine;
using FMODUnity;

public class BackgroundMusic : MonoBehaviour
{
    private FMOD.Studio.EventInstance musicInstance;

    void Start()
    {
        // Replace this path with your event path from FMOD Studio
        musicInstance = RuntimeManager.CreateInstance("event:/Music/Background");

        // Start playing the background music
        musicInstance.start();
    }

    void OnDestroy()
    {
        // Make sure to stop the music when this object is destroyed
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
}
