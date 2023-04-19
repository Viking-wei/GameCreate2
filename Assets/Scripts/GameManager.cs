using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[ShowOdinSerializedPropertiesInInspector]
public class GameManager : Singleton<GameManager>, ISerializationCallbackReceiver, ISupportsPrefabSerialization
{

    [HideInInspector] public bool isChatting;
    private const int COFFEE_SCENE_INDEX = 0;
    private const int NIGHT_SCENE_INDEX = 1;
    private const int PLANE_SCENE_INDEX = 2;


    public Dictionary<string, DialogStorage> dialogStorageDictionary;
    public List<string>nameOfNpc;
    [HideInInspector]public Dictionary<string, int> _favoriabilityRate;

    protected override void Awake() 
    {
        base.Awake();

        //initialize NPC basic favoriabilityRate[0,100]
        foreach(string name in nameOfNpc)
        {
            _favoriabilityRate.Add(name,50);
        }
    }

#region Change Scene APIs
    public void EnterPlane()
    {
        StartCoroutine(LoadScene(PLANE_SCENE_INDEX));
    }
    public void EnterCoffee()
    {
        StartCoroutine(LoadScene(COFFEE_SCENE_INDEX));
    }
    public void ExitToNight()
    {
        StartCoroutine(LoadScene(NIGHT_SCENE_INDEX));
    }
#endregion

    //Scene load
    private IEnumerator LoadScene(int targetSceneIndex)
    {
        Debug.Log("Loading...");
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetSceneIndex, LoadSceneMode.Single);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

#region Serialize Dictionary
    [SerializeField, HideInInspector]
    private SerializationData serializationData;
    SerializationData ISupportsPrefabSerialization.SerializationData { get { return this.serializationData; } set { this.serializationData = value; } }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        UnitySerializationUtility.DeserializeUnityObject(this, ref this.serializationData);
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        UnitySerializationUtility.SerializeUnityObject(this, ref this.serializationData);
    }
#endregion

}
