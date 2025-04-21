using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPCLibro : MonoBehaviour, IInteractable {
    [SerializeField] private Item item;
    public void OnClickAction()
    {
        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.items)
            {
                if (_item.itemID == 1)
                {
                    InventoryManager.Instance.RemoveItem(1);
                    InventoryManager.Instance.AddItem(item);
                    break;
                }
            }
        }
    }

    
}
