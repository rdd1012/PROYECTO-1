using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoroPantalla : MonoBehaviour {
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

    IEnumerator AcariciarCR()
    {
        isInteracting = true;
        Sprite originalSprite = loroImage.sprite;

        try
        {
            //pluma
            if (!interactableLoro.InventoryHasPluma && interactableLoro.InteractableData.CheckItemRequirement())
            {
                if (!interactableLoro.TeniaObjeto)
                {
                    interactableLoro.QuitarItem(interactableLoro.InteractableData.requiredItemID);
                }

                loroImage.sprite = darPlumaSprite;
                interactableLoro.GivePluma();
                yield return new WaitForSeconds(plumaAnimationDuration);
                loroImage.sprite = originalSprite;
                yield break;
            }

            // acariciar
            loroImage.sprite = cariciaSprite;
            interactableLoro.NumeroCaricias++;

            
            if (resetCariciasCoroutine != null)
            {
                StopCoroutine(resetCariciasCoroutine);
            }
            resetCariciasCoroutine = StartCoroutine(ResetCariciasTimer());

            yield return new WaitForSeconds(cariciaAnimationDuration);
            loroImage.sprite = originalSprite;

            // carta
            if (!interactableLoro.InventoryHasCarta && interactableLoro.NumeroCaricias >= 3)
            {
                loroImage.sprite = darCartaSprite;
                interactableLoro.GiveCarta();
                yield return new WaitForSeconds(cartaAnimationDuration);
                loroImage.sprite = originalSprite;
                interactableLoro.NumeroCaricias = 0;

                
                if (resetCariciasCoroutine != null)
                {
                    StopCoroutine(resetCariciasCoroutine);
                }
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