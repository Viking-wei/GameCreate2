using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSizeControlller : MonoBehaviour
{
    private RectTransform _rectTransform;
    private TextMeshProUGUI _textMeshProUGUI;

    private void Awake() 
    {
        _rectTransform=GetComponent<RectTransform>();
        _textMeshProUGUI=GetComponent<TextMeshProUGUI>();
    } 
    private void Update() 
    {
        _rectTransform.sizeDelta=new Vector2(_rectTransform.sizeDelta.x,_textMeshProUGUI.preferredHeight);
    }
}
