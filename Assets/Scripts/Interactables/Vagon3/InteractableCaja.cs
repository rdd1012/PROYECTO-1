using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCaja : MonoBehaviour,IInteractable
{
    [SerializeField]InteractablePuertaCarga puertaCarga;

    [SerializeField] private Item itemToGive;
    AudioSource audioSource;
    InteractableData interactableData;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        interactableData = GetComponent<InteractableData>();
        
    }
    public bool TieneItem() { return interactableData.CheckItemRequirement(); }
    public void OnClickAction()
    {
        if (TieneItem())
        {
            QuitarItem(interactableData.requiredItemID);
            puertaCarga.Unlock();
            audioSource.Play();
            //Destroy(gameObject);
        }
    }
    public bool IsInteractable() { return true; }
    private void QuitarItem(int itemID)
    {
        if (InventoryManager.Instance != null && InventoryManager.Instance.HasItem(itemID))
        {
            InventoryManager.Instance.DecrementarUsos(itemID);
        }
    }
}
