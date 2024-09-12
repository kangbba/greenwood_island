using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class UserActionParameter
{
    private UserActionType actionType; // 액션 타입
    public Func<IEnumerator> actionCoroutine; // 코루틴 액션을 실행하는 함수

    public UserActionType ActionType => actionType;

    // 생성자: SequentialElement의 코루틴을 실행하는 Func<IEnumerator>를 설정
    public UserActionParameter(UserActionType actionType, SequentialElement sequentialElement)
    {
        this.actionType = actionType;
        // 코루틴으로 실행될 액션을 설정합니다.
        if(sequentialElement == null){
            actionCoroutine = null;
        }
        this.actionCoroutine = () => sequentialElement.ExecuteRoutine();
    }
}
