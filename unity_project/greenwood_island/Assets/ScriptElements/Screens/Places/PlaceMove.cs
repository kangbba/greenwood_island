using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// PlaceMove 클래스는 현재 활성화된 장소의 이미지를 이동시키는 역할을 합니다.
/// </summary>
public class PlaceMove : Element
{
    private Vector2 _anchoredPos;  // 이동할 위치
    private float _duration;       // 이동 시간
    private Ease _easeType;        // 이동 애니메이션 이징 타입

    // 생성자: 이동할 위치, 지속 시간, 이징 타입을 받아서 초기화
    public PlaceMove(Vector2 anchoredPos, float duration = 1f, Ease easeType = Ease.OutQuad)
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

    // 장소 이미지를 이동시키는 코루틴
    public override IEnumerator ExecuteRoutine()
    {
        Image currentPlaceImage = PlaceManager.Instance.CurrentPlaceImage;

        if (currentPlaceImage == null)
        {
            Debug.LogError("No active place to move.");
            yield break;
        }

        Debug.Log($"PlaceMove :: 장소의 이미지를 이동 시도합니다.");

        // ImageController를 통해 이동 애니메이션 적용
        ImageController.MoveImage(currentPlaceImage, _anchoredPos, _duration, _easeType);

        // 애니메이션이 끝날 때까지 기다림
        yield return new WaitForSeconds(_duration);
    }
}
