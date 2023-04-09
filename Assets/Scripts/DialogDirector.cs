using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogDirector : Singleton<DialogDirector>
{
    [Required] public Image dialogBackground;
    [Required] public TextMeshProUGUI nameText;
    [Required] public TextMeshProUGUI dialogText;
    [Required] public GameObject selector2;
    [Required] public TextMeshProUGUI[] s2Branch;
    [Required] public GameObject selector3;
    [Required] public TextMeshProUGUI[] s3Branch;




    private DialogStorage _wholeDialogInfo;
    private Vector3 _playerDialogPoint;
    private Vector3 _npcDialogPoint;
    private int[] _branchIndexList;
    private int _currentIndex;
    private string _npcName;
    private string _currentDialogText;
    private bool _isInSelect;
    private bool _endDialog;
    private const string PlayerName = "PlayerName";

    protected override void Awake()
    {
        base.Awake();
        _branchIndexList = new int[3];
        CharaController.InvokeChat += StartDialog;
    }

    public void Onclick(InputAction.CallbackContext context)
    {
        if (context.started && !_isInSelect)
        {
            if (_endDialog)
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

    private void StartDialog(Vector3 playerDialogPoint, Vector3 npcDialogPoint, DialogStorage dialogStorage)
    {
        GameManager.Instance.isChatting = true;

        InitialDialogData(playerDialogPoint, npcDialogPoint, dialogStorage);

        if (_wholeDialogInfo.thePara[_currentIndex].haveBranch)
        {
            ProcessBranches();
        }
        else
            NextDialog();

    }

    private void NextDialog()
    {
        if (_wholeDialogInfo.thePara[_currentIndex].haveBranch)
        {
            ProcessBranches();
        }
        else
        {
            FillRequisiteText();

            SetDialogImagePosition();

            dialogBackground.gameObject.SetActive(true);

            StartCoroutine(ShowWordSlow(_currentDialogText));
        }
    }

    private void ProcessBranches()
    {
        dialogBackground.gameObject.SetActive(false);
        _isInSelect = true;

        int branchNum = _wholeDialogInfo.thePara[_currentIndex].branches.Count;

        if (branchNum == 2)
        {
            for (int i = 0; i < branchNum; i++)
            {
                s2Branch[i].text = _wholeDialogInfo.thePara[_currentIndex].branches[i].dialog;
                _branchIndexList[i] = _wholeDialogInfo.thePara[_currentIndex].branches[i].jumpID;
            }
            selector2.transform.position = _playerDialogPoint;
            selector2.SetActive(true);
            selector3.SetActive(false);

        }
        if (branchNum == 3)
        {
            for (int i = 0; i < branchNum; i++)
            {
                s3Branch[i].text = _wholeDialogInfo.thePara[_currentIndex].branches[i].dialog;
                _branchIndexList[i] = _wholeDialogInfo.thePara[_currentIndex].branches[i].jumpID;
            }
            selector3.transform.position = _playerDialogPoint;
            selector2.SetActive(false);
            selector3.SetActive(true);
        }
        else
            Debug.LogWarning("Configure error!!!");
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

    private void InitialDialogData(Vector3 playerDialogPoint, Vector3 npcDialogPoint, DialogStorage dialogStorage)
    {
        _currentIndex = 0;
        _playerDialogPoint = Camera.main.WorldToScreenPoint(playerDialogPoint);
        _npcDialogPoint = Camera.main.WorldToScreenPoint(npcDialogPoint);
        _wholeDialogInfo = dialogStorage;
        _npcName = dialogStorage.theNpcName;
    }

    private void FillRequisiteText()
    {
        _currentDialogText = _wholeDialogInfo.thePara[_currentIndex].branches[0].dialog;

        if (_wholeDialogInfo.thePara[_currentIndex].isPlayer)
            nameText.text = PlayerName;
        else
            nameText.text = _npcName;

    }

    private void SetDialogImagePosition()
    {
        if (_wholeDialogInfo.thePara[_currentIndex].isPlayer)
            dialogBackground.transform.position = _playerDialogPoint;
        else
            dialogBackground.transform.position = _npcDialogPoint;
    }

    private void MopUp()
    {
        var jID = _wholeDialogInfo.thePara[_currentIndex].branches[0].jumpID;
        if (jID == 0)
            _endDialog = true;
        else
            _currentIndex = jID;

        GameManager.Instance.isPartEnd = true;
    }

    public void OnButton0()
    {
        _currentIndex = _branchIndexList[0];
        _isInSelect = false;
        selector2.SetActive(false);
        selector3.SetActive(false);
        NextDialog();
    }
    public void OnButton1()
    {
        _currentIndex = _branchIndexList[1];
        _isInSelect = false;
        selector2.SetActive(false);
        selector3.SetActive(false);
        NextDialog();
    }
    public void OnButton2()
    {
        _currentIndex = _branchIndexList[2];
        _isInSelect = false;
        selector2.SetActive(false);
        selector3.SetActive(false);
        NextDialog();
    }
}
