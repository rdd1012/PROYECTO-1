using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCaja : MonoBehaviour,IInteractable
{
    [SerializeField]GameObject puertaCarga;

    [SerializeField] private Item itemToGive;
    AudioSource audioSource;
    bool teniaObjeto = false;
    InteractableData interactableData;
    private void Start()
    {
        interactableData = GetComponent<InteractableData>();
        
    }
    public void OnClickAction()
    {
        if (interactableData.CheckItemRequirement())
        {
            if (!teniaObjeto)
                QuitarItem(interactableData.requiredItemID);


            ActivarPuerta();
            Destroy(this.gameObject);
        }
    }
    private void ActivarPuerta()
    {
        puertaCarga.AddComponent<InteractablePuertaCarga>();
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
