using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuertaHorquilla : MonoBehaviour,IInteractable
{
    bool manchaBorrada=false;
    public bool ManchaBorrada { get { return manchaBorrada; } set { manchaBorrada = value; } } 
    bool puertaAbierta = false;
    public bool PuertaAbierta { get { return puertaAbierta; } set { puertaAbierta = value; } }
    [SerializeField]PuertaPantalla puertaPantalla;
    [SerializeField]InteractableData interactableData;
    public InteractableData InteractableData { get { return interactableData; } }
    private void Start()
    {
        interactableData = GetComponent<InteractableData>();
    }

    public void OnClickAction()
    {
        if (!puertaAbierta || !manchaBorrada)
            puertaPantalla.gameObject.SetActive(true);
        else GameManager.Instance.PasarDeNivel();
    }
    public void QuitarItem(int itemID)
    {

        if (InventoryManager.Instance != null)
        {
            foreach (Item _item in InventoryManager.Instance.Items)
            {
                if (_item.itemID == itemID)
                {
                    _item.usos--;
                    if (_item.usos == 0) 
                    { InventoryManager.Instance.RemoveItem(itemID); }
                    break;
                }
            }
        }
    }
}
