using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;
using System.Collections;

// UserActionType Enum 정의
public enum UserActionType
{
    Exit = 0,
    Talk,
    Search,
    Gift,
}

// UserActionConfig 클래스: 각 액션 타입에 대한 설정을 관리하는 클래스
[System.Serializable]
public class UserActionConfig
{
    public UserActionType actionType; // 액션 타입
    public Sprite icon; // 버튼에 사용할 아이콘
    public string displayName; // 버튼에 표시될 텍스트
}

// UserActionUI 클래스 정의
public class UserActionUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Transform _btnParent; // 액션 버튼들이 배치될 부모 패널
    [SerializeField] private TextMeshProUGUI _titleText; // 상단 타이틀 텍스트
    [SerializeField] private UserActionBtn _userActionBtnPrefab; // UserActionButton 프리팹
    [SerializeField] private Image _backgroundImg; // 패널의 배경 이미지
    [SerializeField] private Image _backgroundBorderImg; // 패널의 배경 테두리 이미지
    [SerializeField] private CanvasGroup _canvasGroup; // 페이드 인/아웃을 위한 CanvasGroup
    [SerializeField] private List<UserActionConfig> _actionConfigs; // 각 액션 타입의 설정 리스트

    private List<UserActionBtn> _activeButtons = new List<UserActionBtn>(); // 현재 활성화된 버튼들
    private Dictionary<UserActionType, UserActionBtn> _activeBtnsDictionary = new Dictionary<UserActionType, UserActionBtn>();
    private const float BUTTON_HEIGHT = 150; // 버튼당 높이
    private const float TOP_PADDING = 100; // 위 여백
    private const float BOTTOM_PADDING = 100; // 아래 여백
    private float _initialHeight; // 패널의 초기 높이

    public bool IsActionFinished
    {
        get
        {
            // Exit 버튼이 실행되었는지 확인
            if (_activeBtnsDictionary.ContainsKey(UserActionType.Exit) && _activeBtnsDictionary[UserActionType.Exit].IsActionExecuted)
            {
                return true;
            }

            // 모든 버튼이 실행되었는지 확인
            bool allActionsExecuted = _activeButtons.All(button => button.IsActionExecuted);
            
            return allActionsExecuted;
        }
    }


    private void Awake()
    {
        // 초기화 시 UI 비활성화
        _canvasGroup.alpha = 0f; // 초기 상태에서 투명하게 설정
    }

    // UI 초기화 메서드: 액션 파라미터 리스트를 받아 설정
    public void Init(List<UserActionParameter> actionParameters)
    {
        // 기존 버튼 제거
        ClearButtons();

        bool hasExitAction = actionParameters.Exists(param => param.ActionType == UserActionType.Exit);
        if(!hasExitAction){
            actionParameters.Add(new UserActionParameter(UserActionType.Exit, null));
        }

        // 패널 크기를 먼저 계산하여 설정
        float totalHeight = TOP_PADDING + actionParameters.Count * BUTTON_HEIGHT + BOTTOM_PADDING;
        AdjustPanelSize(totalHeight);

        // 액션 파라미터에 따른 버튼 생성
        for (int i = 0; i < actionParameters.Count; i++)
        {
            CreateActionButton(actionParameters[i].ActionType, actionParameters[i].actionCoroutine, i);
        }

    }

    // UI 열기 메서드
    public void Open(float duration)
    {
        // 애니메이션 시작 전 초기화
        SetInitialSize(0); // 배경 이미지 크기를 0으로 설정하여 말려있는 상태로 시작
        _canvasGroup.alpha = 1f; // 캔버스 그룹을 보이도록 설정

        // 배경 이미지와 테두리를 펼치는 애니메이션
        _backgroundImg.rectTransform.DOSizeDelta(new Vector2(_backgroundImg.rectTransform.sizeDelta.x, _initialHeight), duration).SetEase(Ease.OutQuad);
        _backgroundBorderImg.rectTransform.DOSizeDelta(new Vector2(_backgroundBorderImg.rectTransform.sizeDelta.x, _initialHeight), duration).SetEase(Ease.OutQuad);
    }

    // UI 닫기 메서드
    public void Close(float duration)
    {
        // 배경 이미지와 테두리를 말아 올리듯 닫는 애니메이션
        _backgroundImg.rectTransform.DOSizeDelta(new Vector2(_backgroundImg.rectTransform.sizeDelta.x, 0), duration).SetEase(Ease.InQuad)
            .OnComplete(() => _canvasGroup.alpha = 0f); // 애니메이션 완료 후 캔버스 그룹 숨기기
        _backgroundBorderImg.rectTransform.DOSizeDelta(new Vector2(_backgroundBorderImg.rectTransform.sizeDelta.x, 0), duration).SetEase(Ease.InQuad);
    }

    // 기존 버튼들을 제거하는 메서드
    private void ClearButtons()
    {
        foreach (var button in _activeButtons)
        {
            Destroy(button.gameObject);
        }
        _activeButtons.Clear();
    }
    private void CreateActionButton(UserActionType actionType, Func<IEnumerator> actionCoroutine, int index)
    {
        var config = _actionConfigs.Find(c => c.actionType == actionType);

        if (config == null)
        {
            Debug.LogWarning($"Configuration not found for action type: {actionType}");
            return;
        }

        UserActionBtn newButton = Instantiate(_userActionBtnPrefab, _btnParent);
        newButton.Init(config.icon, config.displayName, () => ButtonActionCoroutine(actionType, actionCoroutine));

        RectTransform buttonRect = newButton.GetComponent<RectTransform>();
        buttonRect.anchoredPosition = new Vector2(0, -index * BUTTON_HEIGHT);

        _activeButtons.Add(newButton);
        _activeBtnsDictionary.Add(actionType, newButton);
    }

    private IEnumerator ButtonActionCoroutine(UserActionType actionType, Func<IEnumerator> actionCoroutine)
    {
        if(actionCoroutine == null){
            yield break;
        }
        Close(0.5f); // UI를 닫는 애니메이션 실행
        yield return new WaitForSeconds(0.5f); // 닫는 애니메이션 대기

        yield return StartCoroutine(actionCoroutine?.Invoke()); // 액션 코루틴 실행
        Open(0.5f); // UI를 여는 애니메이션 실행
        yield return new WaitForSeconds(0.5f); // 여는 애니메이션 대기
    }



    // 패널의 크기를 설정하는 메서드
    private void AdjustPanelSize(float totalHeight)
    {
        _backgroundImg.rectTransform.sizeDelta = new Vector2(_backgroundImg.rectTransform.sizeDelta.x, totalHeight);
        _backgroundBorderImg.rectTransform.sizeDelta = new Vector2(_backgroundBorderImg.rectTransform.sizeDelta.x, totalHeight);
        _initialHeight = totalHeight; // 초기 높이를 최신 높이로 업데이트
    }

    // 배경 이미지 크기 초기화 메서드
    private void SetInitialSize(float height)
    {
        _backgroundImg.rectTransform.sizeDelta = new Vector2(_backgroundImg.rectTransform.sizeDelta.x, height);
        _backgroundBorderImg.rectTransform.sizeDelta = new Vector2(_backgroundBorderImg.rectTransform.sizeDelta.x, height);
    }
}
