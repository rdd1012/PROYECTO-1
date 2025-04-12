using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSetLimpieza : MonoBehaviour, IInteractable {
    [SerializeField] private Item item;
    public void OnClickAction()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}