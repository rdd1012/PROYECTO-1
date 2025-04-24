using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBombilla : MonoBehaviour, IInteractable
{
    private Light luz;
    private bool lightIsOn;
    public bool LightIsOn { get; set; }
    private InteractableVisuals interactableVisuals;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteEncendido;
    [SerializeField] Sprite spriteApagado;
    private void Start()
    {
        spriteRenderer = transform.parent.GetComponentInChildren<SpriteRenderer>();
        interactableVisuals = transform.parent.GetComponentInChildren<InteractableVisuals>();
        luz = GetComponentInChildren <Light>();
        if (luz.enabled) lightIsOn=true;
        else lightIsOn = false;
    }
  
    public void OnClickAction()
    {
        interactableVisuals.DoSelectAnimation();
        SwitchLight();
    }
    private void TurnOnLight() { luz.enabled = true; lightIsOn = true; spriteRenderer.sprite = spriteEncendido; }
    private void TurnOffLight() { luz.enabled = false; lightIsOn = false; spriteRenderer.sprite = spriteApagado; }
    private void SwitchLight() { if (lightIsOn) TurnOffLight(); else TurnOnLight(); }
}
