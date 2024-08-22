using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public enum EDialogueState
{
    NotStarted,    // 대화가 시작되지 않은 상태
    Typing,        // 현재 텍스트가 타이핑되고 있는 상태
    Waiting,       // 타이핑이 끝나고, 대기 중인 상태
    Finished       // 모든 대화가 끝난 상태
}
public class DialoguePlayer : MonoBehaviour
{
    [SerializeField] private RectTransform _panelRectTransform;
    [SerializeField] private SentenceRectMask _sentenceRectMaskPrefab;
    [SerializeField] private Transform _rectMaskParent;

    private List<SentenceRectMask> CeatedRectMasks => _rectMaskBuilder.CreatedRectMasks;
    private int _currentMaskIndex = 0; // 현재 재생 중인 RectMask의 인덱스

    private SentenceRectMaskBuilder _rectMaskBuilder;

    public bool CanCompleteInstantly { get; set; } = true; // 즉시 완료 가능 여부를 설정하는 옵션
    public EDialogueState DialogueState { get => _dialogueState; }

    private EDialogueState _dialogueState = EDialogueState.NotStarted;
    private Coroutine _currentRevealCoroutine; // 현재 실행 중인 RevealRectMasks 코루틴

    private void Start(){
        _rectMaskBuilder = new SentenceRectMaskBuilder(_rectMaskParent, _sentenceRectMaskPrefab);
    }
    public void InitLine(Line line)
    {
        // 대화 상태 및 관련 변수 초기화
        _dialogueState = EDialogueState.NotStarted;
        _currentMaskIndex = 0; // 현재 재생 중인 RectMask의 인덱스를 초기화
        if (_currentRevealCoroutine != null)
        {
            StopCoroutine(_currentRevealCoroutine); // 현재 실행 중인 코루틴이 있으면 중지
            _currentRevealCoroutine = null;
        }

        // RectMaskBuilder 초기화
        _rectMaskBuilder.CreateRectMask(line, ref _currentMaskIndex); // 새로운 마스크 생성

        // ShowNextSentence를 호출하여 첫 번째 문장을 보여줌
        ShowNextSentence();
    }


    public void ShowNextSentence()
    {
        if (_dialogueState == EDialogueState.Typing || _dialogueState == EDialogueState.Finished)
        {
            return; // 이미 타이핑 중이거나 모든 대화가 끝난 상태에서는 새로운 ShowNext를 시작하지 않음
        }

        if (CeatedRectMasks == null || CeatedRectMasks.Count == 0)
        {
            Debug.LogWarning("No masks available to show.");
            return;
        }

        Debug.Log("ShowNext");

        _dialogueState = EDialogueState.Typing;

        int startIndex = _currentMaskIndex;
        int endIndex = _rectMaskBuilder.CalculateEndIndex(CeatedRectMasks, startIndex);

        // Start and End Index Debugging
        Debug.Log($"ShowNext StartIndex: {startIndex}, EndIndex: {endIndex}");

        // Display the content of the current rect masks
        for (int i = startIndex; i <= endIndex && i < CeatedRectMasks.Count; i++)
        {
            Debug.Log($"RectMask {i}: {_rectMaskBuilder.CreatedRectMasks[i].Sentence}");
        }

        _currentRevealCoroutine = StartCoroutine(RevealRectMasks(startIndex, endIndex, 0.05f));
    }


    private IEnumerator RevealRectMasks(int startIndex, int endIndex, float delay)
    {
        for (int i = startIndex; i <= endIndex && i < CeatedRectMasks.Count; i++)
        {
            var rectMask = CeatedRectMasks[i];
            if (rectMask != null)
            {
                yield return StartCoroutine(rectMask.RevealMask(delay)); 
            }
            else
            {
                Debug.LogWarning("RectMask is null, skipping.");
            }
        }
        
        _currentMaskIndex = endIndex + 1; // 다음 인덱스 업데이트

        if (_currentMaskIndex >= CeatedRectMasks.Count)
        {
            _dialogueState = EDialogueState.Finished; // 모든 RectMask 재생 완료
        }
        else if (CeatedRectMasks[_currentMaskIndex - 1].FragmentReason == SentenceRectMask.EFragmentReason.Regex)
        {
            _dialogueState = EDialogueState.Waiting; // Regex로 인한 대기 상태로 전환
        }
        else
        {
            _dialogueState = EDialogueState.Typing; // 여전히 타이핑 중인 상태로 유지
        }
    }

    public void CompleteCurSentence()
    {
        if (_dialogueState != EDialogueState.Typing || _currentRevealCoroutine == null || !CanCompleteInstantly)
        {
            return; // Typing 상태가 아니거나 코루틴이 실행 중이 아니거나, 즉시 완료가 불가능한 상태라면 리턴
        }

        StopCoroutine(_currentRevealCoroutine); // 현재 실행 중인 RevealRectMasks 코루틴 중지

        int startIndex = _currentMaskIndex;
        int endIndex = _rectMaskBuilder.CalculateEndIndex(CeatedRectMasks, startIndex);

        // 해당 범위의 모든 SentenceRectMask에 대해 RevealInstantly 호출
        for (int i = startIndex; i <= endIndex && i < CeatedRectMasks.Count; i++)
        {
            var rectMask = CeatedRectMasks[i];
            if (rectMask != null)
            {
                rectMask.RevealInstantly(); // 즉시 텍스트를 완성
            }
        }

        // 상태를 업데이트
        _currentMaskIndex = endIndex + 1;

        if (_currentMaskIndex >= CeatedRectMasks.Count)
        {
            _dialogueState = EDialogueState.Finished;
        }
        else if (CeatedRectMasks[_currentMaskIndex - 1].FragmentReason == SentenceRectMask.EFragmentReason.Regex)
        {
            _dialogueState = EDialogueState.Waiting;
        }
        else
        {
            _dialogueState = EDialogueState.Typing;
        }
    }

    public void ShowPanel(bool show, float duration)
    {
        float offScreenY = -_panelRectTransform.rect.height;
        Vector2 targetPos = show ? Vector2.zero : new Vector2(0, offScreenY);
        _panelRectTransform.DOAnchorPos(targetPos, duration).SetEase(Ease.OutCubic);
    }
}
