using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuertaNivel1 : MonoBehaviour, IInteractable {
    public void OnClickAction()
    {
        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.items)
            {
                if (_item.itemID == 2)
                {
                    InventoryManager.Instance.RemoveItem(2);
                    break;
                }
            }

        }
    }

}

