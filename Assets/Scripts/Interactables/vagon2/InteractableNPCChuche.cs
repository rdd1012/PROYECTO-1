using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPCChuche : NPCBase, IInteractable {
    private InteractableData interactableData;
    [SerializeField] private Item itemToGive;
    bool teniaObjeto = false;
    private bool inventoryHasItem = false;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    [SerializeField]Sprite normal;
    [SerializeField] Sprite pestañeo;
    [SerializeField] Sprite hablando;
    [SerializeField] Sprite feliz;
    [SerializeField] private NPCdialogoSO dialogos;
    YapBubble yapBubble;
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        yapBubble = GetComponentInChildren<YapBubble>();
        yapBubble.gameObject.SetActive(false);
        interactableData = GetComponent<InteractableData>();
        audioSource = GetComponent<AudioSource>();
        
    }
    public bool TieneItem() { return interactableData.CheckItemRequirement(); }
    public void OnClickAction()
    {
        if (TieneItem())
        {
            if (!teniaObjeto)
            {
                QuitarItem(interactableData.requiredItemID);
                StartCoroutine(Yap(dialogos.frases[1], hablando, feliz, spriteRenderer, yapBubble));
                StartCoroutine(Blink(pestañeo, spriteRenderer));
            }

            GiveItem();
        }
        if(!teniaObjeto) StartCoroutine(Yap(dialogos.frases[0], hablando, normal, spriteRenderer, yapBubble));
    }

    private void QuitarItem(int itemID)
    {
        if (InventoryManager.Instance != null && InventoryManager.Instance.HasItem(itemID))
        {
            teniaObjeto = true;
            InventoryManager.Instance.DecrementarUsos(itemID);
        }
    }
    public bool IsInteractable() { return true; }
    private void GiveItem()
    {
        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.Items)
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
                //audioSource.Play();

            }

        }
    }
}
