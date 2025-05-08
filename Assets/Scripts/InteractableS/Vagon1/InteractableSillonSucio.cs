using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSillonSucio : MonoBehaviour, IInteractable {
    private InteractableData interactableData;
    public InteractableData InteractableData { 
        get {return interactableData; }
        private set {interactableData = value; }}
    [SerializeField] Canvas sillonSucioPantalla;
    private void Start()
    {
        interactableData = GetComponent<InteractableData>();
    }
    public void OnClickAction()
    {
        sillonSucioPantalla.gameObject.SetActive(true);
        
    }
    public bool TieneItem() { return true; }
    public bool IsInteractable() { return true; }
    public void QuitarItem(int itemID)
    {
        InventoryManager.Instance?.DecrementarUsos(itemID);
    }
}
