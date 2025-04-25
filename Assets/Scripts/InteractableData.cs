using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableData : MonoBehaviour
{
    [SerializeField] private Transform goToPoint;
    public Transform GoToPoint => goToPoint;
    public int requiredItemID = -1; // -1 = no requiere item
    public bool requireSelectedItem = true;
    public bool CheckItemRequirement()
    {
        if (requiredItemID == -1) return true; 

        if (requireSelectedItem)
        {
            return InventoryManager.Instance.SelectedItem?.itemID == requiredItemID;
        }
        else
        {
            return InventoryManager.Instance.Items.Exists(i => i.itemID == requiredItemID);

        }
    }
}
