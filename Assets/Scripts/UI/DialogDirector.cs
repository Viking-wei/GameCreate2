using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogDirector : MonoBehaviour
{
    private Image dialogBackground;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI dialogText;
    private GameObject selector2;
    private TextMeshProUGUI[] s2Branch;
    private GameObject selector3;
    private TextMeshProUGUI[] s3Branch;

    private const string PlayerName = "我";
    private DialogStorage _wholeDialogInfo;
    private Vector3 _playerDialogPoint;
    private Vector3 _npcDialogPoint;
    private Dictionary<string, int> _npcDialogIndex;
    private int[] _branchIndexList;
    private string _npcName;
    private string _currentDialogText;
    private bool _isPartEnd;
    private bool _isInBranches;
    private bool _endDialog;


    private void Awake()
    {
        s2Branch=new TextMeshProUGUI[2];
        s3Branch=new TextMeshProUGUI[3];

        Debug.Log("Awake 函数执行了");

        dialogBackground = transform.GetChild(0).GetComponent<Image>();
        nameText = dialogBackground.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogText = dialogBackground.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        selector2 = transform.GetChild(1).gameObject;
        for (int i = 0; i < selector2.transform.childCount; i++)
            s2Branch[i] = selector2.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();

        selector3 = transform.GetChild(2).gameObject;
        for (int i = 0; i < selector2.transform.childCount; i++)
            s3Branch[i] = selector2.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();

        _branchIndexList = new int[3];
        _npcDialogIndex = new Dictionary<string, int>();

        CharaController.InvokeChat += StartDialog;
    }

    private void Start() 
    {
        _npcDialogIndex =GameManager.Instance.NpcDialogIndex;
    }

    public void Onclick(InputAction.CallbackContext context)
    {
        if (context.started && !_isInBranches)
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
                if (_isPartEnd)
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

        if (_wholeDialogInfo.thePara[_npcDialogIndex[_npcName]].haveBranch)
        {
            ProcessBranches();
        }
        else
            NextDialog();

    }

    private void NextDialog()
    {
        if (_wholeDialogInfo.thePara[_npcDialogIndex[_npcName]].haveBranch)
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
        _isInBranches = true;

        int branchNum = _wholeDialogInfo.thePara[_npcDialogIndex[_npcName]].branches.Count;

        if (branchNum == 2)
        {
            for (int i = 0; i < branchNum; i++)
            {
                s2Branch[i].text = _wholeDialogInfo.thePara[_npcDialogIndex[_npcName]].branches[i].dialog;
                _branchIndexList[i] = _wholeDialogInfo.thePara[_npcDialogIndex[_npcName]].branches[i].jumpID;
            }
            selector2.transform.position = _playerDialogPoint;
            selector2.SetActive(true);
            selector3.SetActive(false);

        }
        else if (branchNum == 3)
        {
            for (int i = 0; i < branchNum; i++)
            {
                s3Branch[i].text = _wholeDialogInfo.thePara[_npcDialogIndex[_npcName]].branches[i].dialog;
                _branchIndexList[i] = _wholeDialogInfo.thePara[_npcDialogIndex[_npcName]].branches[i].jumpID;
            }
            selector3.transform.position = _playerDialogPoint;
            selector2.SetActive(false);
            selector3.SetActive(true);
        }
        else
            Debug.LogWarning("Configure error  "+branchNum.ToString());
    }



    private IEnumerator ShowWordSlow(string content)
    {
        //Debug.Log("show slow");
        _isPartEnd = false;
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
        _isPartEnd = false;
        StopAllCoroutines();

        dialogText.text = content;
        MopUp();
    }

    private void InitialDialogData(Vector3 playerDialogPoint, Vector3 npcDialogPoint, DialogStorage dialogStorage)
    {
        _wholeDialogInfo = dialogStorage;
        _npcName = _wholeDialogInfo.theNpcName;

        _npcDialogIndex.TryAdd(_npcName, 0);

        _endDialog = false;
        _isPartEnd = false;
        _isInBranches = false;

        _playerDialogPoint = Camera.main.WorldToScreenPoint(playerDialogPoint);
        _npcDialogPoint = Camera.main.WorldToScreenPoint(npcDialogPoint);

    }

    private void FillRequisiteText()
    {
        _currentDialogText = _wholeDialogInfo.thePara[_npcDialogIndex[_npcName]].branches[0].dialog;

        if (_wholeDialogInfo.thePara[_npcDialogIndex[_npcName]].isPlayer)
            nameText.text = PlayerName;
        else
            nameText.text = _npcName;

    }

    private void SetDialogImagePosition()
    {
        if (_wholeDialogInfo.thePara[_npcDialogIndex[_npcName]].isPlayer)
            dialogBackground.transform.position = _playerDialogPoint;
        else
            dialogBackground.transform.position = _npcDialogPoint;
    }

    private void MopUp()
    {
        var jID = _wholeDialogInfo.thePara[_npcDialogIndex[_npcName]].branches[0].jumpID;
        if (jID == 0)
            _endDialog = true;
        else
        {
            _npcDialogIndex[_npcName] = jID;
            _npcDialogIndex[_wholeDialogInfo.theNpcName] = _npcDialogIndex[_npcName];
        }

        _isPartEnd = true;
    }

    public void OnButton0()
    {
        _npcDialogIndex[_npcName] = _branchIndexList[0];
        _isInBranches = false;
        selector2.SetActive(false);
        selector3.SetActive(false);
        NextDialog();
    }
    public void OnButton1()
    {
        _npcDialogIndex[_npcName] = _branchIndexList[1];
        _isInBranches = false;
        selector2.SetActive(false);
        selector3.SetActive(false);
        NextDialog();
    }
    public void OnButton2()
    {
        _npcDialogIndex[_npcName] = _branchIndexList[2];
        _isInBranches = false;
        selector2.SetActive(false);
        selector3.SetActive(false);
        NextDialog();
    }
}