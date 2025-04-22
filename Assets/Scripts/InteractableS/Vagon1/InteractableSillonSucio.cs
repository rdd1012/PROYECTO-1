using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InteractableSillonSucio : MonoBehaviour, IInteractable {
    public void OnClickAction()
    {
        QuitarItem(0);       
    }
    private void QuitarItem(int itemID) {

        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.items)
            {
                if (_item.itemID == itemID)
                {
                    InventoryManager.Instance.RemoveItem(itemID);
                    break;
                }
            }
        }
    }
}
