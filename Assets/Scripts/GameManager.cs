using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    private float fadeDuration = 2.0f;
    private Image fadeImage;
    private Canvas fadeCanvas;
    [SerializeField] private Texture2D defaultCursorTexture;
    [SerializeField] private Texture2D interactableCursorTexture;
    AudioSource audioSource;
    [SerializeField] AudioClip sonidoCerrarPuerta;
    [SerializeField] private int targetFrameRate = 60;

    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeCursor();
            InitializeFrameRate();
            CreateFadeSystem();
            StartCoroutine(FadeIn());
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = sonidoCerrarPuerta;
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
    private void CreateFadeSystem()
    {
        
        fadeCanvas = new GameObject("FadeCanvas").AddComponent<Canvas>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        fadeCanvas.sortingOrder = 1000;
        DontDestroyOnLoad(fadeCanvas.gameObject);

        
        GameObject image = new GameObject("FadeImage");
        image.transform.SetParent(fadeCanvas.transform);

        fadeImage = image.AddComponent<Image>();
        fadeImage.color = Color.clear;

        
        RectTransform rt = image.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn());

    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void InitializeCursor()
    {
        Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.Auto);
    }

    private void InitializeFrameRate()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }
   
    void Update()
    {
        HandleClickInteraction();
    }
    

    private void HandleClickInteraction()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (IsPointerOverUI()) return;

        ProcessWorldInteraction();
    }
    
    private void ProcessWorldInteraction()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

        InteractableData topInteractable = GetTopPriorityInteractable(hits);
        if (topInteractable != null)
        {
            PlayerController.Instance.GoToItem(topInteractable);
        }
    }

    private InteractableData GetTopPriorityInteractable(RaycastHit2D[] hits)
    {
        InteractableData topItem = null;
        int highestPriority = int.MinValue;

        foreach (var hit in hits)
        {
            InteractableData data = hit.collider.GetComponent<InteractableData>();
            if (data == null) continue;

            SpriteRenderer renderer = hit.collider.GetComponentInChildren<SpriteRenderer>();
            if (renderer == null) continue;

            int currentPriority = CalculateRenderPriority(renderer);

            if (currentPriority > highestPriority)
            {
                highestPriority = currentPriority;
                topItem = data;
            }
        }

        return topItem;
    }
    private IEnumerator FadeOutAndLoad()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;

        yield return StartCoroutine(FadeOut());

        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    private IEnumerator FadeOut()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.color = Color.black;
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;
        fadeImage.color = Color.black;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1 - Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImage.color = Color.clear;
    }

    private int CalculateRenderPriority(SpriteRenderer renderer)
    {
        int layerPriority = SortingLayer.GetLayerValueFromID(renderer.sortingLayerID);
        int orderPriority = renderer.sortingOrder;
        return (layerPriority * 1000) + orderPriority;
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    
    public void SetCursorDefault()
    {
        Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void SetCursorInteractable()
    {
        Cursor.SetCursor(interactableCursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void PasarDeNivel()
    {
        StartCoroutine(FadeOutAndLoad());
    }

}