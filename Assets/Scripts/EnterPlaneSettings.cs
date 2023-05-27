using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class EnterPlaneSettings : MonoBehaviour
{
    [Header("BasicSettings")]
    [Range(2,5)]
    public int TargetPlaneIndex;
}