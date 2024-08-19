using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public enum DialogueState
{
    Inactive,
    Typing,
    Waiting,
    Completed
}

public class DialoguePlayer : MonoBehaviour
{
    [SerializeField] private RectTransform _panelRectTransform;
    [SerializeField] private Image _waitingMarkImage;
    [SerializeField] private GameObject _lineGroupPrefab;  // LineGroup 프리팹
    [SerializeField] private GameObject _lineTextPrefab;   // LineText 프리팹
    [SerializeField] private Transform _lineGroupParent;   // LineGroups의 부모가 될 트랜스폼

    private float _typingSpeed = 0.05f;
    private bool _isTyping = false;

    public float TypingSpeed
    {
        get => _typingSpeed;
        set => _typingSpeed = Mathf.Clamp(value, 0.01f, 1f);
    }

    private List<string> _sentences;
    private int _currentSentenceIndex;
    private Coroutine _typingCoroutine;
    private DialogueState _state = DialogueState.Inactive;

    public DialogueState CurrentState => _state;

    private List<RectMask2D> _lineGroups = new List<RectMask2D>(); // RectMask2D 저장소
    private List<TextMeshProUGUI> _lineTexts = new List<TextMeshProUGUI>(); // 텍스트 컴포넌트 저장소

    void Start()
    {
        ShowPanel(false, 0f);
        ShowWaitingMark(false);
    }

    public void ShowPanel(bool show, float duration)
    {
        float offScreenY = -_panelRectTransform.rect.height;
        Vector2 targetPos = show ? Vector2.zero : new Vector2(0, offScreenY);
        _panelRectTransform.DOAnchorPos(targetPos, duration).SetEase(Ease.OutCubic);
    }

    public void ShowWaitingMark(bool show)
    {
        _waitingMarkImage.gameObject.SetActive(show);
    }

    public void InitDialogue(Dialogue dialogue)
    {
        ClearLineGroups();

        _sentences = new List<string>();
        foreach (var line in dialogue.Lines)
        {
            _sentences.Add(line.Sentence);
        }
        _currentSentenceIndex = 0;
        _state = DialogueState.Typing;

        DisplayCurLine();
    }

    private void ClearLineGroups()
    {
        foreach (var group in _lineGroups)
        {
            Destroy(group.gameObject);
        }
        _lineGroups.Clear();
        _lineTexts.Clear();
    }

    private void CreateLineGroup(float yOffset)
    {
        GameObject lineGroupInstance = Instantiate(_lineGroupPrefab, _lineGroupParent);
        RectMask2D mask = lineGroupInstance.GetComponent<RectMask2D>();

        // LineGroup의 위치 설정 (yOffset 만큼 아래에 배치)
        RectTransform lineGroupRect = lineGroupInstance.GetComponent<RectTransform>();
        lineGroupRect.anchoredPosition = new Vector2(lineGroupRect.anchoredPosition.x, yOffset);

        GameObject lineTextInstance = Instantiate(_lineTextPrefab, lineGroupInstance.transform);
        TextMeshProUGUI text = lineTextInstance.GetComponent<TextMeshProUGUI>();

        // TextMeshProUGUI의 로컬 포지션을 (0, 0, 0)으로 설정
        RectTransform lineTextRect = lineTextInstance.GetComponent<RectTransform>();
        lineTextRect.localPosition = Vector3.zero;

        _lineGroups.Add(mask);
        _lineTexts.Add(text);

        // 모든 LineGroup이 생성 직후 완전히 가려지도록 초기화
        float textWidth = 1600; // 기본적으로 큰 값으로 설정하여 전체를 가립니다.
        mask.padding = new Vector4(mask.padding.x, mask.padding.y, textWidth, mask.padding.w);
    }


    public void DisplayCurLine()
    {
        if (_currentSentenceIndex < _sentences.Count)
        {
            if (_typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);
            }
            _isTyping = true;
            ShowWaitingMark(false);

            _typingCoroutine = StartCoroutine(TypeSentence(_sentences[_currentSentenceIndex]));
        }
        else
        {
            Debug.Log("모든 대화를 완료했습니다.");
            _state = DialogueState.Completed;
        }
    }

    public void CompleteCurLine()
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);

            // 모든 라인의 텍스트를 완전히 드러나게 설정
            for (int i = 0; i < _lineGroups.Count; i++)
            {
                RectMask2D lineGroup = _lineGroups[i];
                lineGroup.padding = new Vector4(lineGroup.padding.x, lineGroup.padding.y, 0, lineGroup.padding.w);
            }

            _typingCoroutine = null;
            _isTyping = false;
            ShowWaitingMark(true);
            _state = DialogueState.Waiting;
        }
    }

    public void DisplayNextLine()
    {
        if (_currentSentenceIndex < _sentences.Count - 1)
        {
            _currentSentenceIndex++;
            _state = DialogueState.Typing;
            DisplayCurLine();
        }
        else
        {
            _state = DialogueState.Completed;
            Debug.Log("더 이상 다음 문장이 없습니다.");
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        ClearLineGroups();  // 각 문장이 출력될 때 이전 LineGroup들을 모두 제거

        int currentCharIndex = 0;
        float yOffset = 0f;  // 초기 yOffset 설정

        CreateLineGroup(yOffset);  // 새로운 LineGroup 생성

        while (currentCharIndex < sentence.Length)
        {
            TextMeshProUGUI currentLineText = _lineTexts[_lineTexts.Count - 1];
            currentLineText.text += sentence[currentCharIndex];

            currentLineText.ForceMeshUpdate();  // 메쉬 업데이트

            if (currentLineText.isTextOverflowing)
            {
                string overflowText = currentLineText.text;
                currentLineText.text = overflowText.Substring(0, overflowText.Length - 1);

                yOffset -= currentLineText.rectTransform.rect.height;
                CreateLineGroup(yOffset);

                currentLineText = _lineTexts[_lineTexts.Count - 1];
                currentLineText.text = overflowText[overflowText.Length - 1].ToString();

                // 새로 생성된 라인 그룹의 텍스트가 보이지 않도록 보장
                RectMask2D currentMask = _lineGroups[_lineGroups.Count - 1];
                currentMask.padding = new Vector4(currentMask.padding.x, currentMask.padding.y, 1600, currentMask.padding.w);
            }

            currentCharIndex++;
        }

        for (int i = 0; i < _lineGroups.Count; i++)
        {
            RectMask2D lineGroup = _lineGroups[i];
            yield return StartCoroutine(RevealLine(lineGroup));
        }

        _typingCoroutine = null;
        _isTyping = false;
        ShowWaitingMark(true);
        _state = DialogueState.Waiting;
    }


    private IEnumerator RevealLine(RectMask2D lineGroup)
    {
        float startRight = lineGroup.padding.z; // 현재 설정된 right padding 값을 시작 값으로 사용
        float elapsedTime = 0f;
        float revealDuration = _typingSpeed * 30; // 드러나는 속도를 더 느리게 설정

        while (elapsedTime < revealDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / revealDuration);

            float currentRight = Mathf.Lerp(startRight, 0, progress);
            lineGroup.padding = new Vector4(lineGroup.padding.x, lineGroup.padding.y, currentRight, lineGroup.padding.w);

            yield return null;
        }

        // 마스킹을 완전히 제거
        lineGroup.padding = new Vector4(lineGroup.padding.x, lineGroup.padding.y, 0, lineGroup.padding.w);
    }
}
