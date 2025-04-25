using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    private int target = 60;
    [SerializeField] private Texture2D interactiveCursorTexture;

    
    public void SetCursorInteractive()
    {
        Cursor.SetCursor(interactiveCursorTexture, default, default);
    }
    public void SetCursorDefault()
    {
        Cursor.SetCursor(default, default, default);
    }
    public void PasarDeNivel()
    {
       int EscenaActual= SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(EscenaActual++);
    }
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }

}