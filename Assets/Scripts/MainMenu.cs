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
    [SerializeField] private VideoClip clip;
    private AsyncOperation asyncLoad;
    private void Start()
    {
        if (videoPlayer.clip == null && clip != null)
        {
            videoPlayer.clip = clip;
        }
        
        videoPlayer.Prepare();
    }

    public void PlayGame()
    {
        Debug.Log("PlayGame clicked — starting coroutine");
        StartCoroutine(PlayVideoThenLoadScene());
    }

    IEnumerator PlayVideoThenLoadScene()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (videoPlayer.clip == null && clip != null)
            videoPlayer.clip = clip;

        if (videoPlayer.clip == null)
        {
            Debug.LogWarning("No video clip found. Skipping video.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            yield break;
        }

        rawImage.gameObject.SetActive(true);
        videoPlayer.Prepare();

        float timeout = 5f;
        float timer = 0f;

        while (!videoPlayer.isPrepared && timer < timeout)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (!videoPlayer.isPrepared)
        {
            Debug.LogWarning("Video failed to prepare — skipping to next scene.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            yield break;
        }

        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        foreach (AudioSource audioSource in BackGroundMusic.Instance.AudioSources)
        {
            audioSource.Stop();
        }

        float duration = (float)videoPlayer.length;
        Debug.Log("Video playing for " + duration + " seconds");

        asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(duration);

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