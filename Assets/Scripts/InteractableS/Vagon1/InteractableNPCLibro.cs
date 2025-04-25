using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class InteractableNPCLibro : MonoBehaviour, IInteractable {
    private InteractableData interactableData; 
    [SerializeField] private Item itemToGive; 
    bool teniaObjeto = false;
    private bool inventoryHasItem = false;
    AudioSource audioSource;
    private void Start()
    {
        interactableData = GetComponent<InteractableData>();
        audioSource = GetComponent<AudioSource>();
    }
    public void OnClickAction()
    {
        if (interactableData.CheckItemRequirement())
        {
            QuitarItem(interactableData.requiredItemID);
            if (teniaObjeto)
            {
                GiveItem();
            }
        }
        else
        {
            Debug.Log("Necesitas el item requerido");
        }
    }


    private void QuitarItem(int itemID)
    {

        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.Items)
            {
                if (_item.itemID == itemID)
                {
                    teniaObjeto = true;
                    InventoryManager.Instance.RemoveItem(itemID);
                    break;
                }
            }
        }
    }
    private void GiveItem()
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
                audioSource.Play();
            }

        }
    }
}