using UnityEngine;

public class InteractableNPCMaletin : MonoBehaviour, IInteractable {
    private InteractableData interactableData; 
    private void Start()
    {
        interactableData = GetComponent<InteractableData>();
    }
    public void OnClickAction()
    {
    }
 }
   