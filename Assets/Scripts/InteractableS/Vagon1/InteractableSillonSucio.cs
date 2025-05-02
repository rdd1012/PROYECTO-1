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
    public void QuitarItem(int itemID)
    {

        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.Items)
            {
                if (_item.itemID == itemID)
                {
                    InventoryManager.Instance.RemoveItem(itemID);
                    break;
                }
            }
        }
    }
}
