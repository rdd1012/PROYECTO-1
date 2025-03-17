using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePersiana : MonoBehaviour, IInteractable
{
    public void OnClickAction()
    {
        Debug.Log("Clickaste en la persiana!!!!");
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
