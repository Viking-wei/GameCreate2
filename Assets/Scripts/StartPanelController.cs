using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPanelController : MonoBehaviour
{

    public void StartGame()
    {
        StartCoroutine(ProcessSceneChange());
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private IEnumerator ProcessSceneChange()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
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
