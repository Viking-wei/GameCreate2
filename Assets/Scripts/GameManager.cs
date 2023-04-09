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
    [HideInInspector] public bool isPartEnd;

    // [OdinSerialize]
    public Dictionary<string,DialogStorage> dialogStorageDictionary;

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

    //scene load
    public IEnumerator LoadScene(int targetSceneIndex)
    {
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

}
