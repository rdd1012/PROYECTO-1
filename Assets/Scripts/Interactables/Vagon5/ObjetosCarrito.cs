using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjetosCarrito : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private Transform parentToReturnTo;
    public Transform ParentToReturnTo
    {
        get { return parentToReturnTo; }
        set { parentToReturnTo = value; }
    }
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas parentCanvas; 

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>(); 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        transform.SetParent(parentCanvas.transform, true);
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling(); 
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.GetComponent<RectTransform>(),
            eventData.position,
            parentCanvas.worldCamera,
            out Vector2 localPoint
        );
        rectTransform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (transform.parent == parentCanvas.transform)
        {
            transform.SetParent(parentToReturnTo);
            rectTransform.localPosition = Vector3.zero;
        }
    }
}