using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SillonSucioPantalla : MonoBehaviour
{
    [SerializeField]InteractableSillonSucio sillon;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void BorrarMancha(Image image)
    {
        if (sillon.InteractableData.CheckItemRequirement())
        {
            sillon.QuitarItem(sillon.InteractableData.requiredItemID);
            image.gameObject.SetActive(false);
            audioSource.Play();
            //GameManager.Instance.SetCursorDefault();
        }

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
    public void SalirPantalla()
    {
        this.gameObject.SetActive(false);
    }
}
