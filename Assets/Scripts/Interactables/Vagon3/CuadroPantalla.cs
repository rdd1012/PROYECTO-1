using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuadroPantalla : MonoBehaviour
{
    [SerializeField] InteractableCuadro cuadro;
    [SerializeField] Button palanca;
    [SerializeField] Image fondoCuadroImagen;
    [SerializeField] Sprite spriteFeliz;
    [SerializeField] Button botonTrigo;
    //AudioSource audioSource;
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
    private void Start()
    {
        //  audioSource = GetComponent<AudioSource>();
        palanca.interactable   =  false;
    }
    public void LlenarTrigo()
    {
        if (!cuadro.TeniaObjeto)
        {
            if (cuadro.InteractableData.CheckItemRequirement(cuadro.InteractableData.requiredItemID))
            {
                cuadro.TeniaObjeto = true;
                cuadro.QuitarItem(cuadro.InteractableData.requiredItemID);
                fondoCuadroImagen.sprite = spriteFeliz;
                fondoCuadroImagen.raycastTarget = false;
                palanca.interactable = true;
                Destroy(botonTrigo.gameObject);
                Debug.Log("¡Palanca activada!");
            }
        }


    }
}
