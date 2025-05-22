using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    [SerializeField]private GameObject pauseMenuUI;
    [SerializeField] private GameObject optionsMenu;
    AudioSource audioSource;

    private void Start()
    {
        GameIsPaused=false;
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si el men� de opciones est� activo, no hagas nada
            if (optionsMenu.activeSelf)
            {
                return;
            }

            // Si el juego est� pausado y el men� de opciones no est� activo, reanuda el juego
            if (GameIsPaused)
            {
                Resume();
            }
            // Si el juego no est� pausado y el men� de opciones no est� activo, pausa el juego
            else
            {
                Pause();
            }
        }
    }
    private void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        foreach (AudioSource audioSource in BackGroundMusic.Instance.AudioSources) 
        {
            audioSource.Stop();
        }
        audioSource.Play();

    }
    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        foreach (AudioSource audioSource in BackGroundMusic.Instance.AudioSources)
        {
            audioSource.Play();
        }
        audioSource.Stop();

    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
