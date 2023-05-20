using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton_2D<T> : MonoBehaviour where T : Component
{
 public static T Instance { get; private set; }

 protected virtual void Awake()
    {
        Instance=this as T;
    }
}
