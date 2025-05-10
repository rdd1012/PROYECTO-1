using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SillonSucioPantalla : MonoBehaviour
{
    [SerializeField]InteractableSillonSucio sillon;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void BorrarMancha(Image image)
    {
        if (sillon.InteractableData.CheckItemRequirement())
        {
            sillon.QuitarItem(sillon.InteractableData.requiredItemID);
            image.gameObject.SetActive(false);
            audioSource.Play();
            //GameManager.Instance.SetCursorDefault();
        }

    }

    public void SalirPantalla()
    {
        this.gameObject.SetActive(false);
    }
}
