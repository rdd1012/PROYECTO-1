using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance; // Añade esta línea

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
        private float speed = 10f;
    private bool isMovingLeft=false;
    public bool IsMovingLeft { get { return isMovingLeft; } }

    public void GoToItem(InteractableData item)
    {
        IInteractable interactableItem = item.GetComponent<IInteractable>();
        if (interactableItem == null) return;

        StartCoroutine(MoveToPoint(item.GoToPoint.position, interactableItem));
    }
    public IEnumerator MoveToPoint(Vector2 point, IInteractable interactable)
    {
        Vector2 targetPoint = new Vector2(point.x, transform.position.y);

         isMovingLeft = targetPoint.x < transform.position.x;

        while (Vector2.Distance(transform.position, targetPoint) > 0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPoint;

        if (interactable != null)
        {
            Interact(interactable);
        }
    }

    public void Interact(IInteractable item)
    {
        item.OnClickAction();
    }
}