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
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

}
