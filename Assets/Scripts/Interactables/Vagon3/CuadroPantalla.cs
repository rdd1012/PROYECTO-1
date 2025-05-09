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
    private void Update()
    {
        if (isActiveAndEnabled) { Time.timeScale = 0f; }
        else { Time.timeScale = 1f; }
    }
    private void Start()
    {
      //  audioSource = GetComponent<AudioSource>();
        palanca.enabled = false;
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
                palanca.enabled = true ;
                Destroy(botonTrigo);
                
            }
        }


    }
}
