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
    public bool GetPuzzleCompleto() { return puzzleCompleto; }
    public void SetPuzzleCompleto(bool _puzzleCompleto) { puzzleCompleto = _puzzleCompleto; }
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ComprobarPuzzle());
    }
    public bool TieneItem() { return true; }
    public void OnClickAction()
    {
        if (!radioFuera) 
        {
            radioFuera = true;
            spriteRenderer.sprite = spriteFuera;
            audioSource.Play();
            return;
            
        }
        if (!puzzleCompleto) puzzleCanvas.gameObject.SetActive(true);
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
            yield return new WaitForSecondsRealtime(0.05f);
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

            }

        }
    }
    public bool IsInteractable() { return true; }
}
