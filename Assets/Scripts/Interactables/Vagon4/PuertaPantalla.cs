using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuertaPantalla : MonoBehaviour
   
{
    [SerializeField]InteractablePuertaHorquilla puerta;
    [SerializeField] Image cerradura;
    [SerializeField] AudioClip hawktua;
    [SerializeField] AudioClip trapo;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void BorrarMancha(Image image)
    {
        if (!puerta.ManchaBorrada) 
        {
            if (puerta.InteractableDataTrapo.CheckItemRequirement(puerta.InteractableDataTrapo.requiredItemID))
            {
                puerta.ManchaBorrada = true;
                puerta.QuitarItem(puerta.InteractableDataTrapo.requiredItemID);
                image.gameObject.SetActive(false);
                cerradura.gameObject.SetActive(true);
                audioSource.clip = trapo;
                audioSource.Play();
            }
        }
        

    }
    public void AbrirPuerta(Image image)
    {
        if (!puerta.PuertaAbierta)
        {
            if (puerta.InteractableDataHorquilla.CheckItemRequirement(puerta.InteractableDataHorquilla.requiredItemID))
            {
                image.gameObject.SetActive(false);
                puerta.PuertaAbierta = true;
                puerta.QuitarItem(puerta.InteractableDataHorquilla.requiredItemID);
                audioSource.clip = hawktua;
                audioSource.Play();
            }
        } 
    }

}
