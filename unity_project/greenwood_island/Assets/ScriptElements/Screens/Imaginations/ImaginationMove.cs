using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// ImaginationMove 클래스는 현재 활성화된 상상의 이미지를 화면에서 이동시키는 역할을 합니다.
/// </summary>
public class ImaginationMove : Element
{
    private Vector2 _anchoredPos;  // 이동할 위치
    private float _duration;       // 이동 시간
    private Ease _easeType;        // 이동 애니메이션 이징 타입

    // 생성자: 이동할 위치, 지속 시간, 이징 타입을 받아서 초기화
    public ImaginationMove(Vector2 anchoredPos, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _anchoredPos = anchoredPos;
        _duration = duration;
        _easeType = easeType;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    // 상상을 이동시키는 코루틴
    public override IEnumerator ExecuteRoutine()
    {
        Image currentImaginationImage = ImaginationManager.Instance.CurrentImaginationImage;

        if (currentImaginationImage == null)
        {
            Debug.LogError("No active imagination to move.");
            yield break;
        }

        Debug.Log($"ImaginationMove :: 상상의 이미지를 이동 시도합니다.");

        // ImageController를 통해 이동 애니메이션 적용
        currentImaginationImage.MoveImage(_anchoredPos, _duration, _easeType);

        // 애니메이션이 끝날 때까지 기다림
        yield return new WaitForSeconds(_duration);
    }
}
