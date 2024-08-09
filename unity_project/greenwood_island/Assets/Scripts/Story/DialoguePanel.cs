using System.Collections;
using System.Collections.Generic;
using Ensayne.TextDisplayerSystem;
using UnityEngine;
using DG.Tweening;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField] private TextDisplayer _textDisplayer;
    
    [SerializeField] private RectTransform _panelRectTransform;

    void Start()
    {
        ShowPanel(false, 0f);
    }

    public void ShowPanel(bool b, float totalSec)
    {
        // 화면 밖으로 내리기 위한 Y축 위치 계산
        float offScreenY = -_panelRectTransform.rect.height;

        // 조건연산자를 이용해 targetPos를 캐싱
        Vector2 targetPos = b ? Vector2.zero : new Vector2(0, offScreenY);
        
        // 캐싱된 targetPos를 사용해 DOAnchorPos 메서드 호출
        _panelRectTransform.DOAnchorPos(targetPos, totalSec)
            .SetEase(Ease.OutCubic); // 0.5초 동안 이동하며 Ease.OutCubic 커브를 사용
    }

    public void SetTexts(string[] s){
        _textDisplayer.Init(s);
        _textDisplayer.ShowNext();
    }


    public void ShowNext(){
        _textDisplayer.ShowNext();
    }
}
