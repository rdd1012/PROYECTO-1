using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    [SerializeField] private List<Transform> interactables;
    public List<Transform> Interactables { get => interactables; }
    private Camera mainCamera;

    public static Action<Transform> AddToInteractablesEvent;
    public static Action<Transform> RemoveFromInteractablesEvent;

    private void Awake()
    {
        AddToInteractablesEvent += AddToListOfInteractables;
        RemoveFromInteractablesEvent += RemoveFromListOfInteractables;
    }

    private void RemoveFromListOfInteractables(Transform transformRemoveFromList)
    {
        interactables.Remove(transformRemoveFromList);
    }
    private void AddToListOfInteractables(Transform transformAddToList)
    {
        interactables.Add(transformAddToList);
    }

    void Start()
    {
        mainCamera = Camera.main;
        AllChildrenWorldToScreenPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void AllChildrenWorldToScreenPoint() {
        for (int i=0; i< this.transform.childCount;i++) 
        {
            transform.GetChild(i).position = mainCamera.WorldToScreenPoint(transform.GetChild(i).position);

        }
    }
}
