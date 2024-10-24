using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImaginationInvertEffect : Element
{
    private bool _isInverted;  // 반전 효과 적용 여부 (true: 반전, false: 해제)

    // 생성자: 반전 여부만 입력받음
    public ImaginationInvertEffect(bool isInverted)
    {
        _isInverted = isInverted;
    }

    public override void ExecuteInstantly()
    {
        // 현재 상상 이미지 확인
        Image currentImaginationImage = ImaginationManager.Instance.CurrentImaginationImage;
        if (currentImaginationImage == null)
        {
            Debug.LogError("No active imagination to apply invert effect.");
            return;
        }

        // 반전 효과 즉시 적용 (ImaginationManager에서 처리)
        ImaginationManager.Instance.InvertColorEffect(_isInverted);
    }

    // 반전 효과 실행 (Coroutine)
    public override IEnumerator ExecuteRoutine()
    {
        // 현재 상상 이미지 확인
        Image currentImaginationImage = ImaginationManager.Instance.CurrentImaginationImage;

        if (currentImaginationImage == null)
        {
            Debug.LogError("No active imagination to apply invert effect.");
            yield break;
        }

        Debug.Log($"ImaginationInvertEffect :: 반전 효과 실행");

        // ImaginationManager에서 반전 효과 호출
        ImaginationManager.Instance.InvertColorEffect(_isInverted);

        // 필요한 경우 효과 적용 후 추가 작업 가능
        yield return new WaitForSeconds(.1f);

        Debug.Log($"ImaginationInvertEffect :: 반전 효과 완료");
    }
}
