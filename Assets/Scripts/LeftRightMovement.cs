using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponentInParent<PlayerController>();
    }
    private void FixedUpdate()
    {
        spriteRenderer.flipX = playerController.IsMovingLeft;
    }

}
