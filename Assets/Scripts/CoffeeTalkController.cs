using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
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

    const string Path = "\\DialogInfo\\TestTalk.json";
    const string PlayerName = "Player";
    private DialogTextRepository _dialogTextRepository;
    [SerializeField]
    private int _currentID = 0;
    private bool _isInBranches = false;
    private bool _endDialog = false;
    private bool _isPartEnd = false;
    private Vector3 _playerDialogPoint;
    private Vector3 _npcDialogPoint;
    private string _currentDialogText;
    private void Awake()
    {
        string data = ReadJson();
        _dialogTextRepository = JsonConvert.DeserializeObject<DialogTextRepository>(data);
    }
    private string ReadJson()
    {
        string jsonData;
        string fileUrl = Application.dataPath + Path;

        using (StreamReader sr = File.OpenText(fileUrl))
        {
            //数据保存
            jsonData = sr.ReadToEnd();
            sr.Close();
        }

        // Debug.Log(jsonData);
        return jsonData;
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
        if (_dialogTextRepository.Data[_currentID].BranchesNum != 0)
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

        int branchNum = _dialogTextRepository.Data[_currentID].BranchesNum;
        int baseID = _dialogTextRepository.Data[_currentID].JumpID;

        if (branchNum == 2)
        {
            for (int i = 0; i < branchNum; i++)
            {
                s2Branch[i].text = _dialogTextRepository.Data[baseID + i].Content;
            }
            selector2.SetActive(true);
            selector3.SetActive(false);
        }
        else if (branchNum == 3)
        {
            for (int i = 0; i < branchNum; i++)
            {
                s3Branch[i].text = _dialogTextRepository.Data[baseID + i].Content;
            }
            selector2.SetActive(false);
            selector3.SetActive(true);
        }
        else
            Debug.LogWarning($"Configure error of {_currentID} ");
    }

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
}
