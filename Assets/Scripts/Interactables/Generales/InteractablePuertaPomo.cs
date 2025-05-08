using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuertaPomo : MonoBehaviour, IInteractable {
    bool teniaObjeto = false;
    private InteractableData interactableData;

    private void Start()
    {
        interactableData = GetComponent<InteractableData>();
    }
    public void OnClickAction()
    {
        QuitarItem(interactableData.requiredItemID);
        if (teniaObjeto) { GameManager.Instance.PasarDeNivel(); }
    }
    private void QuitarItem(int itemID)
    {
        if (InventoryManager.Instance != null && InventoryManager.Instance.HasItem(itemID))
        {
            teniaObjeto = true;
            InventoryManager.Instance.DecrementarUsos(itemID);
        }
    }

}

