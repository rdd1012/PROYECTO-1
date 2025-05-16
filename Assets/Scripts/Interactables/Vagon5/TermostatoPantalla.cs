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

}
