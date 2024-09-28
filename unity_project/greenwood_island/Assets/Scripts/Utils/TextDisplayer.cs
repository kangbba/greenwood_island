using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextDisplayer : MonoBehaviour
{

    private TextMeshProUGUI _mainText; // 최상위 TextMeshProUGUI
    private CanvasGroup _canvasGroup; // 페이드 효과를 위한 CanvasGroup

    private List<LineContainer> _lineContainers = new List<LineContainer>();

    private void Awake()
    {
        // mainText를 자동으로 바인딩
        _mainText = GetComponent<TextMeshProUGUI>();

        // CanvasGroup 바인딩 또는 추가
        _canvasGroup = GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    // 텍스트를 페이드 인하는 함수
    public void FadeIn(float duration, Ease ease = Ease.OutQuad)
    {
        _canvasGroup.DOFade(1f, duration).SetEase(ease).OnStart(() =>
        {
        });
    }
    // 텍스트를 페이드 아웃하는 함수
    public void FadeOut(float duration, Ease ease = Ease.OutQuad)
    {
        _canvasGroup.DOFade(0f, duration).SetEase(ease).OnComplete(() =>
        {
        });
    }
    public void ClearText(){
        StopAllCoroutines();
        _mainText.text = ""; // 메인 텍스트에 먼저 텍스트를 설정하여 줄 정보를 가져옴
        // 기존 컨테이너 제거
        foreach (var container in _lineContainers)
        {
            Destroy(container.gameObject);
        }
        _lineContainers.Clear();
    }
    public void InitText(string text)
    {
        StopAllCoroutines();
        ClearText();
        _mainText.text = text; // 메인 텍스트에 먼저 텍스트를 설정하여 줄 정보를 가져옴

        // TMP 라인 정보 가져오기
        _mainText.ForceMeshUpdate(); // 텍스트 레이아웃 강제 업데이트
        TMP_TextInfo textInfo = _mainText.textInfo;

        // 각 줄의 컨테이너 생성 및 드러나는 데 걸리는 속도 설정
        for (int i = 0; i < textInfo.lineCount; i++)
        {
            TMP_LineInfo lineInfo = textInfo.lineInfo[i];

            // 각 줄에 해당하는 텍스트 생성 및 설정
            string lineText = _mainText.text.Substring(lineInfo.firstCharacterIndex, lineInfo.characterCount);

            // LineContainer 생성 및 설정
            LineContainer lineContainer = new LineContainer(transform, lineText, _mainText, lineInfo, i);
            _lineContainers.Add(lineContainer);
        }

        // 메인 텍스트 비우기
        _mainText.text = string.Empty;
    }

    // 텍스트를 표시하는 함수
    public IEnumerator ShowTextRoutine(float revealSpeed, IEnumerator waitForInputCoroutine, System.Action onLineStarted = null, System.Action onLineComplete = null)
    {
        foreach (var container in _lineContainers)
        {
            yield return container.RevealNextText(revealSpeed, waitForInputCoroutine, onLineStarted, onLineComplete); // 연속적으로 텍스트를 표시
        }
    }

    public void SetAllCompleted(){
        // 기존 컨테이너 제거
        foreach (var container in _lineContainers)
        {
            container.SetCompleted();
        }
    }
}
