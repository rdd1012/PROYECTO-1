using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCortina : MonoBehaviour,IInteractable
{
    [SerializeField] AudioClip sonidoAbrir;
    [SerializeField] AudioClip sonidoCerrar;
    [SerializeField] Sprite spriteCerrado;
    [SerializeField] Sprite spriteAbrir;
    private SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    private bool isOpen=true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void OnClickAction()
    {
        UtilizarPersiana();
    }
    private void AbrirPersiana()
    {

        isOpen = true;
        spriteRenderer.sprite = spriteAbrir;
        audioSource.clip = sonidoAbrir;
    }
    private void CerrarPersiana()
    {

        isOpen = false;
        spriteRenderer.sprite = spriteCerrado;
        audioSource.clip = sonidoCerrar;
    }
    private void UtilizarPersiana()
    {
        if (isOpen) CerrarPersiana();
        else AbrirPersiana();
        audioSource.Play();
    }
}
