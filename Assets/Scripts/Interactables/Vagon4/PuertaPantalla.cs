using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuertaPantalla : MonoBehaviour
   
{
    [SerializeField]InteractablePuertaHorquilla puerta;
    [SerializeField] Image puertaImagen;
    [SerializeField] Sprite limpio;
    [SerializeField] Sprite abierto;
    [SerializeField] AudioClip hawktua;
    [SerializeField] AudioClip trapo;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void BorrarMancha()
    {
        if (!puerta.ManchaBorrada)
        {
            if (puerta.InteractableDataTrapo.CheckItemRequirement(puerta.InteractableDataTrapo.requiredItemID))
            {
                puerta.ManchaBorrada = true;
                puerta.QuitarItem(puerta.InteractableDataTrapo.requiredItemID);
                puertaImagen.sprite = limpio;
                audioSource.clip = trapo;
                audioSource.Play();
            }
        }
    }
    public void AbrirPuerta()
    {
        StartCoroutine(CRAbrirPuerta());
    }
    IEnumerator CRAbrirPuerta ()
    {
        if (!puerta.PuertaAbierta && puerta.ManchaBorrada)
        {
            if (puerta.InteractableDataHorquilla.CheckItemRequirement(puerta.InteractableDataHorquilla.requiredItemID))
            {
                puertaImagen.sprite = abierto;
                yield return new WaitForSeconds(1);
                audioSource.clip = hawktua;
                audioSource.Play();
                gameObject.SetActive(false);

                puerta.PuertaAbierta = true;
                puerta.QuitarItem(puerta.InteractableDataHorquilla.requiredItemID);

                

            }
        }
    }

}
