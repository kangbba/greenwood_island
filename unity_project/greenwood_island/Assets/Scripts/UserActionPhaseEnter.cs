using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserActionPhaseEnter : Element
{
    private List<WorldCharacterEnter> _characterEnterList;   // 캐릭터 진입 리스트
    private Vector2 _windowAnchoredPos;
    private Dictionary<UserActionType, SequentialElement> _userActions; // 행동과 관련된 SequentialElement 사전

    public UserActionPhaseEnter()
    {
    }

    // 생성자를 통해 PlaceEnter 객체, 캐릭터 진입 데이터, UIEnter를 전달받음
    public UserActionPhaseEnter(List<WorldCharacterEnter> characterEnterList, 
                                Vector2 windowAnchoredPos, Dictionary<UserActionType, SequentialElement> userActions)
    {
       
        _characterEnterList = characterEnterList;
        _windowAnchoredPos = windowAnchoredPos;
        _userActions = userActions;
    }

    // Coroutine을 사용하여 사용자 행동 단계 실행
    public override IEnumerator ExecuteRoutine()
    {
        yield return new DialoguePanelClear().ExecuteRoutine();
        // 1. 레터박스 초기화 및 활성화
        Letterbox letterbox = UIManager.Instance.SystemCanvas.LetterBox;
        letterbox.gameObject.SetActive(true);
        letterbox.SetOn(false, 1f);

        // 3. 캐릭터 등장 실행
        foreach (var characterEnter in _characterEnterList)
        {
            yield return characterEnter.ExecuteRoutine();
        }

        // 4. UserActionWindow 활성화 및 행동 버튼 생성
        var userActionWindow = UIManager.Instance.SystemCanvas.UserActionWindow;
        if (userActionWindow == null)
        {
            Debug.LogError("UserActionWindow is not assigned.");
            yield break;
        }

        // 5. FadeIn: DOTween을 사용하여 부드럽게 나타남
        userActionWindow.gameObject.SetActive(true);
        userActionWindow.Init(_windowAnchoredPos, _userActions);
        userActionWindow.FadeIn(0.5f); // 창을 0.5초 동안 FadeIn
        yield return new WaitForSeconds(0.5f); // 0.5초 대기

        // 6. 버튼을 생성하고 사용자 액션 대기
        yield return new WaitUntil(() => userActionWindow.AreAllActionsCompleted());

        yield return new DialoguePanelClear().ExecuteRoutine();
        // 7. FadeOut: 모든 액션이 완료되면 창을 부드럽게 사라지게 함
        userActionWindow.FadeOut(0.5f); // 창을 0.5초 동안 FadeOut
        yield return new WaitForSeconds(0.5f); // 0.5초 대기
        userActionWindow.gameObject.SetActive(false);
        

    }
}
