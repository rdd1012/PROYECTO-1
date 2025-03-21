using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField] private Transform goToPoint;
    public Transform GoToPoint => goToPoint;
}
