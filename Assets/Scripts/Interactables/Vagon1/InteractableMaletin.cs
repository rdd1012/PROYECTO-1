using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableMaletin : MonoBehaviour, IInteractable {
    private bool puzzleCompleto = false;
    private bool inventoryHasItem = false;
    [SerializeField] private Item itemToGive;
    [SerializeField] private Canvas puzzleCanvas;
    private InteractableVisuals interactableVisuals;
    AudioSource audioSource;
    public bool GetPuzzleCompleto() { return puzzleCompleto; }
    public void SetPuzzleCompleto(bool _puzzleCompleto) { puzzleCompleto = _puzzleCompleto; }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ComprobarPuzzle());
    }
    public void OnClickAction()
    {
        if (!puzzleCompleto) puzzleCanvas.gameObject.SetActive(true);
    }
    IEnumerator ComprobarPuzzle() 
    {
        bool estaCompleto=false;
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

