using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragMe : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private RectTransform _rectTransform;
    private Vector3 _posOffSet;
    private float _wide;
    private Vector3 _originalPos;

    private void Awake() 
    {
        _rectTransform=GetComponent<RectTransform>();

        _originalPos=_rectTransform.position;

        _wide=_rectTransform.rect.width;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin drag!");
        
        _posOffSet=new Vector3(eventData.position.x,eventData.position.y,0)-_rectTransform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform,eventData.position,null,out pos);

        Vector3 targetPos=pos-_posOffSet;

        if(targetPos.x<_originalPos.x&&targetPos.x>(_originalPos.x-_wide))
            _rectTransform.position=new Vector3(targetPos.x,_rectTransform.position.y,0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag!");
    }


}
