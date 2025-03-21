using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTag : MonoBehaviour

{
    private Vector3 nameTagPosition ;
    private void Update()
    {
        FollowMouse();
    }
    public void FollowMouse() 
    {
        transform.position = Input.mousePosition;
    }
}
