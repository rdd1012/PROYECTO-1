using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotsCarrito : MonoBehaviour, IDropHandler {
    [SerializeField] GameObject correctObject;
    [SerializeField] CarritoPantalla carritoPantalla;
    public GameObject CorrectObject { get {return correctObject; } }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (!droppedObject) return;

        ObjetosCarrito droppedObjetocarrito = droppedObject.GetComponent<ObjetosCarrito>();
        if (!droppedObjetocarrito) return;

        Transform originalParent = droppedObjetocarrito.ParentToReturnTo;

        if (transform.childCount == 0)
        {
            
            droppedObjetocarrito.ParentToReturnTo = transform;
            droppedObject.transform.SetParent(transform);
            droppedObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            
            Transform existingChild = transform.GetChild(0);
            ObjetosCarrito existingObjet = existingChild.GetComponent<ObjetosCarrito>();

            
            existingObjet.ParentToReturnTo = originalParent;
            droppedObjetocarrito.ParentToReturnTo = transform;

            
            existingChild.SetParent(originalParent);
            existingChild.localPosition = Vector3.zero;

            
            droppedObject.transform.SetParent(transform);
            droppedObject.transform.localPosition = Vector3.zero;
        }
        CheckPuzzleSolved();
    }

    

    void SwapObjects(GameObject droppedObject, ObjetosCarrito droppedObjeto)
    {
        Transform existingObject = transform.GetChild(0);
        ObjetosCarrito existenteObjeto = existingObject.GetComponent<ObjetosCarrito>();

        existenteObjeto.ParentToReturnTo = droppedObjeto.ParentToReturnTo;
        droppedObjeto.ParentToReturnTo = transform;

        existingObject.SetParent(droppedObjeto.ParentToReturnTo);
        existingObject.localPosition = Vector3.zero;

        droppedObject.transform.SetParent(transform);
        droppedObject.transform.localPosition = Vector3.zero;
    }

    void CheckPuzzleSolved()
    {
        foreach (ObjetosCarrito obj in FindObjectsOfType<ObjetosCarrito>())
        {
            SlotsCarrito parentSlot = obj.ParentToReturnTo.GetComponent<SlotsCarrito>();
            if (parentSlot.correctObject != obj.gameObject) return;
        }
        if (!carritoPantalla.IsPuzzleComplete)
        {
            carritoPantalla.IsPuzzleComplete = true;
            carritoPantalla.CompletarPuzzle();
        }
    }
}
