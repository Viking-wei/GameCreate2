using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CoffeeTalkController : MonoBehaviour
{
    [Header("Dialog UI")]
    public Image dialogBackground;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public GameObject selector2;
    public TextMeshProUGUI[] s2Branch;
    public GameObject selector3;
    public TextMeshProUGUI[] s3Branch;
    [Header("Dialog Point")]
    public GameObject playerDialogPoint;
    public GameObject npcDialogPoint;
    [Header("Input Setting")]
    public PlayerInput playerInput;
    [Header("Coffee Result")]
    public CoffeeMakerController coffeeMakerController;

    const string PlayerName = "大卫";
    private DialogTextRepository _dialogTextRepository;
    [SerializeField]
    private int _currentID = 0;
    private bool _isInBranches = false;
    private bool _endDialog = false;
    private bool _isPartEnd = false;
    private Vector3 _playerDialogPoint;
    private Vector3 _npcDialogPoint;
    private string _currentDialogText;

    private void Start()
    {
        //FIXME: This is a temporary solution to add dialog data
        coffeeMakerController.coffeeResultDelegate+=ProcessCoffeeResult;
        _dialogTextRepository = DialogText.dialogTextRepository[0];
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (context.started && !_isInBranches)
        {

            if (_endDialog)
            {
                dialogBackground.gameObject.SetActive(false);
                return;
            }

            if (_isPartEnd)
                NextDialog();
            else
                ShowWordImmediately(_currentDialogText);
        }
    }

    public void StartDialog()
    {
        playerInput.SwitchCurrentActionMap("Dialog");

        InitialDialogData(playerDialogPoint.transform.position, npcDialogPoint.transform.position);

        NextDialog();
    }

    private void NextDialog()
    {
        if ((_currentID - 1) > 0)
        {
            ProcessExtendInfo(_dialogTextRepository.Data[_currentID - 1].ExtendInfo);
        }

        FillRequisiteText();

        SetDialogImagePosition();

        dialogBackground.gameObject.SetActive(true);

        StartCoroutine(ShowWordSlow(_currentDialogText));

    }

    // private void ProcessBranches()
    // {
    //     dialogBackground.gameObject.SetActive(false);
    //     _isInBranches = true;

    //     int branchNum = _dialogTextRepository.Data[_currentID].BranchesNum;
    //     int baseID = _dialogTextRepository.Data[_currentID].JumpID;

    //     if (branchNum == 2)
    //     {
    //         for (int i = 0; i < branchNum; i++)
    //         {
    //             s2Branch[i].text = _dialogTextRepository.Data[baseID + i].Content;
    //         }
    //         selector2.SetActive(true);
    //         selector3.SetActive(false);
    //     }
    //     else if (branchNum == 3)
    //     {
    //         for (int i = 0; i < branchNum; i++)
    //         {
    //             s3Branch[i].text = _dialogTextRepository.Data[baseID + i].Content;
    //         }
    //         selector2.SetActive(false);
    //         selector3.SetActive(true);
    //     }
    //     else
    //         Debug.LogWarning($"Configure error of {_currentID} ");
    // }

    private IEnumerator ShowWordSlow(string content)
    {
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
        _isPartEnd = false;
        StopAllCoroutines();

        dialogText.text = content;

        MopUp();
    }

    private void InitialDialogData(Vector3 playerDialogPoint, Vector3 npcDialogPoint)
    {
        _endDialog = false;
        _isPartEnd = false;
        _isInBranches = false;

        _playerDialogPoint = Camera.main.WorldToScreenPoint(playerDialogPoint);
        _npcDialogPoint = Camera.main.WorldToScreenPoint(npcDialogPoint);
    }

    private void FillRequisiteText()
    {
        _currentDialogText = _dialogTextRepository.Data[_currentID].Content;

        nameText.text = _dialogTextRepository.Data[_currentID].Name;
    }

    private void SetDialogImagePosition()
    {
        if (_dialogTextRepository.Data[_currentID].Name == PlayerName)
            dialogBackground.transform.position = _playerDialogPoint;
        else
            dialogBackground.transform.position = _npcDialogPoint;
    }

    private void MopUp()
    {
        var jID = _dialogTextRepository.Data[_currentID].JumpID;
        if (jID == -1)
            _endDialog = true;
        else
            _currentID = jID;

        _isPartEnd = true;
    }
    public void SelectBranches(int buttonIndex)
    {
        _currentID = _dialogTextRepository.Data[_currentID + buttonIndex].JumpID;
        _isInBranches = false;
        selector2.SetActive(false);
        selector3.SetActive(false);
        NextDialog();
    }

    private void ProcessCoffeeResult(CoffeeResult coffeeResult)
    {
        //FIXME: This is a temporary solution to add dialog data
        switch (coffeeResult)
        {
            case CoffeeResult.Good:
                _currentID = _dialogTextRepository.Data[_currentID].JumpID;
                break;
            case CoffeeResult.Bad:
                _currentID = _dialogTextRepository.Data[_currentID].JumpID + 1;
                break;
            case CoffeeResult.Normal:
                _currentID = _dialogTextRepository.Data[_currentID].JumpID + 2;
                break;
            default:
                Debug.LogWarning($"CoffeeResult error of {_currentID} ");
                break;
        }
        StartDialog();
    }

    private void ProcessExtendInfo(int extendInfo)
    {   //解锁线索
        if(Convert.ToBoolean(extendInfo&1))
        {
            Debug.Log("解锁线索");
        }
        //解锁文件
        if(Convert.ToBoolean(extendInfo&2))
        {
            Debug.Log("解锁文件");
        }
        //制作咖啡
        if(Convert.ToBoolean(extendInfo&4))
        {
            playerInput.SwitchCurrentActionMap("Play");
            coffeeMakerController._playableDirector.Play();
        }
        //分支对话
        if(Convert.ToBoolean(extendInfo&8))
        {
            Debug.Log("分支对话");
        }
    }
}
