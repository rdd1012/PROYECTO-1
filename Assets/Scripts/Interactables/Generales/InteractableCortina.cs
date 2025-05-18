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
    private bool isOpen;
    public bool IsOpen { get { return isOpen; } set { isOpen = value; } }
    private void Awake()
    {
        isOpen = true;
    }
    public bool TieneItem() { return true; }
    private void Start()
    {
        //sol = GetComponentInChildren<Light>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public bool IsInteractable() { return true; }
    public void OnClickAction()
    {
        Debug.Log("Interactable OnClickAction triggered");
        UtilizarPersiana();
    }
    private void AbrirPersiana()
    {

        isOpen = true;
        spriteRenderer.sprite = spriteAbrir;
        audioSource.clip = sonidoAbrir;
       // sol.gameObject.SetActive(true);
    }
    private void CerrarPersiana()
    {

        isOpen = false;
        spriteRenderer.sprite = spriteCerrado;
        audioSource.clip = sonidoCerrar;
        //sol.gameObject.SetActive(false);
    }
    private void UtilizarPersiana()
    {
        if (isOpen) CerrarPersiana();
        else AbrirPersiana();
        audioSource.Play();
    }
}
