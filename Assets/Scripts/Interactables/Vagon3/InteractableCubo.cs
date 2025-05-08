using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCubo : MonoBehaviour,IInteractable
{
    [SerializeField] private Item itemToGive;
    private bool inventoryHasItem = false;
    AudioSource audioSource;
    public bool IsInteractable() { return true; }
    public void OnClickAction()
    {
        GiveItem();
        Destroy(this.gameObject);
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
   
}
