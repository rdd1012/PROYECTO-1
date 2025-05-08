using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPCLuces : NPCBase, IInteractable
{
    private InteractableData interactableData;
    SpriteRenderer spriteRenderer;
    [SerializeField]Sprite pestañeo;
    [SerializeField] Sprite hablando;
    YapBubble yapBubble;
    Sprite normal;
    [SerializeField] InteractableBombillaCarta interactableBombillaCarta;
    [SerializeField] private NPCdialogoSO dialogos;
    public bool IsInteractable() { return true; }
    void Start()
    {

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        normal = spriteRenderer.sprite;
        yapBubble = GetComponentInChildren<YapBubble>();
        yapBubble.gameObject.SetActive(false);
        interactableData = GetComponent<InteractableData>();
        StartCoroutine(Blink(pestañeo, spriteRenderer));
    }
    public void OnClickAction() 
    {
      
        if (interactableBombillaCarta.ComprobarLuces() && interactableBombillaCarta.ComprobarCortinas())
        {
            StartCoroutine(Yap(dialogos.frases[1], hablando, normal, spriteRenderer, yapBubble));
        }
        else
        {
            StartCoroutine(Yap(dialogos.frases[0], hablando, normal, spriteRenderer, yapBubble));
        }
    
    }

}
