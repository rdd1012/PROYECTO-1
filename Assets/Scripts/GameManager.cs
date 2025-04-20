using System.Collections;
using UnityEngine;

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
    [Header("Inventario")]
    public GameObject canvasInventario;
    public GameObject[] itemsSlots,itemImages;
    public Sprite emptySlots;
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