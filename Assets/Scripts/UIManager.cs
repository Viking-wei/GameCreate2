using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
     public Transform ShowPoint;
     public GameObject Prompt;
     private TextMeshProUGUI _promptText;

     private void Awake() 
     {
          
          _promptText=Prompt.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

          CharaController.ShowPrompt+=ShowPromt;
          CharaController.ClosePrompt+=ClosePrompt;

          if(_promptText==null)
               Debug.LogWarning("_promptText have not been found");
     }
     private void Update() 
     {
          if(Prompt.activeSelf)
               Prompt.transform.position=Camera.main.WorldToScreenPoint(ShowPoint.transform.position);
     }

     private void ShowPromt(string key)
     {
          _promptText.text=key;
          Prompt.SetActive(true);
     }
     private void ClosePrompt()
     {
          Prompt.SetActive(false);
     }
}
