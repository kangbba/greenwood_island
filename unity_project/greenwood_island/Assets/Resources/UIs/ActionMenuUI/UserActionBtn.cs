using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ActionMenuButton은 ActionMenuUI 내의 각 버튼을 관리하는 클래스입니다.
/// </summary>
public class UserActionBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText; // 버튼의 텍스트 컴포넌트
    [SerializeField] private CanvasGroup _graphics;
    [SerializeField] private Button button; // 버튼 컴포넌트
    [SerializeField] private Image arrowImage; // 우측 화살표 이미지

    private int _index;
    private UserActionBtnContent _btnContent; // 연동된 ActionMenu 객체
    private UserActionBtn _parentBtn; // 연동된 ActionMenu 객체
    private List<UserActionBtn> _subButtons = new List<UserActionBtn>(); // 하위 버튼 리스트
    private bool _isExecuting = false; // 현재 액션이 실행 중인지 여부
    private bool _isExpanded = false; // 하위 버튼이 펼쳐졌는지 여부
    private int _depth; // 현재 버튼의 Depth

    // Depth 프로퍼티
    public int Depth => _depth;

    // 람다 식으로 최하위 버튼 여부를 판단
    public bool IsLeafBtn => _btnContent.SubBtnContents == null || _btnContent.SubBtnContents.Count == 0;

    /// <summary>
    /// 버튼을 초기화하는 메서드
    /// </summary>
   
    // 버튼을 초기화하는 메서드
    public void Init(UserActionUI userActionUI, UserActionBtn parentBtn, UserActionBtnContent btnContent, int index, int depth)
    {
        _parentBtn = parentBtn;
        _btnContent = btnContent;
        _index = index;
        _depth = depth;

        buttonText.text = btnContent.Title;
        arrowImage.gameObject.SetActive(!IsLeafBtn); // 하위 메뉴가 있을 때만 화살표 표시
        name = btnContent.Title; // 버튼 이름을 인풋 타이틀과 동일하게 설정
        button.onClick.AddListener(() => OnButtonClicked());

        _subButtons.Clear();
        if(btnContent.SubBtnContents != null){
            for(int i = 0 ; i < btnContent.SubBtnContents.Count ; i++){
                UserActionBtnContent subBtnContent = btnContent.SubBtnContents[i];
                UserActionBtn instSubBtn = userActionUI.CreateActionMenuBtn(this, subBtnContent, i, _depth + 1);
                _subButtons.Add(instSubBtn);
            }
        }
        else{
        }
        
        _isExecuting = false;
        _isExpanded = false;
        if(depth == 0){
            Show();
        }
        else{
            Hide();
        }
    }

    public void Fold(){
        if(!_isExpanded){
            Debug.Log("이미 접힘");
            return;
        }
        _isExpanded = false;
        for(int i = 0 ; i < _subButtons.Count ; i++){
            _subButtons[i].Hide();
            _subButtons[i].Fold();
        }
    }
    public void Expand(){
        if(_isExpanded){
            Debug.Log("이미 펼쳐짐");
            return;
        }
        _isExpanded = true;
        for(int i = 0 ; i < _subButtons.Count ; i++){
            _subButtons[i].Show();
        }
    }
    // 버튼 클릭 시 실행되는 메서드
    private void OnButtonClicked()
    {
            Debug.Log("눌림");
        if (_isExecuting)
        {
            Debug.Log($"Button '{_btnContent.Title}' is already executing.");
            return;
        }
        if (IsLeafBtn)
        {
            // 최하위 버튼의 액션 실행
            if (_btnContent.onClickedAction != null)
            {
                _isExecuting = true;
                StartCoroutine(ExecuteAction());
            }
        }
        else
        {
            Debug.Log("눌림");
            if(_isExpanded){
                Fold();
            }
            else{
                Expand();
            }
        }
    }
    // 액션을 실행하고 완료된 후 상태를 갱신하는 메서드
    private IEnumerator ExecuteAction()
    {
        yield return _btnContent.onClickedAction.ExecuteRoutine();
        _isExecuting = false;
        Debug.Log($"Action '{_btnContent.Title}' completed.");
    }

    // 버튼을 시각적으로 표시하는 메서드
    public void Show()
    {
        button.image.color = Color.white;
        _graphics.alpha = 1f;
        button.interactable = true;
    }

    // 버튼을 시각적으로 숨기는 메서드
    public void Hide()
    {
        button.image.color = Color.clear;
        _graphics.alpha = 0f;
        button.interactable = false;
    }
}
