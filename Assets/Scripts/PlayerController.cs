using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance;
    bool isMoving = false;
    Animator anim;
    private bool controlsEnabled = true;
    public bool ControlsEnabled { get { return controlsEnabled;  } }
    AudioSource audioSourceSeleccion;
    SpriteRenderer playerSpriteRenderer;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSourceSeleccion = GetComponent<AudioSource>();
        anim.Play("Idle");
        anim.SetBool("EstaCaminando", false);
    }
    public void TogglePlayerControl(bool state)
    {
        controlsEnabled = state;
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
    private bool isMovingLeft = false;
    public bool IsMovingLeft { get { return isMovingLeft; } }
    public void GoToItem(InteractableData item)
    {

        if (item == null || item.GoToPoint == null) return;
        IInteractable interactableItem = item.GetComponent<IInteractable>();
        InteractableVisuals itemvisuals = item.GetComponentInChildren<InteractableVisuals>();
        if (itemvisuals == null) return;
        else StartCoroutine(itemvisuals.SelectAnimation());
        if (interactableItem == null || !interactableItem.IsInteractable()) { return; }
        Debug.Log("IsInteractable? " + interactableItem.IsInteractable());
        audioSourceSeleccion.Play();

        Vector2 targetPoint = new Vector2(item.GoToPoint.position.x, transform.position.y);

       
        if (Vector2.Distance(transform.position, targetPoint) <= 0.01f)
        {
            Interact(interactableItem);
            return;
        }

        if (!isMoving)
        {
            StartCoroutine(MoveToPoint(
                item.GoToPoint.position,
                item.GetComponentInChildren<InteractableVisuals>().gameObject.transform.position,
                interactableItem
            ));
        }
    }
    private void Update()
    {
        if (!controlsEnabled) return;
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            HandleClickInteraction();
        }
    }

   
    private void HandleClickInteraction()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                IInteractable iInteractable = hit.collider.GetComponent<IInteractable>();
                if (iInteractable != null && iInteractable.IsInteractable())
                {
                    GoToItem(hit.collider.GetComponent<InteractableData>());
                }
            }
        }
    }

    

    public IEnumerator MoveToPoint(Vector2 point, Vector2 pointVisuals, IInteractable interactable)
    {
        Vector2 targetPoint = new Vector2(point.x, transform.position.y);

        isMoving = true;
        anim.Play("Caminar");
        anim.SetBool("EstaCaminando", true);
        Vector2 targetPointVisuals = new Vector2(pointVisuals.x, transform.position.y);
        float direction = Mathf.Sign(targetPointVisuals.x - transform.position.x);

        //playerSpriteRenderer.flipX = (direction < 0);
        //playerSpriteRenderer.UpdateGITexture();
        
        if (direction != Mathf.Sign(transform.localScale.x))
        {
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x) * direction;
            transform.localScale = newScale;
        }
        


        while (Vector2.Distance(transform.position, targetPoint) > 0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
            yield return null;
        }
        
        
        if (!Mathf.Approximately(targetPointVisuals.x, targetPoint.x))
        {
            float lookDirection = Mathf.Sign(targetPointVisuals.x - transform.position.x);
            if (lookDirection != Mathf.Sign(transform.localScale.x))
            {
                Vector3 newScale = transform.localScale;
                newScale.x = Mathf.Abs(newScale.x) * lookDirection;
                transform.localScale = newScale;
            }
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
        //if (!item.TieneItem()) { audioSourceError.Play(); }
    }
}