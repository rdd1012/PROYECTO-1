using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public static InventoryManager Instance;


    [SerializeField] private Color selectedSlotColor = Color.grey;
    [SerializeField] private GameObject[] itemSlots;
    [SerializeField] private Image[] slotImages;
    [SerializeField] private Vector2 tooltipOffset = new Vector2(30, -30);
    [SerializeField] private float tooltipDelay = 0.1f;
    [SerializeField] private AudioClip sonidoSeleccionar;
    [SerializeField] private AudioClip sonidoDeseleccionar;
    [SerializeField] private AudioClip sonidoDarItem;
    [SerializeField] private AudioClip sonidoCarta;


    [SerializeField] private TMP_Text cartaTexto;
    [SerializeField] private Image cartaImagen;
    [SerializeField]private AudioSource audioSourceSeleccionar;
    [SerializeField] private AudioSource audioSourceDarItem;
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TMP_Text tooltipText;


    private List<Item> items = new List<Item>();
    private Dictionary<int, int> itemUsos = new Dictionary<int, int>();
    private int selectedSlotIndex = -1;
    private bool isMouseOverSlot = false;
    private Coroutine showTooltipCoroutine;
    private Item selectedItem;


    public List<Item> Items => items;
    public Item SelectedItem => selectedItem;

    void Awake()
    {
        Instance = this;
        InitializeSlots();
        InitializeTooltip();
    }
    private void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (tooltipPanel.activeSelf) UpdateTooltipPosition();
    }
    public void AddItem(Item newItem)
    {
        if (items.Count >= itemSlots.Length) return;

        Item copiedItem = CreateItemCopy(newItem);
        items.Add(copiedItem);
        itemUsos[copiedItem.itemID] = copiedItem.usos;
        audioSourceDarItem.Play();
        UpdateUI();
    }

    public void RemoveItem(int itemID)
    {
        Item itemToRemove = items.Find(i => i.itemID == itemID);
        if (itemToRemove == null) return;

        items.Remove(itemToRemove);
        itemUsos.Remove(itemID);
        DeseleccionarItems();
        UpdateUI();
    }

    public void DecrementarUsos(int itemID)
    {
        if (!itemUsos.ContainsKey(itemID)) return;

        itemUsos[itemID]--;
        if (itemUsos[itemID] <= 0) RemoveItem(itemID);
    }

    public bool HasItem(int itemID) => items.Exists(item => item.itemID == itemID);
    public int ObtenerUsos(int itemID) => itemUsos.ContainsKey(itemID) ? itemUsos[itemID] : 0;
    private void UpdateUI()
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

    public void SelectItem(int slotIndex)
    {
        if (selectedSlotIndex == slotIndex)
        {
            DeseleccionarItems();
            return;
        }

        selectedSlotIndex = slotIndex;
        selectedItem = slotIndex < items.Count ? items[slotIndex] : null;

        if (selectedItem != null)
        {
            if (!selectedItem.isReadable)
                PlaySound(sonidoSeleccionar);
            else 
            {
                PlaySound(sonidoCarta);
                AbrirCarta(selectedItem.carta);
            }
            if (selectedItem.givesItem)
            {
                List<Item> itemsqueañadir = new List<Item>(selectedItem.itemsQueDa.Count);
                foreach (Item _item in selectedItem.itemsQueDa) 
                {
                    itemsqueañadir.Add(_item);
                }
                RemoveItem(selectedItem.itemID);
                foreach (Item _item in itemsqueañadir) 
                { AddItem(_item); }


            }
            UpdateSlotHighlights();
        }
        else
        {
            DeseleccionarItems();
        }
    }
    void AbrirCarta(string carta)
    {
        cartaTexto.text = carta;
        cartaImagen.transform.SetAsFirstSibling();
        cartaImagen.gameObject.SetActive(true);
    }
    void CerrarCarta()
    {
        cartaImagen.gameObject.SetActive(false);
    }
    public void DeseleccionarItems()
    {
        if (selectedSlotIndex == -1) return;

        PlaySound(sonidoDeseleccionar);
        if (selectedItem != null && selectedItem.isReadable)
        {
            CerrarCarta();
        }
        selectedSlotIndex = -1;
        selectedItem = null;
        UpdateSlotHighlights();
    }

    private void UpdateSlotHighlights()
    {
        for (int i = 0; i < slotImages.Length; i++)
            slotImages[i].color = (i == selectedSlotIndex) ? selectedSlotColor : Color.white;
    }
    private void InitializeTooltip()
    {
        if (tooltipPanel == null) return;

        tooltipPanel.SetActive(false);
        tooltipPanel.GetComponent<Image>().raycastTarget = false;
        tooltipText.raycastTarget = false;
    }

    private void UpdateTooltipPosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<Canvas>().GetComponent<RectTransform>(),
            Input.mousePosition,
            GetComponent<Canvas>().worldCamera,
            out Vector2 localPoint
        );

        tooltipPanel.GetComponent<RectTransform>().localPosition = localPoint + tooltipOffset;
    }

    private IEnumerator ShowNombreInventarioDelayed(int slotIndex)
    {
        yield return new WaitForSeconds(tooltipDelay);
        if (isMouseOverSlot) ShowNombreInventario(slotIndex);
    }

    public void ShowNombreInventario(int slotIndex)
    {
        if (slotIndex >= items.Count || items[slotIndex] == null) return;

        tooltipPanel.transform.SetAsLastSibling();
        tooltipText.text = items[slotIndex].itemName;
        tooltipPanel.SetActive(true);
        UpdateTooltipPosition();
    }

    private void HideNombreInventario() => tooltipPanel.SetActive(false);

    private Item CreateItemCopy(Item original)
    {
        Item copy = ScriptableObject.CreateInstance<Item>();
        copy.itemID = original.itemID;
        copy.itemName = original.itemName;
        copy.icon = original.icon;
        copy.usos = original.usos;
        copy.isReadable = original.isReadable;
        copy.carta = original.carta;
        copy.givesItem = original.givesItem; 
        copy.itemsQueDa = new List<Item>(original.itemsQueDa); 
        return copy;
    }

    private void InitializeSlots()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            int index = i;
            Button slotButton = itemSlots[i].GetComponent<Button>();

            slotButton.onClick.RemoveAllListeners();
            slotButton.onClick.AddListener(() => SelectItem(index));

            SetupSlotTriggers(itemSlots[i], index);
        }
    }

    private void SetupSlotTriggers(GameObject slot, int index)
    {
        EventTrigger trigger = slot.GetComponent<EventTrigger>() ?? slot.AddComponent<EventTrigger>();
        trigger.triggers.Clear();

        AddTriggerEvent(trigger, EventTriggerType.PointerEnter, () => {
            isMouseOverSlot = true;
            showTooltipCoroutine = StartCoroutine(ShowNombreInventarioDelayed(index));
        });

        AddTriggerEvent(trigger, EventTriggerType.PointerExit, () => {
            isMouseOverSlot = false;
            if (showTooltipCoroutine != null) StopCoroutine(showTooltipCoroutine);
            HideNombreInventario();
        });
    }

    private void AddTriggerEvent(EventTrigger trigger, EventTriggerType type, System.Action action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((data) => action());
        trigger.triggers.Add(entry);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSourceSeleccionar == null || clip == null) return;
        audioSourceSeleccionar.clip = clip;
        audioSourceSeleccionar.Play();
    }
}