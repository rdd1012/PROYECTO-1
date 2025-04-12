using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableData : MonoBehaviour
{
    [SerializeField] private Transform goToPoint;
    public Transform GoToPoint => goToPoint;
    public int requiredItemID = -1; // -1 = no requiere item
}
