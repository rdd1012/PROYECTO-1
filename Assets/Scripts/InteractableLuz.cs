using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLuz : MonoBehaviour, IInteractable
{
    public static Action OnClickLight;
    public void OnClickAction()
    {
        Debug.Log("Clickaste en la luz!!!!");
        OnClickLight.Invoke();
    }
    void OnEnable() 
    {
        InteractablesManager.AddToInteractablesEvent.Invoke(transform);
    }
    void OnDisable()
    {
        InteractablesManager.RemoveFromInteractablesEvent.Invoke(transform);
    }
    
}
