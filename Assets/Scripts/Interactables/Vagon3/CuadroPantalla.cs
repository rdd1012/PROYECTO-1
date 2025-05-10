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
    private void OnEnable()
    {
        Time.timeScale = 0f;
        PlayerController.Instance.AddMovementLock();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        PlayerController.Instance.RemoveMovementLock();
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
