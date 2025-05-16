using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPCZapatos : NPCBase,IInteractable
{
    private InteractableData interactableData;
    [SerializeField] private Item itemToGive;
    bool teniaObjeto = false;
    private bool inventoryHasItem = false;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Sprite normal;
    [SerializeField] Sprite pestañeo;
    [SerializeField] Sprite pestañeozapatosLimpios;
    [SerializeField] Sprite hablando;
    [SerializeField] Sprite hablandozapatosLimpios;
    [SerializeField] Sprite zapatosLimpios;
    [SerializeField] private NPCdialogoSO dialogos;
    YapBubble yapBubble;
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        normal = spriteRenderer.sprite;
        yapBubble = GetComponentInChildren<YapBubble>();
        yapBubble.gameObject.SetActive(false);
        interactableData = GetComponent<InteractableData>();
        if (!TieneItem()) StartCoroutine(Blink(pestañeo, spriteRenderer));
        else StartCoroutine(BlinkLimpio(pestañeozapatosLimpios, spriteRenderer));
        audioSource = GetComponent<AudioSource>();
    }
    public bool TieneItem() { return interactableData.CheckItemRequirement(); }
    public void OnClickAction()
    {
        if(InventoryManager.Instance.HasItem(3))
        {
            if (TieneItem())
            {
                if (!teniaObjeto)
                { QuitarItem(interactableData.requiredItemID); return; }
                
            }
            if (teniaObjeto)
            {
                StartCoroutine(Yap(dialogos.frases[1], hablando, hablandozapatosLimpios, spriteRenderer, yapBubble));
                GiveItem();
            }

            else StartCoroutine(Yap(dialogos.frases[0], hablando, normal, spriteRenderer, yapBubble));
        }
    }
    public IEnumerator BlinkLimpio(Sprite _pestañeo, SpriteRenderer _spriteRenderer)
    {
        Sprite _normal;


        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4f, 5f));
            _normal = _spriteRenderer.sprite;
            _spriteRenderer.sprite = _pestañeo;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.sprite = _normal;
        }

    }
    public bool IsInteractable() { return true; }
    public override IEnumerator Yap(string _text, Sprite _hablando, Sprite _normal, SpriteRenderer _spriteRenderer, YapBubble _yapbubble)
    {
        _spriteRenderer.sprite = _hablando;
        _yapbubble.gameObject.SetActive(true);
        _yapbubble.SetupText(_text);
        yield return new WaitForSeconds(3f);
        _yapbubble.gameObject.SetActive(false);
        if (teniaObjeto) _spriteRenderer.sprite = zapatosLimpios;
        else _spriteRenderer.sprite = _normal;
    }
    private void QuitarItem(int itemID)
    {
        if (InventoryManager.Instance != null && InventoryManager.Instance.HasItem(itemID))
        {
            teniaObjeto = true;
            InventoryManager.Instance.DecrementarUsos(itemID);
            spriteRenderer.sprite = zapatosLimpios;
            audioSource.Play();
        }
    }

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
                

            }

        }
    }
}
