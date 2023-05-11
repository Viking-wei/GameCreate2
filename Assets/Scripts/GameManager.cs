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
    [HideInInspector]
    public Vector3 playerPosition=new Vector3(40,1.2f,0);
    
    private const int COFFEE_SCENE_INDEX = 0;
    private const int NIGHT_SCENE_INDEX = 1;
    private const int PLANE_SCENE_INDEX = 2;
    private const int NEWS_NUM=10;

    private Animator _transitionAnimator;

    //对话存储系统
    public Dictionary<string, DialogStorage> dialogStorageDictionary;
    //NPC名单
    public List<string>nameOfNpc;
    //好感度字典
    [HideInInspector]public Dictionary<string, int> _favoriabilityRate;
    //新闻字典（索引作为key）
    [HideInInspector]public bool[] NewsArray;
    //NPC对话索引记录
    [HideInInspector]public Dictionary<string, int> NpcDialogIndex;

    protected override void Awake() 
    {
        base.Awake();

        //initialize NPC basic favoriabilityRate[0,100]
        _favoriabilityRate=new Dictionary<string, int>();
        foreach(string name in nameOfNpc)
        {
            _favoriabilityRate.Add(name,50);
        }

        NewsArray=new bool[NEWS_NUM];

        NpcDialogIndex=new Dictionary<string, int>();

        SceneManager.activeSceneChanged+=FindFadedCanvas;
    }

    private void FindFadedCanvas(Scene current, Scene next)
    {
        _transitionAnimator=GameObject.Find("ScenesChangeTransition").GetComponent<Animator>();

        if(_transitionAnimator==null)
        {
            Debug.LogError("Can't find transition animator");
        }
    }

#region Change Scene APIs
    /// <summary>Enter Plane Scene</summary>
    public void EnterPlane()
    {
        StartCoroutine(LoadScene(PLANE_SCENE_INDEX));
    }
    /// <summary>Enter Coffee Scene</summary>
    public void EnterCoffee()
    {
        StartCoroutine(LoadScene(COFFEE_SCENE_INDEX));
    }
    /// <summary>Enter Night Scene</summary>
    public void ExitToNight()
    {
        StartCoroutine(LoadScene(NIGHT_SCENE_INDEX));
    }
#endregion

    //Scene load
    private IEnumerator LoadScene(int targetSceneIndex)
    {
        //Save player position if current scene is coffee scene
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.buildIndex==NIGHT_SCENE_INDEX)
        {
            playerPosition=GameObject.Find("Player").transform.position;
            Debug.Log(playerPosition.ToString());
        }

        Debug.Log("Loading...");
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetSceneIndex, LoadSceneMode.Single);

        operation.allowSceneActivation = false;

        //Set transition animator
        _transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

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
