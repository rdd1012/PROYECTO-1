using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSeñoraExtravagante : NPCBase, IInteractable {
    private InteractableData interactableData;
    [SerializeField] private Item itemToGive;
    bool teniaObjeto = false;
    private bool inventoryHasItem = false;
    private bool haHablado = false;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Sprite normal;
    [SerializeField] Sprite pestañeobrillando;
    [SerializeField] Sprite hablando;
    [SerializeField] Sprite hablandobrillando;
    [SerializeField] Sprite brillando;
    [SerializeField] private NPCdialogoSO dialogos;
    YapBubble yapBubble;
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        normal = spriteRenderer.sprite;
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
                QuitarItem(interactableData.requiredItemID);

            spriteRenderer.sprite = brillando;
            StartCoroutine(Blink(pestañeobrillando, spriteRenderer));
            GiveItem();
        }
        if (teniaObjeto&& !haHablado) { 
            StartCoroutine(Yap(dialogos.frases[1], hablandobrillando, brillando, spriteRenderer, yapBubble));
            haHablado = true;
        }
        else if (!haHablado) StartCoroutine(Yap(dialogos.frases[0], hablando, normal, spriteRenderer, yapBubble));
    }
    public override IEnumerator Yap(string _text, Sprite _hablando, Sprite _normal, SpriteRenderer _spriteRenderer, YapBubble _yapbubble)
    {
        _spriteRenderer.sprite = _hablando;
        _yapbubble.gameObject.SetActive(true);
        _yapbubble.SetupText(_text);
        yield return new WaitForSeconds(3f);
        _yapbubble.gameObject.SetActive(false);
        if (teniaObjeto) _spriteRenderer.sprite = brillando;
        else _spriteRenderer.sprite = _normal;
    }

    private void QuitarItem(int itemID)
    {
        if (InventoryManager.Instance != null && InventoryManager.Instance.HasItem(itemID))
        {
            teniaObjeto = true;
            InventoryManager.Instance.DecrementarUsos(itemID);
            spriteRenderer.sprite = brillando;
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