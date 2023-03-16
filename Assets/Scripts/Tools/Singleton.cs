using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:Component
{
    private static T _instance;
    public static T Instance => _instance;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        if (_instance == null)
            _instance = this as T;
        else
            Destroy(this.gameObject);
    }
}

