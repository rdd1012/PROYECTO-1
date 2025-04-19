using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InteractableNPCMaletin : MonoBehaviour, IInteractable {
    private bool puzzleCompleto = false;
    private bool inventoryHasItem = false;
    [SerializeField] private Item item;
    public bool GetPuzzleCompleto() { return puzzleCompleto; }
    public void SetPuzzleCompleto(bool _puzzleCompleto) { puzzleCompleto = _puzzleCompleto; }
    public void OnClickAction()
    {
        if (puzzleCompleto) 
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
                if (!inventoryHasItem) InventoryManager.Instance.AddItem(item);

            }
        }
    }
}
