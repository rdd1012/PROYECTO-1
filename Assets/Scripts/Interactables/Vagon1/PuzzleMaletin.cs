using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PuzzleMaletin : MonoBehaviour {
    [SerializeField] TextMeshProUGUI numeroUno;
    [SerializeField] TextMeshProUGUI numeroDos;
    [SerializeField] TextMeshProUGUI numeroTres;
    [SerializeField] Image pantallaVictoria;
    [SerializeField] Image puzzle;
    [SerializeField] InteractableMaletin interactableMaletin;
    private bool verificandoSolucion;
    private int numero1 = 0;
    private int numero2 = 0;
    private int numero3 = 0;

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
            StartCoroutine(VerificacionRetrasada());
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

    IEnumerator VerificacionRetrasada()
    {

        
        yield return new WaitForSecondsRealtime(1f); 

        
        int numero1, numero2, numero3;
        int.TryParse(numeroUno.text.Trim(), out numero1);
        int.TryParse(numeroDos.text.Trim(), out numero2);
        int.TryParse(numeroTres.text.Trim(), out numero3);

        if (numero1 == numerosSolucion[0] &&
            numero2 == numerosSolucion[1] &&
            numero3 == numerosSolucion[2])
        {
            PantallaVictoria();
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

  

    public void SalirPuzzle()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }
    void PantallaVictoria()
    {
        puzzle.gameObject.SetActive(false);
        pantallaVictoria.gameObject.SetActive(true);
    }
    public void LibroPantallaVictoria() 
    {
        StartCoroutine(LibroPantallaVictoriaCR());
    }
    IEnumerator LibroPantallaVictoriaCR() 
    {
        interactableMaletin.SetPuzzleCompleto(true);
        yield return new WaitForSecondsRealtime(1f);
        SalirPuzzle();
    } 
}
