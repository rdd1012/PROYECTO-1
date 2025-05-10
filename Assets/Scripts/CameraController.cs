using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private float moveSpeed = 15f;
    private int edgeScrollSize = 50;
    private float minX = -18.7f;
    private float maxX = 18.7f;
    [SerializeField] GameObject flechaDerecha;
    [SerializeField] GameObject flechaIzquierda;
    private bool controlsEnabled = true;
    private void Start()
    {
        flechaIzquierda.SetActive(false);
        flechaDerecha.SetActive(false);
    }
    public void ToggleCameraControl(bool state)
    {
        controlsEnabled = state;
        flechaIzquierda.SetActive(false);
        flechaDerecha.SetActive(false);
    }
    private void Update()
    {
        if (!controlsEnabled) return;
        Vector3 inputDir = Vector3.zero;


        if (Input.mousePosition.x < edgeScrollSize)
        {
            inputDir.x = -1f;
            if (Time.timeScale==1)
                flechaIzquierda.SetActive(true);
        }
        else if (Input.mousePosition.x > Screen.width - edgeScrollSize)
        {
            inputDir.x = 1f;
            if (Time.timeScale == 1)
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