using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleMaletin : MonoBehaviour {
    [SerializeField] TextMeshProUGUI numeroUno;
    [SerializeField] TextMeshProUGUI numeroDos;
    [SerializeField] TextMeshProUGUI numeroTres;
    private int numero1 = 0;
    private int numero2 = 0;
    private int numero3 = 0;
    [SerializeField] InteractableMaletin interactableNPCMaletin;

    [SerializeField] int[] numerosSolucion;

    private void ComprobarResultadoPuzzle()
    {
        if (numerosSolucion.Length < 3)
        {
            Debug.LogError("El array de solución debe tener al menos 3 elementos.");
            return;
        }

        int.TryParse(numeroUno.text.Trim(), out numero1);
        int.TryParse(numeroDos.text.Trim(), out numero2);
        int.TryParse(numeroTres.text.Trim(), out numero3);

        if (numero1 == numerosSolucion[0] &&
            numero2 == numerosSolucion[1] &&
            numero3 == numerosSolucion[2])
        {
            interactableNPCMaletin.SetPuzzleCompleto(true);
        }
    }

    public void SumarNumero(TextMeshProUGUI numeroTMP)
    {
        int numero;
        int.TryParse(numeroTMP.text.Trim(), out numero);
        numero++;
        if (numero > 9) numero = 0;
        numeroTMP.SetText(numero.ToString());
        ComprobarResultadoPuzzle();
    }

    public void RestarNumero(TextMeshProUGUI numeroTMP)
    {
        int numero;
        int.TryParse(numeroTMP.text.Trim(), out numero);
        numero--;
        if (numero < 0) numero = 9;
        numeroTMP.SetText(numero.ToString());
        ComprobarResultadoPuzzle();
    }

    private void Update()
    {
        if (isActiveAndEnabled) Time.timeScale = 0f;
    }

    public void SalirPuzzle()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }
}
