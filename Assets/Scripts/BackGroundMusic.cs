using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackGroundMusic : MonoBehaviour
{
    [SerializeField]float volumen=1f;
    AudioSource audioSource;
    [SerializeField] List<AudioClip> listaMusicaMenu = new();
    [SerializeField] List<AudioClip> listaMusicaNivel1 = new();
    private List<AudioSource> audioSources = new();
    public static BackGroundMusic Instance { get; private set; }
    int escena;
    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else 
        {
            Destroy(gameObject);
        }

            
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        escena = scene.buildIndex;
        CleanupAudioSources();
        SetupMusic();
    }
    void ConfigureAudioSource(AudioSource source)
    {
        source.loop = true;
        source.playOnAwake = false;
        source.spatialBlend = 0;
        source.volume = volumen;
    }
    void CreateAudioSources(List<AudioClip> clips)
    {
        foreach (AudioClip clip in clips)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = clip;
            ConfigureAudioSource(newSource);
            audioSources.Add(newSource);
        }
    }
    void CleanupAudioSources()
    {
        foreach (AudioSource source in audioSources)
        {
            Destroy(source);
        }
        audioSources.Clear();
    }
    public void SetTrackVolume(int trackIndex, float volume)
    {
        if (trackIndex >= 0 && trackIndex < audioSources.Count)
        {
            audioSources[trackIndex].volume = Mathf.Clamp01(volume);
        }
    }

    public void SetTrackActive(int trackIndex, bool active)
    {
        if (trackIndex >= 0 && trackIndex < audioSources.Count)
        {
            if (active) audioSources[trackIndex].Play();
            else audioSources[trackIndex].Stop();
        }
    }
    void SetupMusic()
    {
        switch (escena)
        {
            case 0:
                CreateAudioSources(listaMusicaMenu);
                PlayAllTracks();
                break;
            case 1:
                CreateAudioSources(listaMusicaNivel1);
                PlayAllTracks();
                break;
        }
    }
    void PlayAllTracks()
    {
        foreach (AudioSource source in audioSources)
        {
            source.Play();
        }
    }
}
