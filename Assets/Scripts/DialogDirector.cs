using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogDirector : Singleton<DialogDirector>
{
    [Required] public DialogStorage wholeDialogInfo;
    [Required] public Image dialogBackground;
    [Required] public TextMeshProUGUI dialogText;
    string _currentDialogText;
    Vector3 _playerDialogPoint;
    Vector3 _npcDialogPoint;
    bool _isNeedEndDialog;

    protected override void Awake()
    {
        base.Awake();
        CharaController.InvokeChat += StartDialog;
    }

    public void Onclick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_isNeedEndDialog)
            {
                GameManager.Instance.isChatting = false;
                dialogBackground.gameObject.SetActive(false);
                return;
            }

            if (GameManager.Instance.isChatting)
            {
                //Debug.Log("Click");
                if (GameManager.Instance.isPartEnd)
                    NextDialog();
                else
                    ShowWordImmediately(_currentDialogText);
            }
        }
    }

    private void StartDialog(Vector3 playerDialogPoint, Vector3 npcDialogPoint)
    {
        GameManager.Instance.isChatting = true;

        _playerDialogPoint = playerDialogPoint;
        _npcDialogPoint = npcDialogPoint;
        _currentDialogText = wholeDialogInfo.thePara[GameManager.Instance.theIndexOfDialog].dialog;

        if (wholeDialogInfo.thePara[GameManager.Instance.theIndexOfDialog].isNpc)
            dialogBackground.transform.position = _npcDialogPoint;
        else
            dialogBackground.transform.position = _playerDialogPoint;

        dialogBackground.gameObject.SetActive(true);

        StartCoroutine(ShowWordSlow(_currentDialogText));
    }

    private void NextDialog()
    {
        _currentDialogText = wholeDialogInfo.thePara[GameManager.Instance.theIndexOfDialog].dialog;

        if (wholeDialogInfo.thePara[GameManager.Instance.theIndexOfDialog].isNpc)
            dialogBackground.transform.position = _npcDialogPoint;
        else
            dialogBackground.transform.position = _playerDialogPoint;

        StartCoroutine(ShowWordSlow(_currentDialogText));
    }

    private IEnumerator ShowWordSlow(string content)
    {
        //Debug.Log("show slow");
        GameManager.Instance.isPartEnd = false;
        string s = "";
        int theCharLength = content.Length, theCharIndex = 0;

        while (theCharIndex < theCharLength)
        {
            s += content[theCharIndex];
            dialogText.text = s;
            theCharIndex++;
            yield return new WaitForSeconds(0.05f);
        }
        MopUp();
    }
    private void ShowWordImmediately(string content)
    {
        //Debug.Log("show Immediately");
        GameManager.Instance.isPartEnd = false;
        StopAllCoroutines();

        dialogText.text = content;
        MopUp();
    }
    private void MopUp()
    {
        _isNeedEndDialog = wholeDialogInfo.thePara[GameManager.Instance.theIndexOfDialog].isEndSentence;
        GameManager.Instance.isPartEnd = true;
        GameManager.Instance.theIndexOfDialog++;
    }

}
