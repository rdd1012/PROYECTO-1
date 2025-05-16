using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjetosCarrito : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private Transform parentToReturnTo;
    public Transform ParentToReturnTo { get { return parentToReturnTo; } set {  parentToReturnTo=value; } }
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        parentToReturnTo = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (transform.parent == transform.root)
        {
            transform.SetParent(parentToReturnTo);
            transform.localPosition = Vector3.zero;
        }
    }
}
