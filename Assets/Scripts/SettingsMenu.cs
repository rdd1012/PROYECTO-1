using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;

public class SettingsMenu : MonoBehaviour {
    [Header("Referencias UI")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI masterPercent;
    [SerializeField] private TextMeshProUGUI musicPercent;
    [SerializeField] private TextMeshProUGUI sfxPercent;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private void Start()
    {
        InitializeSliders();
        UpdateAllPercentages();
        InitializeDisplaySettings();
    }

    private void InitializeSliders()
    {
        masterSlider.value = AudioManager.Instance.GetLinearVolume("MasterVolume");
        musicSlider.value = AudioManager.Instance.GetLinearVolume("MusicVolume");
        sfxSlider.value = AudioManager.Instance.GetLinearVolume("SFXVolume");

        masterSlider.onValueChanged.AddListener(v => {
            AudioManager.Instance.SetMasterVolume(v);
            UpdatePercentages();
        });

        musicSlider.onValueChanged.AddListener(v => {
            AudioManager.Instance.SetMusicVolume(v);
            UpdatePercentages();
        });

        sfxSlider.onValueChanged.AddListener(v => {
            AudioManager.Instance.SetSFXVolume(v);
            UpdatePercentages();
        });
    }

    private void UpdateAllPercentages()
    {
        masterPercent.text = $"{Mathf.RoundToInt(masterSlider.value * 100)}%";
        musicPercent.text = $"{Mathf.RoundToInt(musicSlider.value * 100)}%";
        sfxPercent.text = $"{Mathf.RoundToInt(sfxSlider.value * 100)}%";
    }

    private void UpdatePercentages()
    {
        masterPercent.text = $"{Mathf.RoundToInt(masterSlider.value * 100)}%";
        musicPercent.text = $"{Mathf.RoundToInt(AudioManager.Instance.GetLinearVolume("MusicVolume") * 100)}%";
        sfxPercent.text = $"{Mathf.RoundToInt(AudioManager.Instance.GetLinearVolume("SFXVolume") * 100)}%";
    }

    private void InitializeDisplaySettings()
    {
        
        filteredResolutions = Screen.resolutions
            .GroupBy(r => $"{r.width}x{r.height}")
            .Select(g => g.OrderByDescending(r => r.refreshRateRatio.value).Last())
            .OrderByDescending(r => r.width)
            .ThenByDescending(r => r.height)
            .ToList();

       
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        
        int currentWidth = Screen.width;
        int currentHeight = Screen.height;

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            var res = filteredResolutions[i];
            options.Add($"{res.width}x{res.height} ({res.refreshRateRatio.value:0}Hz)");

            
            if (res.width == currentWidth && res.height == currentHeight)
            {
                currentResolutionIndex = i;
            }
        }

        
        if (currentResolutionIndex == 0 && filteredResolutions.Count > 0)
        {
            Screen.SetResolution(
                filteredResolutions[0].width,
                filteredResolutions[0].height,
                Screen.fullScreenMode,
                filteredResolutions[0].refreshRateRatio
            );
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        
        fullscreenToggle.isOn = Screen.fullScreen;

        
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    private void SetResolution(int resolutionIndex)
    {
        Resolution selected = filteredResolutions[resolutionIndex];
        Screen.SetResolution(
            selected.width,
            selected.height,
            Screen.fullScreenMode,
            selected.refreshRateRatio
        );
    }

    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        
        Screen.fullScreenMode = isFullscreen ?
            FullScreenMode.ExclusiveFullScreen :
            FullScreenMode.Windowed;
    }

}