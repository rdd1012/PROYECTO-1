using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPCLibro : MonoBehaviour, IInteractable {
    [SerializeField] private Item item;
    bool inventoryHasItem = false;
    bool teniaObjeto=false;
    public void OnClickAction()
    {
        QuitarItem(1);
        if (teniaObjeto) GiveItem();


    }
    private void GiveItem()
    {
        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.items)
            {
                if (_item.itemID == item.itemID)
                {
                    inventoryHasItem = true;
                    break;
                }
            }
            if (!inventoryHasItem)
            {
                InventoryManager.Instance.AddItem(item);
                inventoryHasItem = true;
            }

        }
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
