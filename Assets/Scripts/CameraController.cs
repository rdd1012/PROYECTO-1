using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private float moveSpeed = 15f;
    private int edgeScrollSize = 50;
    private float minX = 19f;
    private float maxX = 19f;
    [SerializeField] GameObject flechaDerecha;
    [SerializeField] GameObject flechaIzquierda;
    private void Start()
    {
        flechaIzquierda.SetActive(false);
        flechaDerecha.SetActive(false);
    }
    private void Update()
    {
        Vector3 inputDir = Vector3.zero;


        if (Input.mousePosition.x < edgeScrollSize)
        {
            inputDir.x = -1f;
            flechaIzquierda.SetActive(true);
        }
        else if (Input.mousePosition.x > Screen.width - edgeScrollSize)
        {
            inputDir.x = 1f;
            flechaDerecha.SetActive(true);
        }
        else
        {
            flechaIzquierda.SetActive(false);
            flechaDerecha.SetActive(false);
        }


            Vector3 moveDir = transform.right * inputDir.x;

        
        Vector3 newPosition = transform.position + moveDir * moveSpeed * Time.deltaTime;

        
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        
        transform.position = newPosition;
    }
}