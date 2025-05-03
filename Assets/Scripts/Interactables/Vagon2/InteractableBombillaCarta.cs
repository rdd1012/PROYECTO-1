using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBombillaCarta : MonoBehaviour, IInteractable
{
    private Light luz;
    private bool lightIsOn;
    [SerializeField] private Item itemToGive;
    private bool inventoryHasItem = false;

    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteEncendido;
    [SerializeField] Sprite spriteApagado;
    [SerializeField] Sprite spriteApagadoCarta;

    AudioSource audioSource;
    [SerializeField] AudioClip sonidoEncender;
    [SerializeField] AudioClip sonidoApagar;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        luz = GetComponentInChildren<Light>();
        if (luz.enabled) { lightIsOn = true; spriteRenderer.sprite = spriteEncendido; }
        else {lightIsOn = false; spriteRenderer.sprite = spriteApagadoCarta;
        }
    }
  
    public void OnClickAction()
    {
        SwitchLight();
        
    }
    public void TurnOnLight()
    {
        luz.enabled = true;
        lightIsOn = true;
        spriteRenderer.sprite = spriteEncendido;
        audioSource.clip = sonidoEncender;
        GiveItem();
    }
    public void TurnOffLight()
    {
        luz.enabled = false;
        lightIsOn = false;
        if (inventoryHasItem )
        spriteRenderer.sprite = spriteApagado;
        else spriteRenderer.sprite = spriteApagadoCarta;
        audioSource.clip = sonidoApagar;
    }
    public void SwitchLight()
    {
        if (lightIsOn) TurnOffLight();
        else TurnOnLight();
        audioSource.Play();
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
