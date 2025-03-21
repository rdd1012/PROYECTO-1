using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBombilla : MonoBehaviour, IInteractable
{
    private Light luz;
    private bool lightIsOn;
    public bool LightIsOn { get; set; }
    private void Start()
    {
        luz = GetComponentInChildren <Light>();
        if (luz.enabled) lightIsOn=true;
        else lightIsOn = false;
    }
  
    public void OnClickAction()
    {
        SwitchLight();
    }
    private void TurnOnLight() { luz.enabled = true; lightIsOn = true; }
    private void TurnOffLight() { luz.enabled = false; lightIsOn = false; }
    private void SwitchLight() { if (lightIsOn) TurnOffLight(); else TurnOnLight(); }
}
