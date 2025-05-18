using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCaja : MonoBehaviour,IInteractable
{
    [SerializeField]InteractablePuertaCarga puertaCarga;

    [SerializeField] private Item itemToGive;
    AudioSource audioSource;
    InteractableData interactableData;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        interactableData = GetComponent<InteractableData>();
        
    }
    public bool TieneItem() { return interactableData.CheckItemRequirement(); }
    public void OnClickAction()
    {
        if (TieneItem())
        {
            QuitarItem(interactableData.requiredItemID);
            puertaCarga.Unlock();
            audioSource.Play();
            Vector3 posicion = gameObject.transform.localPosition;

            Vector3 targetPosition = transform.localPosition + new Vector3(-10, 0, 0);
            StartCoroutine(MoveToPosition(targetPosition, 2));
        }
    }
    IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            
            transform.localPosition = Vector3.Lerp(
                startPosition,
                target,
                elapsedTime / duration
            );
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        transform.localPosition = target; 
    }
    public bool IsInteractable() { return true; }
    private void QuitarItem(int itemID)
    {
        if (InventoryManager.Instance != null && InventoryManager.Instance.HasItem(itemID))
        {
            InventoryManager.Instance.DecrementarUsos(itemID);
        }
    }
}
