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
    public Vector3 playerPosition=new Vector3(0,1.2f,-11.6f);

    public int Attack
    {
        get => _attack + parserCodeFragmentNum % 3;
    }

    public int Health
    {
        get => _health + encryptionAlgorithmCodeFragmentNum % 3;
    }
    
    //TODO:need to fix the initial value
    private readonly int _attack=3;
    private readonly int _health=3;
    
    [HideInInspector]public int parserCodeFragmentNum = 0;
    [HideInInspector]public int encryptionAlgorithmCodeFragmentNum = 0;
    
    private const int CoffeeSceneIndex = 0;
    private const int NightSceneIndex = 1;
    private const int NewsNum=13;

    public static int Paragraph=0;

    private Animator _transitionAnimator;

    //对话存储系统
    public Dictionary<string, DialogStorage> dialogStorageDictionary;
    //NPC名单
    public List<string>nameOfNpc;
    //NPC对话索引记录
    [HideInInspector]public Dictionary<string, int> NpcDialogIndex;
    [HideInInspector]public int newsNum = 0;
    [HideInInspector]public int fileNum = 0;

    protected override void Awake()
    {
        base.Awake();

        NpcDialogIndex=new Dictionary<string, int>();

        SceneManager.activeSceneChanged+=FindFadedCanvas;
    }

    private void FindFadedCanvas(Scene current, Scene next)
    {
        _transitionAnimator=GameObject.Find("ScenesChangeTransition").GetComponent<Animator>();

        if(next.buildIndex==CoffeeSceneIndex)
        {
            Paragraph++;
        }
    }

    #region Change Scene APIs
    /// <summary>Enter Plane Scene</summary>
    public void EnterPlane(int index)
    {
        StartCoroutine(LoadScene(index));
    }
    /// <summary>Enter Coffee Scene</summary>
    public void EnterCoffee()
    {
        StartCoroutine(LoadScene(CoffeeSceneIndex));
    }
    /// <summary>Enter Night Scene</summary>
    public void ExitToNight()
    {
        StartCoroutine(LoadScene(NightSceneIndex));
    }
#endregion

    //Scene load
    private IEnumerator LoadScene(int targetSceneIndex)
    {
        //Save player position if current scene is coffee scene
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.buildIndex==NightSceneIndex)
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
