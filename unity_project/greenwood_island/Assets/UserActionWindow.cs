using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening; // DOTween을 사용하기 위해 추가

[System.Serializable]
public class UserActionButtonSettings
{
    public UserActionType actionType;  // 행동 타입 (enum)
    public Sprite actionIcon;          // 버튼에 사용할 아이콘

    public UserActionButtonSettings(UserActionType actionType, Sprite actionIcon)
    {
        this.actionType = actionType;
        this.actionIcon = actionIcon;
    }
}

public enum UserActionType
{
    Talking,       // 대화하기
    Giving,        // 물건 주기
    TakingPicture  // 사진 찍기
    // 필요한 행동을 계속 추가 가능
}

public class UserActionWindow : MonoBehaviour
{
    [SerializeField] private GameObject _userActionWindowBtnPrefab;  // 버튼 프리팹
    [SerializeField] private List<UserActionButtonSettings> _actionSettings;  // 행동 설정 리스트
    [SerializeField] private RectTransform _windowRectTransform; // UserActionWindow의 RectTransform
    [SerializeField] private Transform _btnsParent;  // 버튼 부모 오브젝트
    [SerializeField] private CanvasGroup _canvasGroup; // CanvasGroup을 사용하여 투명도 조절

    private List<UserActionWindowBtn> _actionButtons = new List<UserActionWindowBtn>();  // 생성된 버튼들 관리 리스트

    private const float ButtonSpacing = 100f;  // 버튼 간 간격 상수

    // FadeIn 메서드: DOTween을 사용하여 투명도를 부드럽게 1로 변경 (등장)
    public void FadeIn(float duration)
    {
        _canvasGroup.alpha = 0f;  // 처음에 투명하게 시작
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        _canvasGroup.DOFade(1f, duration);  // DOTween을 사용하여 부드럽게 나타남
    }

    // FadeOut 메서드: DOTween을 사용하여 투명도를 부드럽게 0으로 변경 (사라짐)
    public void FadeOut(float duration)
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        _canvasGroup.DOFade(0f, duration);  // DOTween을 사용하여 부드럽게 사라짐
    }

    // Init 메서드: SequentialElement 사전(Dictionary)을 받아 각 버튼을 생성하고 초기화
    public void Init(Vector2 anchoredPos, Dictionary<UserActionType, SequentialElement> elementDict)
    {
        _windowRectTransform.anchoredPosition = anchoredPos;
        _btnsParent.DestroyAllChildren();

        _actionButtons.Clear();  // 버튼 리스트 초기화

        // 각 행동 버튼 생성 및 초기화
        int buttonIndex = 0;
        foreach (var elementPair in elementDict)
        {
            UserActionType actionType = elementPair.Key;
            SequentialElement seqElement = elementPair.Value;

            GameObject btnObject = Instantiate(_userActionWindowBtnPrefab, _btnsParent);  // 버튼 프리팹 인스턴스화
            UserActionWindowBtn btn = btnObject.GetComponent<UserActionWindowBtn>();

            // 행동 타입에 맞는 설정 가져오기
            UserActionButtonSettings settings = GetActionSettings(actionType);

            // 버튼 초기화: 설정 및 코루틴 전달
            btn.Init(settings, seqElement.ExecuteRoutine());

            // 버튼 위치 설정: 각 버튼을 아래로 100씩 간격을 두고 배치
            RectTransform btnRectTransform = btn.GetComponent<RectTransform>();
            btnRectTransform.anchoredPosition = new Vector2(0, -buttonIndex * ButtonSpacing);  // 왼쪽 위 기준으로 Y 위치 변경

            _actionButtons.Add(btn);  // 생성된 버튼을 리스트에 추가

            buttonIndex++;  // 버튼 인덱스 증가
        }

        // UserActionWindow의 크기를 버튼 수에 맞춰 조정
        ResizeWindow(_actionButtons.Count);
    }

    // 행동 타입에 맞는 설정 반환
    private UserActionButtonSettings GetActionSettings(UserActionType actionType)
    {
        foreach (var setting in _actionSettings)
        {
            if (setting.actionType == actionType)
            {
                return setting;
            }
        }
        return null;  // 설정을 찾지 못했을 경우 null 반환
    }

    // 모든 버튼의 상태를 체크하여 완료 여부를 반환
    public bool AreAllActionsCompleted()
    {
        foreach (var btn in _actionButtons)
        {
            if (!btn.IsActionCompleted)
            {
                return false;  // 하나라도 완료되지 않았다면 false 반환
            }
        }
        return true;  // 모든 버튼이 완료되었으면 true 반환
    }

    // 윈도우 크기 조정: 버튼 수에 따라 높이를 조정
    private void ResizeWindow(int buttonCount)
    {
        float newHeight = buttonCount * ButtonSpacing;  // 버튼 수에 따라 높이 조정
        _windowRectTransform.sizeDelta = new Vector2(_windowRectTransform.sizeDelta.x, newHeight);  // 높이만 수정
    }
}
