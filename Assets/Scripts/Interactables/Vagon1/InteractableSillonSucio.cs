using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InteractableSillonSucio : MonoBehaviour, IInteractable {
    public void OnClickAction()
    {
        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.items)
            {
                if (_item.itemID == 0f)
                {
                    LimpiarSillon();
                }
            }
        }
    }
    void LimpiarSillon() { }
}
