using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class DialoguePlayer : MonoBehaviour
{
    private Line testLine = new Line(EEmotionID.Angry, 0, "안녕하세요. 긴 문장 테스트를 해보겠습니다 어떻게 보일지 과연 너무나 신기할것같아요 하하하. 하지만 이건 잘 작동하겠죠. 솔직하게 말하자구요");
    private Line _currentLine; // 현재 타겟이 되는 Line을 저장하는 변수
    private List<Line> _lines;

    // RectMask 프리팹과 부모 객체
    [SerializeField] private SentenceRectMask _sentenceRectMaskPrefab;
    [SerializeField] private Transform _rectMaskParent;

    private List<SentenceRectMask> _createdRectMasks = new List<SentenceRectMask>();
    private int _currentMaskIndex = 0; // 현재 재생 중인 RectMask의 인덱스

    private Regex _splitRegex = new Regex(@"\.");

    private void Start()
    {
        // 초기화 시 테스트 라인을 설정하고 RectMask 생성 후 ShowNext 호출
        CreateRectMask(testLine);
        ShowNext();
    }

    public void InitDialogue(Dialogue dialogue)
    {
        _lines = dialogue.Lines;
    }

    public void ShowNext()
    {
        // 생성된 RectMask들을 순차적으로 재생하는 코루틴 시작
        StartCoroutine(RevealRectMasks());
    }

    private void CreateRectMask(Line line)
    {
        _currentLine = line; // 현재 타겟이 되는 Line을 저장
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

    private IEnumerator RevealRectMasks()
    {
        while (_currentMaskIndex < _createdRectMasks.Count)
        {
            var rectMask = _createdRectMasks[_currentMaskIndex];
            yield return StartCoroutine(rectMask.RevealMask(0.05f)); // 예시로 0.05초의 딜레이를 줌

            // 만약 현재 RectMask의 FragmentReason이 Regex라면 ShowNext를 다시 호출할 때까지 대기
            if (rectMask.FragmentReason == SentenceRectMask.EFragmentReason.Regex)
            {
                _currentMaskIndex++; // 인덱스 업데이트
                yield break; // 코루틴 종료, ShowNext를 다시 호출하면 이어서 진행
            }

            _currentMaskIndex++; // 다음 Mask로 이동
        }
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            ShowNext();
        }
    }
}
