using System.Collections;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class InteractableNPCLibro : MonoBehaviour, IInteractable,INPC {
    private InteractableData interactableData; 
    [SerializeField] private Item itemToGive; 
    bool teniaObjeto = false;
    private bool inventoryHasItem = false;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite normal;
    [SerializeField] Sprite hablando;
    [SerializeField] Sprite libro;
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
    public void OnClickAction()
    {
        if (interactableData.CheckItemRequirement())
        {
            QuitarItem(interactableData.requiredItemID);
            
            StartCoroutine(Yap(dialogos.frases[1]));
            if (teniaObjeto)
            {
                GiveItem();
            }
        }
        else
        {
            StartCoroutine(Yap(dialogos.frases[0]));
        }
    }
    public IEnumerator Yap(string _text)
    {
        yapBubble.gameObject.SetActive(true);
        yapBubble.SetupText(_text);
        yield return new WaitForSeconds(3f);
        yapBubble.gameObject.SetActive(false);
    }

    private void QuitarItem(int itemID)
    {

        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.Items)
            {
                if (_item.itemID == itemID)
                {
                    teniaObjeto = true;
                    InventoryManager.Instance.RemoveItem(itemID);
                    spriteRenderer.sprite = libro;
                    break;
                    
                }
            }
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
                audioSource.Play();
                
            }

        }
    }
}