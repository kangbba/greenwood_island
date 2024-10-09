using System.Collections;
using UnityEngine;

public class ImaginationInvertEffect : Element
{
    private bool _isInverted;      // 반전 효과 적용 여부 (true: 반전, false: 해제)

    // 생성자: 반전 여부와 지속 시간만 입력받음
    public ImaginationInvertEffect(bool isInverted)
    {
        _isInverted = isInverted;
    }
    public override void ExecuteInstantly()
    {
        string currentImaginationID = ImaginationManager.Instance.CurrentImaginationID;
        if (string.IsNullOrEmpty(currentImaginationID))
        {
            Debug.LogError("No active imagination to apply invert effect.");
            return;
        }
        Execute();
    }
    

    // ExecuteRoutine: 반전 효과 실행 (Coroutine)
    public override IEnumerator ExecuteRoutine()
    {
        // 현재 상상 이미지 ID 확인
        string currentImaginationID = ImaginationManager.Instance.CurrentImaginationID;

        if (string.IsNullOrEmpty(currentImaginationID))
        {
            Debug.LogError("No active imagination to apply invert effect.");
            yield break;
        }

        Debug.Log($"ImaginationInvertEffect :: {currentImaginationID} 반전 효과 실행");

        // ImaginationManager에서 색상 반전 효과를 호출
        ImaginationManager.Instance.InvertColorEffect(_isInverted);

        // 효과가 완료된 후 실행할 작업이 있으면 추가 가능
        Debug.Log($"ImaginationInvertEffect :: {currentImaginationID} 반전 효과 완료");
    }
}
