using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class DialoguePlayer : MonoBehaviour
{
    [SerializeField] private RectTransform _panelRectTransform;
    [SerializeField] private TextMeshProUGUI _characterText;
    [SerializeField] private TextMeshProUGUI _lineText;
    [SerializeField] private Image _waitingMarkImage;
    [SerializeField] private float _typingSpeed = 0.05f;

    private List<string> _sentences;
    private int _currentSentenceIndex;
    private Coroutine _typingCoroutine;
    private bool _isTyping;

    public bool IsTyping => _isTyping;

    public bool IsLastLine => _currentSentenceIndex >= _sentences.Count - 1;

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
        _characterText.text = dialogue.CharacterID.ToString();
        _sentences = new List<string>();
        foreach (var line in dialogue.Lines)
        {
            _sentences.Add(line.Sentence);
        }
        _currentSentenceIndex = 0;
        DisplayCurLine();
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
            Debug.Log($"문장 {_currentSentenceIndex + 1}/{_sentences.Count}");
        }
        else
        {
            Debug.Log("모든 대화를 완료했습니다.");
        }
    }

    public void CompleteCurLine()
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
            _lineText.text = _sentences[_currentSentenceIndex];
            _typingCoroutine = null;
            _isTyping = false;
            ShowWaitingMark(true);
        }
    }

    public void DisplayNextLine()
    {
        if (_currentSentenceIndex < _sentences.Count - 1)
        {
            _currentSentenceIndex++;
            DisplayCurLine();
        }
        else
        {
            Debug.Log("더 이상 다음 문장이 없습니다.");
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _lineText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            _lineText.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
        }
        _typingCoroutine = null;
        _isTyping = false;
        ShowWaitingMark(true);
    }
}
