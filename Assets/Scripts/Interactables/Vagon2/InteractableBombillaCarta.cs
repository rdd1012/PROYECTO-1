using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableBombillaCarta : MonoBehaviour, IInteractable
{
    private Light luz;
    private bool lightIsOn;
    [SerializeField] private Item itemToGive;
    private bool inventoryHasItem = false;
    private bool todoBien;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteEncendido;
    [SerializeField] Sprite spriteApagado;
    [SerializeField] Sprite spriteEncendidoCarta;

    [SerializeField]InteractableBombilla[] bombillas;

    [SerializeField] InteractableCortina[] cortinas;

    AudioSource audioSource;
    [SerializeField] AudioClip sonidoEncender;
    [SerializeField] AudioClip sonidoApagar;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        luz = GetComponentInChildren<Light>();
        if (luz.enabled) { lightIsOn = true; spriteRenderer.sprite = spriteEncendido; }
        else {lightIsOn = false; spriteRenderer.sprite = spriteApagado;
        }
        StartCoroutine(ComprobarTodoBien());
    }
    public bool IsInteractable() { return true; }
    IEnumerator ComprobarTodoBien() 
    {    
        while (!inventoryHasItem)
        {
            if (ComprobarLuces() & ComprobarCortinas())
            {
                todoBien = true;
                spriteRenderer.sprite = spriteEncendidoCarta;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void OnClickAction()
    {     
        if (todoBien) 
        {
            GiveItem();
        }
        SwitchLight();

    }
    public bool ComprobarLuces()
    {
        int contadorLuces = 0;
        foreach (InteractableBombilla bombilla in bombillas)
        {
            if (bombilla.LightIsOn) contadorLuces++;
        }
        if (lightIsOn) contadorLuces++;
        Debug.Log(contadorLuces);
        if (contadorLuces == 3) return true;
        else return false;
    }
    public bool ComprobarCortinas()
    {
        int contadorCortinas = 0;
        foreach (InteractableCortina cortina in cortinas)
        {
            if (!cortina.IsOpen) contadorCortinas++;
        }
        Debug.Log(contadorCortinas);
        if (contadorCortinas == 3) return true;
        else return false;
    }
    public void TurnOnLight()
    {
        luz.enabled = true;
        lightIsOn = true;
        spriteRenderer.sprite = spriteEncendido;
        audioSource.clip = sonidoEncender;
    }
    public void TurnOffLight()
    {
        luz.enabled = false;
        lightIsOn = false;
        spriteRenderer.sprite = spriteApagado;
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
