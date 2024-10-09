using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserActionPhaseEnter : Element
{
    private StorySavedData _storySavedData; // StorySavedData 인풋
    private string _userActionID; // UserActionPhaseEnter의 고유 ID
    private UserActionWindow.AnchorType _anchorType;  // AnchorType 필드를 전역 변수로 추가
    private Vector2 _windowAnchoredPos;
    private Dictionary<UserActionType, SequentialElement> _userActions;

    // 생성자에서 AnchorType과 _windowAnchoredPos, _userActionID, StorySavedData를 모두 받음
    public UserActionPhaseEnter(StorySavedData storySavedData, string userActionID, Dictionary<UserActionType, SequentialElement> userActions, UserActionWindow.AnchorType anchorType, Vector2 windowAnchoredPos)
    {
        _storySavedData = storySavedData;
        _userActionID = userActionID; // UserActionPhaseEnter의 고유 ID 저장
        _anchorType = anchorType;  // 전역 변수로 저장
        _windowAnchoredPos = windowAnchoredPos; // AnchorType을 통해 기본 위치 설정
        _userActions = userActions;
    }

    public override void ExecuteInstantly()
    {

        // StorySavedData가 유효하지 않으면 즉시 리턴
        if (_storySavedData == null)
        {
            Debug.LogError("StorySavedData가 유효하지 않음.");
            return;
        }

        // DialoguePanel과 Letterbox 클리어 작업을 즉시 수행
        new DialoguePanelClear().ExecuteInstantly();
        new LetterboxClear().ExecuteInstantly();

        // StorySavedData에서 해당 userActionID에 대한 ActionType 리스트 가져오기
        List<UserActionType> completedActions = _storySavedData.GetUserActionTypeFromHistory(_userActionID);

        // 기록된 수행 액션이 없으면 경고하고 리턴
        if (completedActions.Count == 0)
        {
            Debug.LogWarning($"userActionID {_userActionID}에 대한 기록된 수행 액션이 없습니다.");
            return;
        }

        // 기록된 ActionType에 대해 즉시 실행 처리
        foreach (var actionType in completedActions)
        {
            if (!_userActions.ContainsKey(actionType))
            {
                Debug.LogError($"UserActionType {actionType}에 해당하는 액션이 존재하지 않습니다.");
                continue;
            }

            _userActions[actionType].ExecuteInstantly();
        }

    }

    // Coroutine을 사용하여 사용자 행동 단계 실행
    public override IEnumerator ExecuteRoutine()
    {
        yield return new DialoguePanelClear().ExecuteRoutine();
        yield return new LetterboxClear().ExecuteRoutine();

        var userActionWindow = UIManager.SystemCanvas.InstantiateUserActionWindow();
        if (userActionWindow == null)
        {
            Debug.LogError("UserActionWindow is not assigned.");
            yield break;
        }
        if (_userActions == null)
        {
            Debug.LogError("_userActions is not assigned.");
            yield break;
        }
        if (_userActions.Values == null)
        {
            Debug.LogError("_userActions.Values is not assigned.");
            yield break;
        }

        userActionWindow.Init(_anchorType, _windowAnchoredPos, _userActions); // AnchorType과 _windowAnchoredPos를 사용
        userActionWindow.FadeIn(0.5f);
        yield return new WaitForSeconds(0.5f);

        // 사용자의 행동 완료를 기다림
        yield return new WaitUntil(() => userActionWindow.AreAllActionsCompleted());

        userActionWindow.FadeOutAndDestroy(.5f);
        yield return new WaitForSeconds(.5f);
    }
}
