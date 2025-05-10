using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractablePuertaPomo : MonoBehaviour, IInteractable {
    bool teniaObjeto = false;
    private InteractableData interactableData;
    AudioSource audioSource;
    public bool IsInteractable() { return true; }
    private void Start()
    {
        interactableData = GetComponent<InteractableData>();
        audioSource = GetComponent<AudioSource>();
    }
    public bool TieneItem() { return interactableData.CheckItemRequirement(); }
    public void OnClickAction()
    {
        if (TieneItem())
        {
            if (!teniaObjeto)
                QuitarItem(interactableData.requiredItemID);
            return;
        }
        if (teniaObjeto) { GameManager.Instance.PasarDeNivel();
            audioSource.Play();}
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

