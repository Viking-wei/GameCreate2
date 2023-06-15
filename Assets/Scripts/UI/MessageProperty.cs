using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
namespace UI
{
    public class MessageProperty : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Image _image;
        private Sequence _sequence;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(_rectTransform.DOMoveY(Screen.height*0.75f, 0.5f).SetEase(Ease.OutQuint));
            _sequence.Append(_image.DOFade(0, 0.5f).SetEase(Ease.OutQuint).OnComplete(() => Destroy(gameObject)));
        }
    }
}
