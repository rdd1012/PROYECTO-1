using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLuz : MonoBehaviour, IInteractable
{
    public void OnClickAction()
    {
        Debug.Log("Clickaste en la luz!!!!");
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
