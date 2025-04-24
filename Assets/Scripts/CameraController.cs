using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    private void Update()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);
         int edgeScrollSize = 20;

        if (Input.mousePosition.x < edgeScrollSize) { inputDir.x = -1f; }
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) { inputDir.x = +1f; }
        Vector3 moveDir = transform.right * inputDir.x;
        float moveSpeed = 15f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

}

