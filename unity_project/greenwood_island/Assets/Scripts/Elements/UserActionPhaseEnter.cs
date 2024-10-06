using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UserActionPhaseEnter : Element
{
    private UserActionWindow.AnchorType _anchorType;  // AnchorType 필드를 전역 변수로 추가
    private Vector2 _windowAnchoredPos;
    private Dictionary<UserActionType, SequentialElement> _userActions;

    // 생성자에서 AnchorType과 _windowAnchoredPos를 모두 받음
    public UserActionPhaseEnter(UserActionWindow.AnchorType anchorType, Vector2 windowAnchoredPos, Dictionary<UserActionType, SequentialElement> userActions)
    {
        _anchorType = anchorType;  // 전역 변수로 저장
        _windowAnchoredPos = windowAnchoredPos; // AnchorType을 통해 기본 위치 설정
        _userActions = userActions;
    }

    // Coroutine을 사용하여 사용자 행동 단계 실행
    public override IEnumerator ExecuteRoutine()
    {
        yield return new DialoguePanelClear().ExecuteRoutine();
        yield return new LetterboxClear().ExecuteRoutine();

        var userActionWindow = UIManager.SystemCanvas.UserActionWindow;
        if (userActionWindow == null)
        {
            Debug.LogError("UserActionWindow is not assigned.");
            yield break;
        }
        if(_userActions == null){
            Debug.LogError("_userActions is not assigned.");
            yield break;
        }
        if(_userActions.Values == null){
            Debug.LogError("_userActions.Values is not assigned.");
            yield break;
        }

        // UserActionWindow 활성화 및 Init 호출
        userActionWindow.gameObject.SetActive(true);
        userActionWindow.Init(_anchorType, _windowAnchoredPos, _userActions); // AnchorType과 _windowAnchoredPos를 사용
        userActionWindow.FadeIn(0.5f);
        yield return new WaitForSeconds(0.5f);

        yield return new WaitUntil(() => userActionWindow.AreAllActionsCompleted());

        yield return new DialoguePanelClear().ExecuteRoutine();
        userActionWindow.FadeOut(0.5f);
        yield return new WaitForSeconds(0.5f);
        userActionWindow.gameObject.SetActive(false);
    }
}
