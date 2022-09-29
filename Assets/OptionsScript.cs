using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class OptionsScript : MonoBehaviour
{
    public AudioMixer mainMixer;
    [SerializeField] public TMP_Dropdown resDropdown;
    public Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currResIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string o = resolutions[i].width + " × " + resolutions[i].height;
            options.Add(o);

            if(resolutions[i].width == Screen.currentResolution.width 
            && resolutions[i].height == Screen.currentResolution.height)
                currResIndex = i;
        }

        resDropdown.AddOptions(options);
        resDropdown.value = currResIndex;
        resDropdown.RefreshShownValue();
    }
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("MasterVolume", volume);
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    public void SetFullscreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }
    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void StartTime()
    {
        Time.timeScale = 1f;
    }
}
