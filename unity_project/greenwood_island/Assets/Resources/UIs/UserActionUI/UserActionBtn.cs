using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// UserActionButton 클래스: 버튼의 설정과 동작을 관리하는 스크립트
public class UserActionBtn : MonoBehaviour
{
    [SerializeField] private Button _button; // 버튼 컴포넌트
    [SerializeField] private Image _iconImg; // 아이콘 이미지 컴포넌트
    [SerializeField] private TextMeshProUGUI _labelText; // 버튼의 텍스트 컴포넌트
    private bool _isActionExecuted; // 액션이 실행되었는지를 판단하는 플래그

    private Action _onClickAction;

    public bool IsActionExecuted => _isActionExecuted; // 액션이 실행되었는지를 반환하는 게터

    // 버튼 초기화 메서드: 아이콘, 텍스트, 클릭 액션을 설정합니다.
    public void Init(Sprite icon, string displayName, Action onClickAction)
    {
        _iconImg.sprite = icon; // 아이콘 설정
        _labelText.text = displayName; // 텍스트 설정
        _isActionExecuted = false; // 액션 실행 상태 초기화
        _onClickAction = onClickAction;

        _button.onClick.RemoveAllListeners(); // 기존의 모든 리스너 제거
        _button.onClick.AddListener(() =>
        {
            OnClickedBtn();
        });
    }

    private void OnClickedBtn(){

        if (_isActionExecuted) // 이미 실행된 경우 실행하지 않음
        {
            return;
        }
        _isActionExecuted = true; // 액션이 실행되었음을 표시
        _onClickAction.Invoke();
    }
}
