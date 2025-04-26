using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance;
    bool isMoving=false;
    Animator anim;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.Play("Idle");
        anim.SetBool("EstaCaminando", false);
    }
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
        if (item == null || item.GoToPoint == null) return; 

        InteractableVisuals itemvisuals = item.GetComponentInChildren<InteractableVisuals>();
        if (itemvisuals == null) return; 

        IInteractable interactableItem = item.GetComponent<IInteractable>();
        if (interactableItem == null) return;

        StartCoroutine(itemvisuals.SelectAnimation());

        if (!isMoving)
        {
            StartCoroutine(MoveToPoint(item.GoToPoint.position, interactableItem));
        }
    }
    public IEnumerator MoveToPoint(Vector2 point, IInteractable interactable)
    {
        Vector2 targetPoint = new Vector2(point.x, transform.position.y);
        
        //if (targetPoint.x != transform.position.x) 
        isMovingLeft = targetPoint.x < transform.position.x;
        while (Vector2.Distance(transform.position, targetPoint) > 0f)
        {
            isMoving = true;
            anim.Play("Caminar");
            anim.SetBool("EstaCaminando", true);

            transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
        anim.Play("Idle");
        anim.SetBool("EstaCaminando", false);

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