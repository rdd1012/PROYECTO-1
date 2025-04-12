using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();
    public GameObject[] itemSlots;

    void Awake()
    {
        Instance = this;
    }

    public void AddItem(Item newItem)
    {
        if (items.Count < itemSlots.Length)
        {
            items.Add(newItem);
            UpdateUI();
        }
    }

    public void RemoveItem(int itemID)
    {
        Item itemToRemove = items.Find(i => i.itemID == itemID);
        if (itemToRemove != null)
        {
            items.Remove(itemToRemove);
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            var slotImage = itemSlots[i].GetComponent<UnityEngine.UI.Image>();
            slotImage.sprite = i < items.Count ? items[i].icon : GameManager.Instance.emptySlots;
        }
    }
}