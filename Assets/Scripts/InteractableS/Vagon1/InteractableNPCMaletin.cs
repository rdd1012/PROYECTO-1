using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class InteractableNPCMaletin : MonoBehaviour, IInteractable {
    private bool puzzleCompleto = false;
    private bool inventoryHasItem = false;
    [SerializeField] private Item itemToGive;
    [SerializeField] private Canvas puzzleCanvas;
    public bool GetPuzzleCompleto() { return puzzleCompleto; }
    public void SetPuzzleCompleto(bool _puzzleCompleto) { puzzleCompleto = _puzzleCompleto; }
    public void OnClickAction()
    {
        puzzleCanvas.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (puzzleCompleto)
        {
            GiveItem();
        }
    }
    private void GiveItem()
    {
        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.items)
            {
                if (_item.itemID == itemToGive.itemID)
                {
                    inventoryHasItem = true;
                    break;
                }
            }
            if (!inventoryHasItem)
            {
                InventoryManager.Instance.AddItem(itemToGive);
                inventoryHasItem = true;
            }

        }
    }
}

