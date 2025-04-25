using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBombilla : MonoBehaviour, IInteractable
{
    private Light luz;
    private bool lightIsOn;
    public bool LightIsOn { get; set; }

    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteEncendido;
    [SerializeField] Sprite spriteApagado;

    AudioSource audioSource;
    [SerializeField] AudioClip sonidoEncender;
    [SerializeField] AudioClip sonidoApagar;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        luz = GetComponentInChildren <Light>();
        if (luz.enabled) lightIsOn=true;
        else lightIsOn = false;
    }
  
    public void OnClickAction()
    {
        SwitchLight();
        
    }
    private void TurnOnLight() 
    {
        luz.enabled = true; 
        lightIsOn = true;
        spriteRenderer.sprite = spriteEncendido;
        audioSource.clip = sonidoEncender;
    }
    private void TurnOffLight() 
    { 
        luz.enabled = false; 
        lightIsOn = false; 
        spriteRenderer.sprite = spriteApagado;
        audioSource.clip = sonidoApagar;
    }
    private void SwitchLight() 
    {
        if (lightIsOn) TurnOffLight();
        else TurnOnLight();
        audioSource.Play();
    }
}
