using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCortina : MonoBehaviour,IInteractable
{
    [SerializeField] AudioClip sonidoAbrir;
    [SerializeField] AudioClip sonidoCerrar;
    private bool isOpen=false;
    public void OnClickAction()
    {
        UtilizarPersiana();
    }
    private void AbrirPersiana() { }
    private void CerrarPersiana() { }
    private void UtilizarPersiana() { if (isOpen) CerrarPersiana(); else AbrirPersiana(); }
}
