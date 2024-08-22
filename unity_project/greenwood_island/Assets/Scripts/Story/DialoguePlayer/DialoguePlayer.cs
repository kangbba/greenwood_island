using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialoguePlayer : MonoBehaviour
{
    private Line testLine = new Line(EEmotionID.Angry, 0, "안녕하세요. 긴 문장 테스트를 해보겠습니다 어떻게 보일지 과연 너무나 신기할것같아요 하하하. 하지만 이건 잘 작동하겠죠. 솔직하게 말하자구요");
    private List<Line> _lines;

    [SerializeField] private SentenceRectMask _sentenceRectMaskPrefab;
    [SerializeField] private Transform _rectMaskParent;

    private List<SentenceRectMask> _createdRectMasks;
    private int _currentMaskIndex = 0; // 현재 재생 중인 RectMask의 인덱스

    private SentenceRectMaskBuilder _rectMaskBuilder;

    public enum EDialogueState
    {
        NotStarted,    // 대화가 시작되지 않은 상태
        Typing,        // 현재 텍스트가 타이핑되고 있는 상태
        Waiting,       // 타이핑이 끝나고, 대기 중인 상태
        Finished       // 모든 대화가 끝난 상태
    }

    public bool CanCompleteInstantly { get; set; } = true; // 즉시 완료 가능 여부를 설정하는 옵션

    private EDialogueState _dialogueState = EDialogueState.NotStarted;
    private Coroutine _currentRevealCoroutine; // 현재 실행 중인 RevealRectMasks 코루틴

    private void Start()
    {
        _rectMaskBuilder = new SentenceRectMaskBuilder(_rectMaskParent, _sentenceRectMaskPrefab);

        // 초기화 시 테스트 라인을 설정하고 RectMask 생성 후 ShowNext 호출
        _createdRectMasks = _rectMaskBuilder.CreateRectMask(testLine, ref _currentMaskIndex);
        ShowNext();
    }

    public void InitDialogue(Dialogue dialogue)
    {
        _lines = dialogue.Lines;
        _dialogueState = EDialogueState.NotStarted;
    }

    public void ShowNext()
    {
        if (_dialogueState == EDialogueState.Typing || _dialogueState == EDialogueState.Finished)
        {
            return; // 이미 타이핑 중이거나 모든 대화가 끝난 상태에서는 새로운 ShowNext를 시작하지 않음
        }

        _dialogueState = EDialogueState.Typing;

        int startIndex = _currentMaskIndex;
        int endIndex = _rectMaskBuilder.CalculateEndIndex(_createdRectMasks, startIndex);

        _currentRevealCoroutine = StartCoroutine(RevealRectMasks(startIndex, endIndex, 0.05f));
    }

    private IEnumerator RevealRectMasks(int startIndex, int endIndex, float delay)
    {
        for (int i = startIndex; i <= endIndex && i < _createdRectMasks.Count; i++)
        {
            var rectMask = _createdRectMasks[i];
            yield return StartCoroutine(rectMask.RevealMask(delay)); // 예시로 0.05초의 딜레이를 줌
        }
        
        _currentMaskIndex = endIndex + 1; // 다음 인덱스 업데이트

        if (_currentMaskIndex >= _createdRectMasks.Count)
        {
            _dialogueState = EDialogueState.Finished; // 모든 RectMask 재생 완료
        }
        else if (_createdRectMasks[_currentMaskIndex - 1].FragmentReason == SentenceRectMask.EFragmentReason.Regex)
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
        int endIndex = _rectMaskBuilder.CalculateEndIndex(_createdRectMasks, startIndex);

        // 해당 범위의 모든 SentenceRectMask에 대해 RevealInstantly 호출
        for (int i = startIndex; i <= endIndex && i < _createdRectMasks.Count; i++)
        {
            var rectMask = _createdRectMasks[i];
            rectMask.RevealInstantly(); // 즉시 텍스트를 완성
        }

        // 상태를 업데이트
        _currentMaskIndex = endIndex + 1;

        if (_currentMaskIndex >= _createdRectMasks.Count)
        {
            _dialogueState = EDialogueState.Finished;
        }
        else if (_createdRectMasks[_currentMaskIndex - 1].FragmentReason == SentenceRectMask.EFragmentReason.Regex)
        {
            _dialogueState = EDialogueState.Waiting;
        }
        else
        {
            _dialogueState = EDialogueState.Typing;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_dialogueState == EDialogueState.Waiting)
            {
                ShowNext();
            }
            else if (_dialogueState == EDialogueState.Typing)
            {
                CompleteCurSentence();
            }
        }
    }
}
