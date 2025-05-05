using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuertaPantalla : MonoBehaviour
   
{
    [SerializeField]InteractablePuertaHorquilla puerta;
    [SerializeField] Image cerradura;
    public void BorrarMancha(Image image)
    {
        if (!puerta.ManchaBorrada) 
        {
            if (puerta.InteractableData.CheckItemRequirement(4))
            {
                puerta.ManchaBorrada = true;
                puerta.QuitarItem(4);
                image.gameObject.SetActive(false);
                cerradura.gameObject.SetActive(true);
            }
        }
        

    }
    public void AbrirPuerta(Image image)
    {
        if (!puerta.PuertaAbierta)
        {
            if (puerta.InteractableData.CheckItemRequirement(5))
            {
                image.gameObject.SetActive(false);
                puerta.PuertaAbierta = true;
                puerta.QuitarItem(5);
            }
        } 
    }

}
