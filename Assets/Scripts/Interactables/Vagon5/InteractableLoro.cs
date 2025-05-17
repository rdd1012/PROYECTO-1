using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLoro : MonoBehaviour, IInteractable {
    private InteractableData interactableData;
    [SerializeField] Item cartaDar;
    [SerializeField] Item plumaDar;
    AudioSource audioSource;
    bool teniaObjeto = false;
    public bool TeniaObjeto
    {
        get { return teniaObjeto; }
        set { teniaObjeto = value; }
    }
    bool inventoryHasCarta=false;
    
    public bool InventoryHasCarta
    {
        get { return inventoryHasCarta; }
        set { inventoryHasCarta = value; }
    }
    bool inventoryHasPluma = false;
    public bool InventoryHasPluma
    {
        get { return inventoryHasPluma; }
        set { inventoryHasPluma = value; }
    }
    public InteractableData InteractableData
    {
        get { return interactableData; }
        set { interactableData = value; }
    }
    [SerializeField] Canvas loroPantalla;


    private int numeroCaricias = 0;
    public int NumeroCaricias
    {
        get { return numeroCaricias; }
        set { numeroCaricias = value; }
    }


    private void Start()
    {
        interactableData = GetComponent<InteractableData>();
        audioSource = GetComponent<AudioSource>();
    }

   

    public void OnClickAction()
    {
        loroPantalla.gameObject.SetActive(true);
    }

    public bool TieneItem()
    {
        return interactableData.CheckItemRequirement();
    }
    public bool IsInteractable() { return true; }



    public void GiveCarta()
    {
        if (InventoryManager.Instance != null && !InventoryHasCarta)
        { 
            InventoryManager.Instance.AddItem(cartaDar);
            inventoryHasCarta = true;
            audioSource.Play();
        }
    }
    public void GivePluma()
    {
        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.Items)
            {
                if (_item.itemID == plumaDar.itemID)
                {
                    inventoryHasPluma = true;
                    break;
                }
            }
            if (!inventoryHasPluma)
            {
                InventoryManager.Instance.AddItem(plumaDar);
                inventoryHasPluma = true;
                audioSource.Play();
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
