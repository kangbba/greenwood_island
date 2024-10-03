using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProUGUI를 사용하기 위해 추가
using DG.Tweening; // DOTween을 사용하기 위해 추가
using System.Collections;
using UnityEngine.EventSystems; // Pointer 이벤트 처리를 위해 추가

public class UserActionWindowBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private enum BtnStatus
    {
        On,        // 버튼이 활성화된 상태
        Selected,  // 버튼이 선택된 상태
        Off        // 버튼이 비활성화된 상태
    }

    [SerializeField] private CanvasGroup _graphicGroup;  // 버튼의 CanvasGroup (배경 대신)
    [SerializeField] private TextMeshProUGUI _actionText;  // 버튼에 표시될 텍스트
    [SerializeField] private Image _icon;  // 아이콘 이미지
    [SerializeField] private RectTransform _rectTr;  // 아이콘 이미지

    private BtnStatus _btnStatus;  // 버튼 상태
    private bool _isActionCompleted = false;  // 액션 완료 여부
    private IEnumerator _actionCoroutine;  // 전달받은 코루틴 저장

    public bool IsActionCompleted => _isActionCompleted;  // 버튼의 완료 여부 확인

    public RectTransform RectTr { get => _rectTr; }


    // Init 메서드: 버튼 세팅(UserActionButtonSettings)과 IEnumerator를 받아 버튼을 초기화
    public void Init(UserActionButtonSettings buttonSettings, IEnumerator actionCoroutine)
    {
        _actionText.text = buttonSettings.actionType.ToString();  // 행동 타입을 텍스트로 표시
        _icon.sprite = buttonSettings.actionIcon;  // 아이콘 설정
        _actionCoroutine = actionCoroutine;  // 실행할 IEnumerator 설정 (null일 수 있음)

        SetBtnStatus(BtnStatus.On);  // 버튼을 활성화 상태로 초기화

        // 버튼 클릭 시의 리스너 등록
        GetComponent<Button>().onClick.AddListener(() => StartAction());
    }

    // StartAction: 버튼 클릭 시 호출되어 코루틴을 실행
    private void StartAction()
    {
        if (_btnStatus == BtnStatus.Off) return;  // 버튼이 비활성화 상태면 클릭 방지

        SetBtnStatus(BtnStatus.Off);  // 클릭된 즉시 비활성화 상태로 전환하여 중복 방지

        // Coroutine이 null이 아닐 때만 실행
        if (_actionCoroutine != null)
        {
            // Coroutine을 실행하고 완료 대기
            StartCoroutine(WaitForCoroutineCompletion());
        }
        else
        {
            // 코루틴이 없으면 바로 완료 상태로 전환
            _isActionCompleted = true;  // 액션이 없는 경우에도 완료로 처리
        }
    }

    // WaitForCoroutineCompletion: 코루틴이 완료될 때까지 대기
    private IEnumerator WaitForCoroutineCompletion()
    {
        yield return StartCoroutine(_actionCoroutine);  // 전달받은 IEnumerator 실행 및 완료 대기

        // 액션이 완료되면 상태 업데이트
        _isActionCompleted = true;  // 액션 완료 상태 설정
    }

    // SetBtnStatus: 버튼의 상태를 변경하고 UI 업데이트, duration 추가 (기본값 1초)
    private void SetBtnStatus(BtnStatus status, float duration = .2f)
    {
        _btnStatus = status;

        switch (status)
        {
            case BtnStatus.On:
                // 활성화 상태: 글자는 흰색, CanvasGroup의 투명도는 1로
                _actionText.DOColor(Color.white, duration);  // 글자 애니메이션 적용
                _graphicGroup.DOFade(0f, duration);  // CanvasGroup 투명도 설정
                _icon.DOColor(Color.white, duration);  // 아이콘 색상 변경
                _graphicGroup.interactable = true;  // 버튼 상호작용 가능
                _graphicGroup.blocksRaycasts = true;  // 클릭 이벤트 차단 안 함
                break;

            case BtnStatus.Selected:
                // 선택 상태: 색상 반전 (글자는 검은색)
                _actionText.DOColor(Color.black, duration);  // 글자는 검은색
                _icon.DOColor(Color.black, duration);  // 아이콘도 검은색
                _graphicGroup.DOFade(1f, duration);  // CanvasGroup 투명도 설정
                break;

            case BtnStatus.Off:
                // 비활성화 상태: 전체적으로 흐리게 처리, CanvasGroup의 투명도는 0.5로
                Color fadedColor = Color.grey.ModifiedAlpha(0.5f);
                _actionText.DOColor(fadedColor, duration);  // 글자 흐리게
                _icon.DOColor(fadedColor, duration);  // 아이콘 흐리게
                _graphicGroup.DOFade(0f, duration);  // CanvasGroup 투명도 설정
                _graphicGroup.interactable = false;  // 버튼 상호작용 불가능
                _graphicGroup.blocksRaycasts = false;  // 클릭 이벤트 차단
                break;
        }
    }

    // 마우스가 버튼 위로 올라갔을 때 호출되는 함수 (IPointerEnterHandler)
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_btnStatus == BtnStatus.On)  // 버튼이 활성화된 상태에서만 Selected로 전환
        {
            SetBtnStatus(BtnStatus.Selected);
        }
    }

    // 마우스가 버튼에서 나갔을 때 호출되는 함수 (IPointerExitHandler)
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_btnStatus == BtnStatus.Selected)  // Selected 상태에서 마우스가 나가면 On으로 돌아옴
        {
            SetBtnStatus(BtnStatus.On);
        }
    }
}
