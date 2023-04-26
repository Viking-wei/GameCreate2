using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class UIManager : MonoBehaviour
{
    [Required] public GameObject Prompt;

    private Vector3 _showPoint;
    private TextMeshProUGUI _promptText;
    private const float LerpRate = 0.5f;

    private void Awake()
    {
        CharaController.ShowPrompt += ShowPromt;
        CharaController.ClosePrompt += ClosePrompt;

        var ShowPrompt = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1);

        _showPoint = Camera.main.WorldToScreenPoint(ShowPrompt.position);

        Prompt.transform.position = _showPoint;

        _promptText = Prompt.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (_promptText == null)
            Debug.LogWarning("_promptText have not been found");
    }


    private void ShowPromt(string key)
    {
        _promptText.text = key;

        if (Prompt != null)
            Prompt.SetActive(true);
        else
            Debug.Log("Prompt is null");
    }
    private void ClosePrompt()
    {
        if (Prompt != null)
            Prompt.SetActive(false);
        else
            Debug.Log("Prompt is null");
    }
    
}
