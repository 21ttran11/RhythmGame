using UnityEngine;
using FMODUnity; // To access FMOD functionality

public class GlobalVolumeControl : MonoBehaviour
{
    [SerializeField]
    [FMODUnity.ParamRef] private string vcaName = "vca:/MasterVolume"; // Path to the VCA

    private FMOD.Studio.VCA masterVCA;

    private void Start()
    {
        // Get the VCA instance by its name
        masterVCA = FMODUnity.RuntimeManager.GetVCA(vcaName);

        if (masterVCA.isValid())
        {
            Debug.Log($"Successfully loaded VCA: {vcaName}");
        }
        else
        {
            Debug.LogError($"Failed to load VCA: {vcaName}. Check the name and setup in FMOD Studio.");
        }
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        SetVolume(savedVolume);
    }

    /// <summary>
    /// Sets the VCA volume.
    /// </summary>
    /// <param name="volume">Volume value between 0.0f (mute) and 1.0f (full volume).</param>
    public void SetVolume(float volume)
    {
        if (masterVCA.isValid())
        {
            masterVCA.setVolume(Mathf.Clamp01(volume));
            PlayerPrefs.SetFloat("MasterVolume", volume); // Save volume
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("VCA is not valid. Cannot set volume.");
        }
    }
}
