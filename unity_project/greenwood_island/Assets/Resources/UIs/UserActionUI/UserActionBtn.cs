using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserActionBtn : MonoBehaviour
{
    [SerializeField] private Button _button; // 버튼 컴포넌트
    [SerializeField] private Image _iconImg; // 아이콘 이미지 컴포넌트
    [SerializeField] private TextMeshProUGUI _labelText; // 버튼의 텍스트 컴포넌트
    private bool _isActionExecuted; // 액션이 실행되었는지를 판단하는 플래그
    private bool _isCoroutineRunning; // 코루틴이 실행 중인지 확인하는 플래그

    private Func<IEnumerator> _onClickCoroutine; // 실행할 코루틴 함수

    public bool IsActionExecuted => _isActionExecuted; // 액션이 실행되었는지를 반환하는 게터

    // 버튼 초기화 메서드: 아이콘, 텍스트, 클릭 액션을 설정합니다.
    public void Init(Sprite icon, string displayName, Func<IEnumerator> onClickCoroutine)
    {
        _iconImg.sprite = icon; // 아이콘 설정
        _labelText.text = displayName; // 텍스트 설정
        _isActionExecuted = false; // 액션 실행 상태 초기화
        _isCoroutineRunning = false; // 코루틴 실행 상태 초기화
        _onClickCoroutine = onClickCoroutine;

        _button.onClick.RemoveAllListeners(); // 기존의 모든 리스너 제거
        _button.onClick.AddListener(() =>
        {
            OnClickedBtn();
        });
    }

    private void OnClickedBtn()
    {
        if (_isActionExecuted || _isCoroutineRunning) // 이미 실행된 경우 또는 코루틴이 실행 중인 경우 실행하지 않음
        {
            return;
        }

        _isActionExecuted = true; // 액션이 실행되었음을 표시
        StartCoroutine(ExecuteCoroutine()); // 코루틴 실행
    }

    private IEnumerator ExecuteCoroutine()
    {
        _isCoroutineRunning = true; // 코루틴 실행 상태 설정

        if (_onClickCoroutine != null)
        {
            yield return StartCoroutine(_onClickCoroutine.Invoke());
        }

        _isCoroutineRunning = false; // 코루틴이 완료되면 실행 상태 해제
    }
}
