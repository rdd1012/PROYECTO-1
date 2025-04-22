using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuertaNivel1 : MonoBehaviour, IInteractable {
    bool teniaObjeto = false;
    public void OnClickAction()
    {
        QuitarItem(2);
        if (teniaObjeto) { }
    }
    private void QuitarItem(int itemID)
    {

        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.items)
            {
                if (_item.itemID == itemID)
                {
                    teniaObjeto = true;
                    InventoryManager.Instance.RemoveItem(itemID);
                    break;
                }
            }
        }
    }

}

