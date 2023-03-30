using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;
public class GameManager : Singleton<GameManager>
{

    [HideInInspector] public bool isChatting;
    [HideInInspector] public bool isPartEnd;
    public int theIndexOfDialog;

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
