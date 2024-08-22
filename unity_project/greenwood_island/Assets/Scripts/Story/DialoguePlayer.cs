using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class DialoguePlayer : MonoBehaviour
{
    private Line testLine = new Line(EEmotionID.Angry, 0, "안녕하세요. 긴 문장 테스트를 해보겠습니다 어떻게 보일지 과연 너무나 신기할것같아요 하하하. 하지만 이건 잘 작동하겠죠. 솔직하게 말하자구요");
    private List<Line> _lines;

    // RectMask 프리팹과 부모 객체
    [SerializeField] private SentenceRectMask _sentenceRectMaskPrefab;
    [SerializeField] private Transform _rectMaskParent;

    private List<SentenceRectMask> _createdRectMasks = new List<SentenceRectMask>();
    private int _currentMaskIndex = 0; // 현재 재생 중인 RectMask의 인덱스

    private Regex _splitRegex = new Regex(@"\.");

    public enum EDialogueState
    {
        NotStarted,    // 대화가 시작되지 않은 상태
        Typing,        // 현재 텍스트가 타이핑되고 있는 상태
        Waiting,       // 타이핑이 끝나고, 대기 중인 상태
        Finished       // 모든 대화가 끝난 상태
    }

    private EDialogueState _dialogueState = EDialogueState.NotStarted;

    private void Start()
    {
        // 초기화 시 테스트 라인을 설정하고 RectMask 생성 후 ShowNext 호출
        CreateRectMask(testLine);
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
        int endIndex = CalculateEndIndex(startIndex);

        StartCoroutine(RevealRectMasks(startIndex, endIndex));
    }

    private void CreateRectMask(Line line)
    {
        _currentMaskIndex = 0; // 인덱스를 0으로 초기화

        // 기존에 생성된 RectMask들을 제거하고 리스트 초기화
        foreach (var rectMask in _createdRectMasks)
        {
            Destroy(rectMask.gameObject);
        }
        _createdRectMasks.Clear();

        string sentence = line.Sentence;
        float xOffset = 0f;
        float yOffset = 0f;
        float maxWidth = 1600f;

        string currentPart = "";
        foreach (char letter in sentence)
        {
            currentPart += letter;
            float currentWidth = GetCharacterWidth(currentPart);

            // 다음 줄로 넘어가야 하는 경우
            if (xOffset + currentWidth > maxWidth)
            {
                float availableWidth = maxWidth - xOffset;
                int splitIndex = FindSplitIndex(currentPart, availableWidth);
                
                string part1 = currentPart.Substring(0, splitIndex);
                SentenceRectMask rectMask1 = AddRectMask(part1, ref xOffset, ref yOffset, maxWidth);
                rectMask1.FragmentReason = SentenceRectMask.EFragmentReason.Overflow;

                string part2 = currentPart.Substring(splitIndex);
                xOffset = 0f;
                yOffset -= 80f;
                currentPart = part2;
            }

            else if (_splitRegex.IsMatch(letter.ToString()))
            {
                SentenceRectMask rectMask = AddRectMask(currentPart, ref xOffset, ref yOffset, maxWidth);
                rectMask.FragmentReason = SentenceRectMask.EFragmentReason.Regex;
                currentPart = "";
            }
        }

        // 마지막 남은 부분 처리
        if (!string.IsNullOrEmpty(currentPart))
        {
            SentenceRectMask rectMask = AddRectMask(currentPart, ref xOffset, ref yOffset, maxWidth);
            rectMask.FragmentReason = SentenceRectMask.EFragmentReason.LastFragment;
        }
    }

    private SentenceRectMask AddRectMask(string text, ref float xOffset, ref float yOffset, float maxWidth)
    {
        SentenceRectMask rectMask = Instantiate(_sentenceRectMaskPrefab, _rectMaskParent);
        rectMask.Init(text);

        RectTransform rectTransform = rectMask.GetComponent<RectTransform>();
        float rectWidth = rectTransform.rect.width;

        if (xOffset + rectWidth > maxWidth)
        {
            xOffset = 0f;
            yOffset -= 80f;
        }

        rectMask.SetPosition(xOffset, yOffset);
        xOffset += rectWidth;

        _createdRectMasks.Add(rectMask);

        return rectMask;
    }

    private int FindSplitIndex(string text, float availableWidth)
    {
        int splitIndex = 0;
        float width = 0f;

        for (int i = 0; i < text.Length; i++)
        {
            width = GetCharacterWidth(text.Substring(0, i + 1));
            if (width > availableWidth)
            {
                break;
            }
            splitIndex = i + 1;
        }

        return splitIndex;
    }

    private float GetCharacterWidth(string text)
    {
        SentenceRectMask tempRectMask = Instantiate(_sentenceRectMaskPrefab, _rectMaskParent);
        tempRectMask.Init(text);

        RectTransform rectTransform = tempRectMask.GetComponent<RectTransform>();
        float width = rectTransform.rect.width;

        Destroy(tempRectMask.gameObject);

        return width;
    }

    private IEnumerator RevealRectMasks(int startIndex, int endIndex)
    {
        for (int i = startIndex; i <= endIndex && i < _createdRectMasks.Count; i++)
        {
            var rectMask = _createdRectMasks[i];
            yield return StartCoroutine(rectMask.RevealMask(0.05f)); // 예시로 0.05초의 딜레이를 줌
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

    private int CalculateEndIndex(int startIndex)
    {
        int endIndex = _createdRectMasks.Count - 1;

        for (int i = startIndex; i < _createdRectMasks.Count; i++)
        {
            if (_createdRectMasks[i].FragmentReason == SentenceRectMask.EFragmentReason.Regex)
            {
                endIndex = i;
                break;
            }
        }

        return endIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_dialogueState == EDialogueState.Waiting)
            {
                ShowNext();
            }
        }
    }
}
