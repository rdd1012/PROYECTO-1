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
    [SerializeField] Button cerradura;
    [SerializeField] Button mancha;
    AudioSource audioSource;
  private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }
    public void BorrarMancha()
    {
        Debug.Log("Se hizo clic en BorrarMancha");
        if (!puerta.ManchaBorrada)
        {
            if (puerta.InteractableDataTrapo.CheckItemRequirement(puerta.InteractableDataTrapo.requiredItemID))
            {
                puerta.ManchaBorrada = true;
                puerta.QuitarItem(puerta.InteractableDataTrapo.requiredItemID);
                mancha.gameObject.SetActive(false);
                cerradura.gameObject.SetActive(true);
                puertaImagen.sprite = limpio;
                audioSource.clip = trapo;
                audioSource.Play();
            }
        }
    }
    public void AbrirPuerta()
    {
        Debug.Log("Se hizo clic en AbrirPuerta");
        if (!puerta.PuertaAbierta && puerta.ManchaBorrada)
        {
            StartCoroutine(CRAbrirPuerta());
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

    IEnumerator CRAbrirPuerta ()
    {
        if (!puerta.PuertaAbierta && puerta.ManchaBorrada)
        {
            if (puerta.InteractableDataHorquilla.CheckItemRequirement(puerta.InteractableDataHorquilla.requiredItemID))
            {
                puertaImagen.sprite = abierto;
                audioSource.clip = hawktua;
                audioSource.Play();
                yield return new WaitForSecondsRealtime(1f);
                puerta.PuertaAbierta = true;
                puerta.QuitarItem(puerta.InteractableDataHorquilla.requiredItemID);
                gameObject.SetActive(false);



                

            }
        }
    }

}
