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
    private Canvas parentCanvas; // Canvas padre principal (ej: el del UI)

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>(); // Obtiene el Canvas padre
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Mueve el objeto al Canvas padre principal para evitar problemas de renderizado
        transform.SetParent(parentCanvas.transform, true);
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling(); // Asegura que esté encima de todo
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convierte la posición del cursor al espacio del Canvas padre
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