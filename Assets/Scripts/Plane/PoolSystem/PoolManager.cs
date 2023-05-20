using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] playerProjectilePools;

    static Dictionary<GameObject, Pool> dictionary;
    private void Start()
    {
        dictionary = new Dictionary<GameObject, Pool>();
        Initialize(playerProjectilePools);
    }
#if UNITY_EDITOR
    private void OnDestroy()
    {
        CheakPoolSize(playerProjectilePools);
    }
#endif
    void CheakPoolSize(Pool[]pools)
    {
        foreach(var pool in pools)
        {
            if(pool.RuntimeSize>pool.Size)
            {
                Debug.LogWarning(string.Format("Pool:{0} has a runtime size{1}bigger than its initial size{2}!",
                    pool.Prefab.name,pool.RuntimeSize,pool.Size));
            }
        }
    }
    void Initialize(Pool[]pools)
    {
        foreach(var pool in pools)
        {
#if UNITY_EDITOR
            if(dictionary.ContainsKey(pool.Prefab))
            {
                Debug.LogError("Same prefab in multiple pools! Prefab:"+pool.Prefab.name);
                continue;
            }
#endif
            dictionary.Add(pool.Prefab,pool);
            Transform poolParent=  new GameObject("Pool:" + pool.Prefab.name).transform;
            poolParent.parent = transform;
            pool.Initialize(poolParent); 
        }
    }
    ///
    public static GameObject Release(GameObject prefab)
    {
#if UNITY_EDITOR
        if(!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool manager can NOT find  Prefab:" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject();
    }
    public static GameObject Release(GameObject prefab,Vector3 position)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool manager can NOT find  Prefab:" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position);
    }
    public static GameObject Release(GameObject prefab, Vector3 position,Quaternion rotation)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool manager can NOT find  Prefab:" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position,rotation);
    }
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation,Vector3 localScale)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool manager can NOT find  Prefab:" + prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position, rotation,localScale);
    }
}
