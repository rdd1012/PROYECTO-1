using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TermostatoPantalla : MonoBehaviour
{
    [SerializeField] InteractableTermostato interactableTermostato;
    [SerializeField] Slider sliderTemp;
    private void Update()
    {
        interactableTermostato.Temperatura = sliderTemp.value;
    }
    [SerializeField] CameraController cameraController;
    void OnEnable()
    {
        if (cameraController != null)
            cameraController.ToggleCameraControl(false);
        PlayerController.Instance.TogglePlayerControl(false);
        GameManager.Instance.GestionarCambioRaton(false);
    }
    private void OnDisable()
    {
        if (cameraController != null)
            cameraController.ToggleCameraControl(true);
        PlayerController.Instance.TogglePlayerControl(true);
        GameManager.Instance.GestionarCambioRaton(true);
    }

}
