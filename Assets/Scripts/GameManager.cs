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
    [SerializeField] Item cartaInicial;

    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeCursor();
            CreateFadeSystem();
            StartCoroutine(FadeIn());
            audioSource = GetComponent<AudioSource>();
            if (SceneManager.GetActiveScene().buildIndex != 0)
                audioSource.clip = sonidoCerrarPuerta;
            audioSource.Play();
            if (SceneManager.GetActiveScene().buildIndex==1)
                InventoryManager.Instance.AddItem(cartaInicial);
            if (PlayerController.Instance != null)
            {
                if (PlayerController.Instance.ControlsEnabled)
                    PlayerController.Instance.TogglePlayerControl(true);
            }
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        HandleHoveringInteraction();
    }

    private void HandleHoveringInteraction()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {
            DetectInteractable(hit.collider.gameObject);
        }
        else
        {
            SetCursorDefault();
        }
    }
    public void DetectInteractable(GameObject hitObject)
    {
        IInteractable iInteractable = hitObject.GetComponent<IInteractable>();

        if (iInteractable != null)
        {
            // Debug.Log("Interactable found: " + hitObject.gameObject.name);
            SetCursorInteractable();
        }
        else
        {
            SetCursorDefault();
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

    private IEnumerator FadeOutAndLoad()
    {
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;

       
        yield return StartCoroutine(FadeOut());

        
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        
        asyncLoad.allowSceneActivation = true;

        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        
        StopAllCoroutines();
        fadeImage.color = Color.clear;
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
       
        fadeImage.color = Color.black;

        float elapsed = 0f;
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
        PlayerController.Instance.TogglePlayerControl(false);
        StartCoroutine(FadeOutAndLoad());
    }

}