using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField]
    public Slider volumeSlider;
    public TMPro.TMP_Dropdown resolution_dropdown;
    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            PlayerPrefs.Save();
        } else {
            Load();
            AudioListener.volume = volumeSlider.value;
        }

        //for fullscreen toggle and resolution////////////////////////////////////////
        //quality/////
        int saved_quality = PlayerPrefs.GetInt("GraphicsQuality", QualitySettings.GetQualityLevel());
        set_quality(saved_quality);

        //fullscreen//////
        bool saved_fullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        set_fullscreen(saved_fullscreen);

        //resolutions////////
        resolutions = Screen.resolutions;
        resolution_dropdown.ClearOptions();
        //turn array of resolutions into formatetd strings
        List<string> options = new List<string>();
        //save
        int saved_resolutionIndex = PlayerPrefs.GetInt("Resolution", 0);
        set_resolution(saved_resolutionIndex);


        int current_res = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                current_res = i;
            }
        }
        resolution_dropdown.AddOptions(options);
        resolution_dropdown.value = saved_resolutionIndex;
        resolution_dropdown.RefreshShownValue();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.Save();
    }
    
    //for fullscreen toggle and resolution////////////////////////////////////////
    public void set_quality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
    }

    public void set_fullscreen (bool is_fullscreen)
    {
        Screen.fullScreen = is_fullscreen;
        PlayerPrefs.SetInt("Fullscreen", is_fullscreen ? 1 : 0);
    }

    public void set_resolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
    }
}
