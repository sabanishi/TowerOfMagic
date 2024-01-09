using System;
using UnityEngine;

public abstract class ManagerInterface : MonoBehaviour
{
    [SerializeField] protected MenuManager parent;
    public abstract void Act();
}
