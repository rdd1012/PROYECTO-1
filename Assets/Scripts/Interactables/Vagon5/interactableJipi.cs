using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class interactableJipi : NPCBase ,IInteractable
{
    [SerializeField] InteractableLoro loro;
    [SerializeField] NPCdialogoSO dialogos;
    InteractableData interactableData; 
    YapBubble yapBubble;
    Sprite normal;
    [SerializeField]Sprite hablando;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        normal = spriteRenderer.sprite;
        yapBubble = GetComponentInChildren<YapBubble>();
        yapBubble.gameObject.SetActive(false);
    }
    public bool TieneItem() { return true; }
    public void OnClickAction()
    {
        if (!loro.InventoryHasCarta) 
        {
            StartCoroutine(Yap(dialogos.frases[0], hablando, normal, spriteRenderer, yapBubble));
        }
        else if (loro.InventoryHasCarta && !loro.InventoryHasPluma) 
        { 
            StartCoroutine(Yap(dialogos.frases[1], hablando, normal, spriteRenderer, yapBubble)); 
        }
        else if(loro.InventoryHasPluma)
        {
            StartCoroutine(Yap(dialogos.frases[2], hablando, normal, spriteRenderer, yapBubble));
        }
    }
    public bool IsInteractable() { return true; }
}
