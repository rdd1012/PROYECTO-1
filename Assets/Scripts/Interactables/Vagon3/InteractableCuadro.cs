using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCuadro : MonoBehaviour, IInteractable {
    [SerializeField] private Item itemToGive;
    private bool inventoryHasItem = false;
    AudioSource audioSource;
    bool teniaObjeto = false;
    InteractableData interactableData;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteSinCubo;
    [SerializeField] Sprite spriteConCuboyPalanca;
    [SerializeField] Sprite spriteSinPalanca;
    private void Start()
    {
        interactableData = GetComponent<InteractableData>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = spriteSinCubo;
    }
    public void OnClickAction()
    {
        if (interactableData.CheckItemRequirement())
        {
            if (!teniaObjeto)
                QuitarItem(interactableData.requiredItemID);
            spriteRenderer.sprite = spriteConCuboyPalanca;
            return;
            
        }
        if (teniaObjeto) 
        {
            GiveItem();
            spriteRenderer.sprite = spriteSinPalanca;
        }
    }
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
