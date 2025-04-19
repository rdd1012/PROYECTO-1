using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    [Header("Referencias UI")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI masterPercent;
    [SerializeField] private TextMeshProUGUI musicPercent;
    [SerializeField] private TextMeshProUGUI sfxPercent;

    private void Start()
    {
        InitializeSliders();
        UpdateAllPercentages();
    }

    private void InitializeSliders()
    {
        // Configurar valores iniciales desde PlayerPrefs
        masterSlider.value = AudioManager.Instance.GetLinearVolume("MasterVolume");
        musicSlider.value = AudioManager.Instance.GetLinearVolume("MusicVolume");
        sfxSlider.value = AudioManager.Instance.GetLinearVolume("SFXVolume");

        // Configurar listeners
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
}