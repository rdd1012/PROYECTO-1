using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour {
    [SerializeField] private List<Transform> interactables;
    public List<Transform> Interactables { get => interactables; }
    private Camera mainCamera;

    private Dictionary<Transform, Vector3> interactableWorldPositions = new Dictionary<Transform, Vector3>();

    public static Action<Transform> AddToInteractablesEvent;
    public static Action<Transform> RemoveFromInteractablesEvent;

    private void Awake()
    {
        AddToInteractablesEvent += AddToListOfInteractables;
        RemoveFromInteractablesEvent += RemoveFromListOfInteractables;
    }

    private void OnDestroy()
    {
        AddToInteractablesEvent -= AddToListOfInteractables;
        RemoveFromInteractablesEvent -= RemoveFromListOfInteractables;
    }

    private void RemoveFromListOfInteractables(Transform transformToRemove)
    {
        interactables.Remove(transformToRemove);
        interactableWorldPositions.Remove(transformToRemove);
    }

    private void AddToListOfInteractables(Transform transformToAdd)
    {
        interactables.Add(transformToAdd);
        interactableWorldPositions[transformToAdd] = transformToAdd.position;
    }

    void Start()
    {
        mainCamera = Camera.main;
        foreach (var interactable in interactables)
        {
            interactableWorldPositions[interactable] = interactable.position;
        }
    }

    void Update()
    {
        UpdateInteractablesScreenPositions();
    }

    private void UpdateInteractablesScreenPositions()
    {
        foreach (var interactable in interactables)
        {
            if (interactableWorldPositions.TryGetValue(interactable, out Vector3 worldPos))
            {
                // Update screen position based on the original world position
                Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);
                interactable.position = screenPos;
            }
        }
    }
}