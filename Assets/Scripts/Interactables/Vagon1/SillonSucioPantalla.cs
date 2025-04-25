using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SillonSucioPantalla : MonoBehaviour
{
    [SerializeField]InteractableSillonSucio sillon;
    public void BorrarMancha(Image image)
    {
        if (sillon.InteractableData.CheckItemRequirement())
        {
            sillon.QuitarItem(sillon.InteractableData.requiredItemID);
            image.gameObject.SetActive(false);
            GameManager.Instance.SetCursorDefault();
        }

    }

    public void SalirPantalla()
    {
        this.gameObject.SetActive(false);
    }
}
