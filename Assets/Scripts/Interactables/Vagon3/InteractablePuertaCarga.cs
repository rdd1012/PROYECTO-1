using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuertaCarga : MonoBehaviour,IInteractable
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickAction()
    {
        GameManager.Instance.PasarDeNivel();
        audioSource.Play();

    }
}
