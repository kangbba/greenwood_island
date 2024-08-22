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
    [SerializeField] private TextMeshProUGUI _characterText;
    [SerializeField] private DialogueGuide _dialogueGuide;  // DialogueGuide 참조

    private List<SentenceRectMask> CreatedRectMasks => _rectMaskBuilder.CreatedRectMasks;
    private int _currentMaskIndex = 0; // 현재 재생 중인 RectMask의 인덱스
    private SentenceRectMaskBuilder _rectMaskBuilder;
    private Coroutine _currentRevealCoroutine; // 현재 실행 중인 RevealRectMasks 코루틴
    private EDialogueState _dialogueState = EDialogueState.NotStarted;

    public bool CanCompleteInstantly { get; set; } = true; // 즉시 완료 가능 여부를 설정하는 옵션
    public EDialogueState DialogueState => _dialogueState;

    private void Start()
    {
        ShowPanel(false, 0f);
        SetCharacterText("", Color.clear);
        _rectMaskBuilder = new SentenceRectMaskBuilder(_rectMaskParent, _sentenceRectMaskPrefab);
    }

    public void Clear()
    {
        SetCharacterText("", Color.clear);
        _rectMaskBuilder.Clear();
    }

    public void SetCharacterText(string s, Color targetColor)
    {
        _characterText.SetText(s);

        // 기존에 실행 중이던 색상 변환이 있다면 중지
        _characterText.DOKill(); 

        // 현재 색상에서 targetColor로 색상 변경 애니메이션
        _characterText.DOColor(targetColor, 0.5f); // 0.5초 동안 색상이 바뀌도록 설정
    }

    public void InitLine(Line line)
    {
        // 대화 상태 및 관련 변수 초기화
        SetState(EDialogueState.NotStarted);
        _currentMaskIndex = 0; // 현재 재생 중인 RectMask의 인덱스를 초기화
        if (_currentRevealCoroutine != null)
        {
            StopCoroutine(_currentRevealCoroutine); // 현재 실행 중인 코루틴이 있으면 중지
            _currentRevealCoroutine = null;
        }

        // 새로운 마스크 생성
        _rectMaskBuilder.CreateRectMask(line, ref _currentMaskIndex);

        // 첫 번째 문장을 보여줌
        ShowNextSentence();
    }

    public void ShowNextSentence()
    {
        if (CreatedRectMasks == null || CreatedRectMasks.Count == 0)
        {
            Debug.LogWarning("No masks available to show.");
            return;
        }

        Debug.Log("ShowNext");

        SetState(EDialogueState.Typing);

        int startIndex = _currentMaskIndex;
        int endIndex = _rectMaskBuilder.CalculateEndIndex(CreatedRectMasks, startIndex);

        // Start and End Index Debugging
        Debug.Log($"ShowNext StartIndex: {startIndex}, EndIndex: {endIndex}");

        // Display the content of the current rect masks
        for (int i = startIndex; i <= endIndex && i < CreatedRectMasks.Count; i++)
        {
            Debug.Log($"RectMask {i}: {CreatedRectMasks[i].Sentence}");
        }

        _currentRevealCoroutine = StartCoroutine(RevealRectMasks(startIndex, endIndex, 0.05f));
    }

    private IEnumerator RevealRectMasks(int startIndex, int endIndex, float delay)
    {
        for (int i = startIndex; i <= endIndex && i < CreatedRectMasks.Count; i++)
        {
            var rectMask = CreatedRectMasks[i];
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

        if (_currentMaskIndex >= CreatedRectMasks.Count)
        {
            SetState(EDialogueState.Finished); // 모든 RectMask 재생 완료
        }
        else if (CreatedRectMasks[_currentMaskIndex - 1].FragmentReason == SentenceRectMask.EFragmentReason.Regex)
        {
            SetState(EDialogueState.Waiting); // Regex로 인한 대기 상태로 전환
        }
        else
        {
            SetState(EDialogueState.Typing); // 여전히 타이핑 중인 상태로 유지
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
        int endIndex = _rectMaskBuilder.CalculateEndIndex(CreatedRectMasks, startIndex);

        // 해당 범위의 모든 SentenceRectMask에 대해 RevealInstantly 호출
        for (int i = startIndex; i <= endIndex && i < CreatedRectMasks.Count; i++)
        {
            var rectMask = CreatedRectMasks[i];
            if (rectMask != null)
            {
                rectMask.RevealInstantly(); // 즉시 텍스트를 완성
            }
        }

        // 상태를 업데이트
        _currentMaskIndex = endIndex + 1;

        if (_currentMaskIndex >= CreatedRectMasks.Count)
        {
            SetState(EDialogueState.Finished);
        }
        else if (CreatedRectMasks[_currentMaskIndex - 1].FragmentReason == SentenceRectMask.EFragmentReason.Regex)
        {
            SetState(EDialogueState.Waiting);
        }
        else
        {
            SetState(EDialogueState.Typing);
        }
    }

    public void SetState(EDialogueState newState)
    {
        if (_dialogueState == newState)
        {
            return; // 중복된 상태로 전환하지 않음
        }

        _dialogueState = newState;

        // 상태 전환에 따른 작업 수행
        switch (newState)
        {
            case EDialogueState.Typing:
                // Typing 상태로 전환될 때 _dialogueGuide를 숨김
                _dialogueGuide.SetState(GuideState.Hidden);
                break;
            case EDialogueState.Waiting:
                // Waiting 상태로 전환될 때 _dialogueGuide를 마지막 텍스트 위치로 이동 및 Ongoing 상태로 전환
                MoveGuideToLastCharacter();
                _dialogueGuide.SetState(GuideState.Ongoing);
                break;
            case EDialogueState.Finished:
                // Finished 상태로 전환될 때 _dialogueGuide를 마지막 텍스트 위치로 이동 및 Ended 상태로 전환
                MoveGuideToLastCharacter();
                _dialogueGuide.SetState(GuideState.Ended);
                break;
            case EDialogueState.NotStarted:
                // NotStarted 상태에서는 _dialogueGuide를 숨김
                _dialogueGuide.SetState(GuideState.Hidden);
                break;
        }
    }

    private void MoveGuideToLastCharacter()
    {
        if (_currentMaskIndex > 0 && _currentMaskIndex <= CreatedRectMasks.Count)
        {
            var lastMask = CreatedRectMasks[_currentMaskIndex - 1];;
            Vector3 offset =  Vector3.right * (lastMask.PreferredWidth + 0) + Vector3.down * 50f;
            _dialogueGuide.transform.position = lastMask.transform.position + offset;
        }
    }

    public void ShowPanel(bool show, float duration)
    {
        float offScreenY = -_panelRectTransform.rect.height;
        Vector2 targetPos = show ? Vector2.zero : new Vector2(0, offScreenY);
        _panelRectTransform.DOAnchorPos(targetPos, duration).SetEase(Ease.OutCubic);
    }
}
