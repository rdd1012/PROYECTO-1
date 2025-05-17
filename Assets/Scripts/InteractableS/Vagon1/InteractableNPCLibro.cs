using System.Collections;
using UnityEngine;

public class InteractableNPCLibro : NPCBase, IInteractable {
    private InteractableData interactableData; 
    [SerializeField] private Item itemToGive; 
    bool teniaObjeto = false;
    private bool inventoryHasItem = false;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Sprite normal;
    [SerializeField] Sprite pestañeo;
    [SerializeField] Sprite pestañeoLibro;
    [SerializeField] Sprite hablando;
    [SerializeField] Sprite libro;
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
        StartCoroutine(Blink(pestañeo,spriteRenderer));
    }
    public bool TieneItem() { return interactableData.CheckItemRequirement(); }
    public void OnClickAction()
    {
        if (TieneItem())
        {
            if (!teniaObjeto)
            QuitarItem(interactableData.requiredItemID);
            StopCoroutine(Blink(pestañeo, spriteRenderer));
            StartCoroutine(Blink(pestañeoLibro, spriteRenderer));

            GiveItem();
        }
        if (teniaObjeto) StartCoroutine(Yap(dialogos.frases[1], hablando, normal, spriteRenderer, yapBubble));
        else StartCoroutine(Yap(dialogos.frases[0], hablando, normal, spriteRenderer, yapBubble));
    }
    public override IEnumerator Yap(string _text,Sprite _hablando,Sprite _normal,SpriteRenderer _spriteRenderer,YapBubble _yapbubble)
    {
        _spriteRenderer.sprite = _hablando;
        _yapbubble.gameObject.SetActive(true);
        _yapbubble.SetupText(_text);
        yield return new WaitForSeconds(3f);
        _yapbubble.gameObject.SetActive(false);
        if (teniaObjeto) _spriteRenderer.sprite = libro;
        else _spriteRenderer.sprite = _normal;
    }

    private void QuitarItem(int itemID)
    {
        if (InventoryManager.Instance != null && InventoryManager.Instance.HasItem(itemID))
        {
            teniaObjeto = true;
            InventoryManager.Instance.DecrementarUsos(itemID);
            spriteRenderer.sprite = libro;
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