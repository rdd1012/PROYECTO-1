using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemData : MonoBehaviour, IInteractable {
    public Item item;

    public void OnClickAction()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < 2f)
        {
            InventoryManager.Instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}
