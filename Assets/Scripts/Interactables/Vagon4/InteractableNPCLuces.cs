using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPCLuces : NPCBase, IInteractable
{
    private InteractableData interactableData;
    SpriteRenderer spriteRenderer;
    [SerializeField]Sprite pestañeo;
    [SerializeField] Sprite hablando;
    [SerializeField] Sprite dormida;
    [SerializeField] Sprite dormidaHablando;
    YapBubble yapBubble;
    Sprite normal;
    [SerializeField] InteractableBombillaCarta interactableBombillaCarta;
    [SerializeField] private NPCdialogoSO dialogos;
    public bool IsInteractable() { return true; }
    bool isDormida = false;
    void Start()
    {

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        normal = spriteRenderer.sprite;
        yapBubble = GetComponentInChildren<YapBubble>();
        yapBubble.gameObject.SetActive(false);
        interactableData = GetComponent<InteractableData>();
        StartCoroutine(Blink(pestañeo, spriteRenderer));
    }
    public bool TieneItem() { return true; }
    public void OnClickAction() 
    {
      
        if (!interactableBombillaCarta.ComprobarLuces() && !interactableBombillaCarta.ComprobarCortinas())
        {
            if (!isDormida)
                StartCoroutine(Yap(dialogos.frases[0], hablando, normal, spriteRenderer, yapBubble));
        }
    
    }
    private void Update()
    {
        if (interactableBombillaCarta.ComprobarLuces() && interactableBombillaCarta.ComprobarCortinas() &&!isDormida)
        {
           isDormida = true;
           StopAllCoroutines();
           spriteRenderer.sprite = dormida;
           StartCoroutine(Blink(dormidaHablando, spriteRenderer));
        }
    }

}
