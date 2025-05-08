using UnityEngine;

public class InteractablePuertaCarga : MonoBehaviour, IInteractable {
    [SerializeField] private bool isLocked = true;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickAction()
    {
        if (!IsInteractable()) return; 

        GameManager.Instance.PasarDeNivel();
        audioSource.Play();
    }

    
    public bool IsInteractable()
    {
        return !isLocked; 
    }

    public void Unlock()
    {
        isLocked = false;
    }
}