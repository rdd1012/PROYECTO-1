using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    // Nombres exactos de los parámetros expuestos en el AudioMixer
    private const string MASTER_VOL = "MasterVolume";
    private const string MUSIC_VOL = "MusicVolume";
    private const string SFX_VOL = "SFXVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolumes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadVolumes()
    {
        SetVolume(MASTER_VOL, PlayerPrefs.GetFloat(MASTER_VOL, 1f));
        SetVolume(MUSIC_VOL, PlayerPrefs.GetFloat(MUSIC_VOL, 1f));
        SetVolume(SFX_VOL, PlayerPrefs.GetFloat(SFX_VOL, 1f));
    }

    public void SetMasterVolume(float volume) => SetVolume(MASTER_VOL, volume);
    public void SetMusicVolume(float volume) => SetVolume(MUSIC_VOL, volume);
    public void SetSFXVolume(float volume) => SetVolume(SFX_VOL, volume);

    private void SetVolume(string parameter, float volume)
    {
        // Conversión correcta a decibelios
        float dB = volume > 0.01f ? Mathf.Log10(volume) * 20f : -80f;
        audioMixer.SetFloat(parameter, dB);
        PlayerPrefs.SetFloat(parameter, volume);
    }

    // Para obtener valores en escala lineal (0-1)
    public float GetLinearVolume(string parameter)
    {
        audioMixer.GetFloat(parameter, out float dB);
        return dB > -80f ? Mathf.Pow(10, dB / 20f) : 0f;
    }
}