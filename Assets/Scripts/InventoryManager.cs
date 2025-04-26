using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    [SerializeField] private Color selectedSlotColor = Color.grey;
    [SerializeField] private List<Item> items = new List<Item>();
    public List<Item> Items { get { return items; } set { items = Items; } }
    [SerializeField] private GameObject[] itemSlots;
    [SerializeField] private Image[] slotImages;
    [SerializeField] private Item selectedItem;
    public Item SelectedItem { get { return selectedItem; } set { selectedItem = SelectedItem; } }

    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TMP_Text tooltipText;
    [SerializeField] private Vector2 tooltipOffset = new Vector2(30, -30);
    [SerializeField] private float tooltipDelay = 0.1f;

    public static InventoryManager Instance;
    private int selectedSlotIndex = -1;
    private bool isMouseOverSlot = false;
    private Coroutine showTooltipCoroutine;

    void Awake()
    {
        Instance = this;
        InitializeSlots();
        InitializeTooltip();
    }

    void Update()
    {
        if (tooltipPanel.activeSelf)
        {
            UpdateTooltipPosition();
        }
    }

    void InitializeTooltip()
    {
        if (tooltipPanel != null)
        {
            tooltipPanel.SetActive(false);
            
            var tooltipImage = tooltipPanel.GetComponent<Image>();
            if (tooltipImage != null) tooltipImage.raycastTarget = false;
            tooltipText.raycastTarget = false;
        }
    }

    void UpdateTooltipPosition()
    {
        RectTransform tooltipRect = tooltipPanel.GetComponent<RectTransform>();
        Canvas canvas = GetComponent<Canvas>();

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            Input.mousePosition,
            canvas.worldCamera,
            out localPoint
        );

        tooltipRect.localPosition = localPoint + tooltipOffset;
    }

    public void ShowNombreInventario(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= items.Count || items[slotIndex] == null) return;

        tooltipPanel.transform.SetAsLastSibling();
        tooltipText.text = items[slotIndex].itemName;
        tooltipPanel.SetActive(true);
        UpdateTooltipPosition();
    }

    private void HideNombreInventario()
    {
        tooltipPanel.SetActive(false);
    }

    private void InitializeSlots()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            int index = i;

            
            itemSlots[i].GetComponent<Button>().onClick.AddListener(() => SelectItem(index));

            
            EventTrigger trigger = itemSlots[i].GetComponent<EventTrigger>() ?? itemSlots[i].AddComponent<EventTrigger>();
            trigger.triggers.Clear();

            
            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((data) => {
                isMouseOverSlot = true;
                showTooltipCoroutine = StartCoroutine(ShowNombreInventarioDelayed(index));
            });
            trigger.triggers.Add(entryEnter);

            // Evento PointerExit
            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((data) => {
                isMouseOverSlot = false;
                if (showTooltipCoroutine != null) StopCoroutine(showTooltipCoroutine);
                HideNombreInventario();
            });
            trigger.triggers.Add(entryExit);
        }
    }

    private IEnumerator ShowNombreInventarioDelayed(int slotIndex)
    {
        yield return new WaitForSeconds(tooltipDelay);
        if (isMouseOverSlot) ShowNombreInventario(slotIndex);
    }

    
    public void SelectItem(int slotIndex)
    {
        selectedSlotIndex = slotIndex;
        selectedItem = items.Count > slotIndex ? items[slotIndex] : null;
        if (selectedItem != null) UpdateSlotHighlights();
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
        selectedSlotIndex--;
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
            var slotImage = itemSlots[i].GetComponent<Image>();
            if (slotImage == null) continue;

            bool hasItem = i < items.Count && items[i].icon != null;
            slotImage.sprite = hasItem ? items[i].icon : null;
            slotImage.enabled = hasItem;
        }
    }
}