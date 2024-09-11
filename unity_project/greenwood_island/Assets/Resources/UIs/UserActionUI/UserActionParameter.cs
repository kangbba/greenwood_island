using System;
using UnityEngine;

// UserActionParameter 클래스: 버튼 타입과 액션을 설정하는 클래스
[System.Serializable]
public class UserActionParameter
{
    private UserActionType actionType; // 액션 타입
    public Action action; // 버튼이 눌렸을 때 실행될 액션

    public UserActionType ActionType { get => actionType; }

    public UserActionParameter(UserActionType actionType, Action action)
    {
        this.actionType = actionType;
        this.action = action;
    }

    public UserActionParameter(UserActionType actionType, SequentialElement sequentialElement)
    {
        this.actionType = actionType;
        this.action = () => sequentialElement.Execute();
    }
}
