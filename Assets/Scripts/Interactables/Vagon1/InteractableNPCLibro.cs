using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPCLibro : MonoBehaviour, IInteractable {
    [SerializeField] private Item item;
    private bool inventoryHasItem = false;
    public void OnClickAction()
    {
        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.items)
            {
                if (_item.itemID == 1)
                {
                }
            }

        }
    }
    
}
