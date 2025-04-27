using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCortina : MonoBehaviour,IInteractable
{
    [SerializeField] AudioClip sonidoAbrir;
    [SerializeField] AudioClip sonidoCerrar;
    AudioSource audioSource;
    Animator animator;
    SpriteRenderer spriteRenderer;
    [SerializeField]Sprite abierta;
    [SerializeField]Sprite cerrada;
    private bool isOpen=true;
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    public void OnClickAction()
    {
        UtilizarPersiana();
    }
    private void AbrirPersiana()
    {

        isOpen = true;
        animator.Play("ABRIR");
        spriteRenderer.sprite = abierta;
        audioSource.clip = sonidoAbrir;
    }
    private void CerrarPersiana()
    {

        isOpen = false;
        animator.Play("CERRAR");
        spriteRenderer.sprite = cerrada;
        audioSource.clip = sonidoCerrar;
    }
    private void UtilizarPersiana()
    {
        if (isOpen) CerrarPersiana();
        else AbrirPersiana();
        audioSource.Play();
    }
}
