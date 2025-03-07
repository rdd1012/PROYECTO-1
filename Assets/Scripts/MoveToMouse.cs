using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    private float speed = 7.5f;
    private Vector3 target;
    private void Start()
    {
        target = transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            target.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
