using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRadio : MonoBehaviour, IInteractable {
    SpriteRenderer spriteRenderer;
    [SerializeField]Sprite spriteFuera;
    private bool puzzleCompleto = false;
    [SerializeField] private Canvas puzzleCanvas;
    bool radioFuera=false;
    private bool inventoryHasItem = false;
    [SerializeField] private Item itemToGive;
    AudioSource audioSource;
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ComprobarPuzzle());
    }
    public void OnClickAction()
    {
        if (!radioFuera) 
        {
            radioFuera = true;
            spriteRenderer.sprite = spriteFuera;
            return;
            if (!puzzleCompleto) puzzleCanvas.gameObject.SetActive(true);
        }
    }
    IEnumerator ComprobarPuzzle()
    {
        bool estaCompleto = false;
        while (!estaCompleto)
        {
            if (puzzleCompleto)
            {
                GiveItem();
                estaCompleto = true;
            }
            yield return new WaitForSeconds(0.05f);
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
