using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCarrito : MonoBehaviour,IInteractable
{
    [SerializeField] CarritoPantalla carritoPantalla;
    [SerializeField] private Item itemToGive;
    private bool inventoryHasItem = false;
    public void OnClickAction()
    {
        carritoPantalla.gameObject.SetActive(true);
    }

    public bool TieneItem() { return true; }
    public bool IsInteractable() { return true; }
    public void GiveItem()
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
}
