using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UserAction 클래스: Element를 상속받아 실행 가능한 액션을 관리
public class UserAction : Element
{
    private List<UserActionParameter> _actionParameters; // 액션 파라미터 리스트

    // 생성자: 액션 파라미터 리스트를 받아 초기화
    public UserAction(params UserActionParameter[] actionParameters)
    {
        _actionParameters = new List<UserActionParameter>(actionParameters);
    }

    // Element의 ExecuteRoutine을 오버라이드하여 액션 실행
    public override IEnumerator ExecuteRoutine()
    {
        // UIManager를 통해 UserActionUI를 열고 초기화
        var userActionUI = UIManager.Instance.SystemCanvas.UserActionUI;

        userActionUI.gameObject.SetActive(true);

        userActionUI.Init(_actionParameters); // 액션 파라미터를 통해 UI 초기화
        userActionUI.Open(.5f);
        yield return new WaitForSeconds(.5f);

        // 유저가 액션을 선택하거나 끝내기 버튼을 누를 때까지 대기
        yield return new WaitUntil(() => userActionUI.IsActionFinished);

        // UI 닫기
        userActionUI.Close(.5f);
        yield return new WaitForSeconds(.5f);
        userActionUI.gameObject.SetActive(false);
    }
}
