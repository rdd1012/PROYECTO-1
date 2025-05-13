using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneLoader : MonoBehaviour {
    private VideoPlayer videoPlayer;
    private AsyncOperation asyncLoad;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd; 

        asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;

        videoPlayer.Play(); 
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        asyncLoad.allowSceneActivation = true;
    }
}