using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    
    [SerializeField] private Texture2D defaultCursorTexture;
  
    [SerializeField] private int targetFrameRate = 60;

    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeCursor();
            InitializeFrameRate();
        }
        else
        {
            Destroy(gameObject);
        }
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

    public void PasarDeNivel() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);}
}