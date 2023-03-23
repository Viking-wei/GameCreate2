using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MyGameManager : Singleton<MyGameManager>
{
    public GameObject character;
    //scene load
    public IEnumerator LoadScene(int targetSceneIndex, Vector3 targetDestination)
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
        character.transform.position=targetDestination;
    }

}
