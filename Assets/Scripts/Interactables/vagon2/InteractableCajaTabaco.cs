using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCajaTabaco : MonoBehaviour, IInteractable {
    [SerializeField] private Item itemToGive;
    private bool inventoryHasItem = false;
    //AudioSource audioSource;
    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }
    public bool IsInteractable() { return true; }
    public void OnClickAction()
    {
        GiveItem();
    }
    public bool TieneItem() { return true; }
    private void GiveItem()
    {
        if (InventoryManager.Instance != null)
        {
            if (!inventoryHasItem)
            {
                InventoryManager.Instance.AddItem(itemToGive);
                inventoryHasItem = true;
                Destroy(this.gameObject);
                // audioSource.Play();
            }

        }
    }
}
