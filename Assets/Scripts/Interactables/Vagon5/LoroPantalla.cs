using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoroPantalla : MonoBehaviour {
    [SerializeField] CameraController cameraController;
    [SerializeField] InteractableLoro interactableLoro;
    [SerializeField] Image loroImage;
    [SerializeField] Sprite cariciaSprite;
    [SerializeField] Sprite darCartaSprite;
    [SerializeField] Sprite darPlumaSprite;

     float plumaAnimationDuration = 1f;
     float cariciaAnimationDuration = 0.5f;
     float cartaAnimationDuration = 1f;
    float cariciaResetTime = 3f;

    private bool isInteracting = false;
    private Coroutine resetCariciasCoroutine;

    public void Acariciar()
    {
        if (!isInteracting)
        {
            StartCoroutine(AcariciarCR());
        }
    }
    void OnDisable()
    {
        if (cameraController != null)
            cameraController.ToggleCameraControl(true);
        PlayerController.Instance.TogglePlayerControl(true);
        GameManager.Instance.GestionarCambioRaton(true);
    }
    void OnEnable()
    {
        if (cameraController != null)
            cameraController.ToggleCameraControl(false);
        PlayerController.Instance.TogglePlayerControl(false);
        GameManager.Instance.GestionarCambioRaton(false);
    }
    IEnumerator AcariciarCR()
    {
        isInteracting = true;
        Sprite originalSprite = loroImage.sprite;

        try
        {
            // COMIDA
            if (interactableLoro.TieneItem() && !interactableLoro.InventoryHasPluma)
            {
                loroImage.sprite = darPlumaSprite;
                interactableLoro.GivePluma();
                interactableLoro.QuitarItem(interactableLoro.InteractableData.requiredItemID);
                yield return new WaitForSeconds(plumaAnimationDuration);
                loroImage.sprite = originalSprite;
            }

            //ACARICIAR 
            loroImage.sprite = cariciaSprite;
            interactableLoro.NumeroCaricias++; 

            // Reiniciar temporizador de caricias
            if (resetCariciasCoroutine != null) StopCoroutine(resetCariciasCoroutine);
            resetCariciasCoroutine = StartCoroutine(ResetCariciasTimer());

            yield return new WaitForSeconds(cariciaAnimationDuration);
            loroImage.sprite = originalSprite;

            //  CARTA 
            if (!interactableLoro.InventoryHasCarta && interactableLoro.NumeroCaricias >= 3)
            {
                loroImage.sprite = darCartaSprite;
                interactableLoro.GiveCarta();
                yield return new WaitForSeconds(cartaAnimationDuration);
                loroImage.sprite = originalSprite;
               
            }
        }
        finally
        {
            isInteracting = false;
        }
    }

    IEnumerator ResetCariciasTimer()
    {
        yield return new WaitForSeconds(cariciaResetTime);
        interactableLoro.NumeroCaricias = 0;
    }
}