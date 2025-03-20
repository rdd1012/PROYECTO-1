using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    private float speed = 10f;
    private Vector3 target;
    
    private void Start()
    {
        target = transform.position;
        
    }
    private void Update()
    {
        MovePlayer();
        
    }
    private void MovePlayer() {
        bool isMoving = false;
        if (target.x != transform.position.x) { isMoving = true; }

        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            float mouseXPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            target.x = mouseXPosition;

        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
