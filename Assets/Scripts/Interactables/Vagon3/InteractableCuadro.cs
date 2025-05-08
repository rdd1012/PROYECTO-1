using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCuadro : MonoBehaviour, IInteractable {
    [SerializeField] private Item itemToGive;
    private bool inventoryHasItem = false;
    AudioSource audioSource;
    bool teniaObjeto = false;
    public bool TeniaObjeto { get => teniaObjeto; set => teniaObjeto = value; }
    InteractableData interactableData;
    SpriteRenderer spriteRenderer;
    [SerializeField]Canvas cuadropantalla;
    public InteractableData InteractableData => interactableData;
    private void Start()
    {
        interactableData = GetComponent<InteractableData>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void OnClickAction()
    {
        cuadropantalla.gameObject.SetActive(true);
    }
    public void GiveItem()
    {
        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.Items)
            {
                if (_item.itemID == itemToGive.itemID)
                {
                    inventoryHasItem = true;
                    break;
                }
            }
            if (!inventoryHasItem)
            {
                InventoryManager.Instance.AddItem(itemToGive);
                inventoryHasItem = true;
                // audioSource.Play();
            }

        }
    }
    public void QuitarItem(int itemID)
    {
        if (InventoryManager.Instance != null && InventoryManager.Instance.HasItem(itemID))
        {
            teniaObjeto = true;
            InventoryManager.Instance.DecrementarUsos(itemID);
        }
    }
}
