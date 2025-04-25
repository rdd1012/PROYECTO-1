using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Color selectedSlotColor = Color.grey;
    public static InventoryManager Instance;
    [SerializeField] private List<Item> items = new List<Item>();
    public List<Item> Items { get { return items; } set { items = Items; } }
    [SerializeField]private GameObject[] itemSlots;
    [SerializeField] private Image[] slotImages;
    [SerializeField] private Item selectedItem;
    public Item SelectedItem { get { return selectedItem; } set { selectedItem = SelectedItem; } }
    private int selectedSlotIndex = -1;

    void Awake()
    {
        Instance = this;
        InitializeSlots();
    }
    private void InitializeSlots()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            int index = i;
            itemSlots[i].GetComponent<Button>().onClick.AddListener(() => SelectItem(index));
        }
    }
    public void SelectItem(int slotIndex)
    {
        
        selectedSlotIndex = slotIndex;
        selectedItem = items.Count > slotIndex ? items[slotIndex] : null;
        if (selectedItem!=null)UpdateSlotHighlights();
    }
    private void UpdateSlotHighlights()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            slotImages[i].color = (i == selectedSlotIndex) ? selectedSlotColor : Color.white;
        }
    }
    private void DeselectItem()
    {
        selectedSlotIndex --;
        selectedItem = null;
        UpdateSlotHighlights();
    }


    private void Start()
    {
        UpdateUI();
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
            DeselectItem();
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            var slotImage = itemSlots[i].GetComponent<UnityEngine.UI.Image>();

            if (slotImage != null)
            {
                if (i < items.Count && items[i].icon != null)
                {
                    slotImage.sprite = items[i].icon;
                    slotImage.enabled = true;
                }
                else
                {
                    // sprite para slot vacío
                    slotImage.sprite = null;
                    slotImage.enabled = false; 
                }
            }
            else
            {
                Debug.LogWarning($"Slot {i} no tiene componente Image");
            }
        }
    }
}