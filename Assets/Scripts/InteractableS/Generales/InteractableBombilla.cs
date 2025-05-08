using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBombilla : MonoBehaviour, IInteractable
{
    private Light luz;
    private bool lightIsOn;
    public bool LightIsOn { get { return lightIsOn; } set { lightIsOn = value; } }

    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteEncendido;
    [SerializeField] Sprite spriteApagado;

    AudioSource audioSource;
    [SerializeField] AudioClip sonidoEncender;
    [SerializeField] AudioClip sonidoApagar;
    public bool TieneItem() { return true; }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        luz = GetComponentInChildren<Light>();
        if (luz.enabled) { lightIsOn = true; spriteRenderer.sprite = spriteEncendido; }
        else {lightIsOn = false; spriteRenderer.sprite = spriteApagado;
        }
    }
    public bool IsInteractable() { return true; }
    public  void OnClickAction()
    {
        SwitchLight();
        
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
}
