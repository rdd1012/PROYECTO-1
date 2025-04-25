using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCortina : MonoBehaviour,IInteractable
{
    [SerializeField] AudioClip sonidoAbrir;
    [SerializeField] AudioClip sonidoCerrar;
    AudioSource audioSource;
    private bool isOpen=true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnClickAction()
    {
        UtilizarPersiana();
    }
    private void AbrirPersiana()
    {

        isOpen = true;

        audioSource.clip = sonidoAbrir;
    }
    private void CerrarPersiana()
    {

        isOpen = true;
        audioSource.clip = sonidoCerrar;
    }
    private void UtilizarPersiana()
    {
        if (isOpen) CerrarPersiana();
        else AbrirPersiana();
        audioSource.Play();
    }
}
