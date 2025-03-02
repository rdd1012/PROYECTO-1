using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField]private GameObject pauseMenuUI;
    [SerializeField] private GameObject optionsMenu;

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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void GoMenu ()
    {
        SceneManager.LoadScene(0);
    }
}
