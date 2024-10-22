using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class DialoguePlayer : MonoBehaviour
{
    [SerializeField] private bool _showDebug;
    [SerializeField] private RectTransform _panelRectTransform;
    [SerializeField] private Image _lineTextBackground;
    [SerializeField] private Image _characterTextBackground;
    [SerializeField] private TextMeshProUGUI _characterText;
    [SerializeField] private TextDisplayer _dialogueText;

    [SerializeField] private CanvasGroup _canvasGroup;


    public bool CanCompleteInstantly { get; set; } = true; // 즉시 완료 가능 여부를 설정하는 옵션

    private void Awake(){
        ClearCharacterText();
        ClearDialogueText();
        FadeOutPanel(0f);
    }
    public void Init(){

    }

#if UNITY_EDITOR
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            CompleteCurSentence();
        }
    }
#endif
    public void ClearDialogueText(){
        _dialogueText.ClearText();
    }
    public void ClearCharacterText(){

        SetCharacterText("", Color.black);
    }
  
   // CanvasGroup을 페이드 아웃하는 메서드
    public void FadeInPanel(float duration)
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.DOFade(1f, duration);
    }
    public void FadeOutPanel(float duration)
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOFade(0f, duration);
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

    public void SetCharacterText(string s, Color characterColor)
    {   
        if(_characterText == null){
            Debug.Log("_characterText is null");
            return;
        }
        _characterText.SetText(s);
        _characterText.color = characterColor;
        Color characterTextBackgroundColor = string.IsNullOrEmpty(s) ? Color.clear :  Color.Lerp(characterColor, Color.black, .5f).ModifiedAlpha(_lineTextBackground.color.a);
        _characterTextBackground.color = characterTextBackgroundColor;
    }

    public void CompleteCurSentence()
    {
        _dialogueText.SetAllCompleted();
    }

}
