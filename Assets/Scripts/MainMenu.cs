using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour {
    [SerializeField]private VideoPlayer videoPlayer;
    [SerializeField] private RawImage rawImage;
    private AsyncOperation asyncLoad;
    private void Start()
    {
        videoPlayer.Prepare();
    }

    public void PlayGame()
    {
        PlayVideoAndLoadScene();
    }

    void PlayVideoAndLoadScene()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rawImage.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        videoPlayer.loopPointReached += OnVideoEnd;

        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }

        asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;

    }
    void OnVideoEnd(VideoPlayer vp)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        asyncLoad.allowSceneActivation = true;
    }


    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}