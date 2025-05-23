using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuertaHorquilla : MonoBehaviour, IInteractable {
    
    [SerializeField] private PuertaPantalla puertaPantalla;
    [SerializeField] private InteractableData interactableDataTrapo;
    [SerializeField] private InteractableData interactableDataHorquilla;

    private bool manchaBorrada = false;
    private bool puertaAbierta = false;

    AudioSource audioSource;
    public bool ManchaBorrada { get => manchaBorrada; set => manchaBorrada = value; }
    public bool PuertaAbierta { get => puertaAbierta; set => puertaAbierta = value; }
    public InteractableData InteractableDataTrapo => interactableDataTrapo;
    public InteractableData InteractableDataHorquilla => interactableDataHorquilla;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public bool TieneItem() { return true; }
    public void OnClickAction()
    {
        if (puertaAbierta && manchaBorrada)
        {
            GameManager.Instance.PasarDeNivel();
            audioSource.Play();
            return; 
        }

        if (!puertaAbierta || !manchaBorrada)
        {
            puertaPantalla.gameObject.SetActive(true);
        }
    }
    public bool IsInteractable() { return true; }
    
    public void QuitarItem(int itemID)
    {
        InventoryManager.Instance?.DecrementarUsos(itemID);
    }
}