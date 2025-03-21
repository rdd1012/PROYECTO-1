using UnityEngine;
using System.Collections;
using System; 
public class PlayerController : MonoBehaviour {
    private float speed = 10f;
    private float accuracy = 0.5f;
    private bool isMoving;

    public IEnumerator MoveToPoint(Vector2 point, Action onArrived = null) 
    {
        isMoving = true;
        Vector2 targetPoint = new Vector2(point.x, transform.position.y);

        while (Vector2.Distance(transform.position, targetPoint) > accuracy)
        {

            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPoint,
                speed * Time.deltaTime
            );
            yield return null;
        }

        transform.position = targetPoint;
        isMoving = false;
        onArrived?.Invoke(); 
    }

    public void GoToItem(ItemData item)
    {
        IInteractable interactableItem = item.GetComponent<IInteractable>();
        if (interactableItem == null) return;

        StartCoroutine(MoveToPoint(item.GoToPoint.position, () => Interact(interactableItem)));
    }

    public void Interact(IInteractable item)
    {
        item.OnClickAction(); 
    }
}