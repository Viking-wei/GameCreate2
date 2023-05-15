using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ObjectPool
{
    private static ObjectPool instance;
    private Dictionary<string, Queue<GameObject>> objectpool = new Dictionary<string, Queue<GameObject>>();
    private GameObject pool;
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }
    public GameObject GetObject(GameObject prefab)
    {
        GameObject _object;
        if(!objectpool.ContainsKey(prefab.name)||objectpool[prefab.name].Count==0) 
        {
            _object =GameObject.Instantiate(prefab);
            PushObject(_object);
            if(pool==null)
                pool = new GameObject("ObjectPool");
            GameObject child =GameObject.Find(prefab.name);
            if(!child)
            {
                child=new GameObject(prefab.name);
                child.transform.SetParent(pool.transform);
            }
            _object.transform.SetParent(child.transform);
        }
        _object = objectpool[prefab.name].Dequeue();
        _object.SetActive(true);
        return _object;
    }


    public void PushObject(GameObject prefab)
    {
        string _name =prefab.name.Replace("(Clone)",string.Empty);
        if (!objectpool.ContainsKey(_name))
            objectpool.Add(_name, new Queue<GameObject>());
        objectpool[_name].Enqueue(prefab);
        prefab.SetActive(false);
    }
}

    
