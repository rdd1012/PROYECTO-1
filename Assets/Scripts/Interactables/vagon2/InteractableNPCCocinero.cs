using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPCCocinero : MonoBehaviour, IInteractable {
    [SerializeField] private Item itemToGive;
    private bool inventoryHasItem = false;
    AudioSource audioSource;
    bool teniaObjeto = false;
    InteractableData interactableData;
    SpriteRenderer spriteRender;
    [SerializeField] Sprite spriteEncendido;
    private void Start()
    {
        interactableData = GetComponent<InteractableData>();
        spriteRender = GetComponentInChildren<SpriteRenderer>();
    }
    public bool TieneItem() { return true; }
    public void OnClickAction()
    {
        if (interactableData.CheckItemRequirement())
        {
            if (!teniaObjeto)
                QuitarItem(interactableData.requiredItemID);

            spriteRender.sprite = spriteEncendido;
            GiveItem();
        }
    }
    public bool IsInteractable() { return true; }
    private void GiveItem()
    {
        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.Items)
            {
                if (_item.itemID == itemToGive.itemID)
                {
                    inventoryHasItem = true;
                    break;
                }
            }
            if (!inventoryHasItem)
            {
                InventoryManager.Instance.AddItem(itemToGive);
                inventoryHasItem = true;
                // audioSource.Play();
            }

        }
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
