using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public enum EDialogueState
{
    NotStarted,    // 대화가 시작되지 않은 상태
    Typing,        // 현재 텍스트가 타이핑되고 있는 상태
    Waiting,       // 타이핑이 끝나고, 대기 중인 상태
    Finished       // 모든 대화가 끝난 상태
}

public class DialoguePlayer : MonoBehaviour
{
    [SerializeField] private bool _showDebug;
    [SerializeField] private RectTransform _panelRectTransform;
    [SerializeField] private Image _lineTextBackground;
    [SerializeField] private Image _characterTextBackground;
    [SerializeField] private TextMeshProUGUI _characterText;
    [SerializeField] private TextDisplayer _dialogueText;
    [SerializeField] private DialogueGuide _dialogueGuide;  // DialogueGuide 참조
    
    private bool _isOn = true;
    private EDialogueState _dialogueState = EDialogueState.NotStarted;

    public bool CanCompleteInstantly { get; set; } = true; // 즉시 완료 가능 여부를 설정하는 옵션
    public EDialogueState DialogueState => _dialogueState;

    public bool IsOn { get => _isOn; }

    private void Start()
    {
        ShowUp(false, 0f);
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            CompleteCurSentence();
        }
    }
    public void ClearDialogueText(){
        _dialogueText.ClearText();
    }
    public void ClearCharacterText(){

        SetCharacterText("", Color.black);
    }
    public void ShowCharacterTextBackground(bool show, float duration, Color c)
    {
        // _lineTextBackground의 alpha 값으로 페이드 타겟 설정
        float targetAlpha = show ? _lineTextBackground.color.a : 0f;

        // _characterTextBackground의 페이드 처리
        _characterTextBackground.DOColor(c.ModifiedAlpha(targetAlpha), duration);
    }

   // CanvasGroup을 페이드 아웃하는 메서드
    public void FadeInDialogueText(float duration)
    {
        _dialogueText.FadeIn(duration);
    }
    public void FadeOutDialogueText(float duration)
    {
        _dialogueText.FadeOut(duration);
    }
    public void SetCharacterTextClor(Color targetColor, float duration){
        _characterText.DOColor(targetColor, duration);
    }
    public IEnumerator ShowLineRoutine(Line line, float speed, System.Action OnLineStarted, System.Action OnLineComplete)
    {
        _dialogueText.InitText(line.Sentence);
        yield return _dialogueText.ShowTextRoutine(speed, WaitForInputRoutine(), OnLineStarted, OnLineComplete);
    }


    IEnumerator WaitForInputRoutine(){
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    }

    public void SetCharacterText(string s, Color c)
    {
        _characterText.SetText(s);
        bool showCharacterTextBackground = !string.IsNullOrEmpty(s);
        ShowCharacterTextBackground(showCharacterTextBackground, .1f, c);
    }

    public void CompleteCurSentence()
    {
        _dialogueText.SetAllCompleted();
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
                _dialogueGuide.SetState(GuideState.Hidden);
                break;
            case EDialogueState.Waiting:
                UpdateDialogueGuidePosition();
                _dialogueGuide.SetState(GuideState.Ongoing);
                break;
            case EDialogueState.Finished:
                UpdateDialogueGuidePosition();
                _dialogueGuide.SetState(GuideState.Ended);
                break;
            case EDialogueState.NotStarted:
                _dialogueGuide.SetState(GuideState.Hidden);
                break;
        }
    }
    

    private void UpdateDialogueGuidePosition()
    {
    }

    public void ShowUp(bool show, float duration)
    {
        if(_isOn == show){
            return;
        }
        _isOn = show;
        float offScreenY = -500;
        Vector2 targetPos = show ? Vector2.zero : new Vector2(0, offScreenY);
        _panelRectTransform.DOAnchorPos(targetPos, duration).SetEase(Ease.OutQuad);
    }
}
