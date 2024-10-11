using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// PlaceClear 클래스는 현재 활성화된 장소의 장면을 화면에서 제거하는 역할을 합니다.
/// </summary>
public class PlaceClear : Element
{
    private float _duration; // 페이드 아웃 지속 시간
    private Ease _easeType;  // 페이드 아웃 애니메이션 이징 타입

    // 생성자: 페이드 아웃 지속 시간과 이징 타입을 받아서 초기화
    public PlaceClear(float duration, Ease easeType = Ease.OutQuad)
    {
        _duration = duration;
        _easeType = easeType;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    // 장소의 장면을 제거하는 코루틴
    public override IEnumerator ExecuteRoutine()
    {
        Image currentPlaceImage = PlaceManager.Instance.CurrentPlaceImage;

        // 활성화된 장소 이미지가 없으면 경고 출력하고 중단
        if (currentPlaceImage == null)
        {
            Debug.LogWarning("No active place to clear.");
            yield break;
        }

        Debug.Log($"PlaceClear :: 장소의 장면을 제거합니다.");

        // ImageController를 통해 장소 이미지 제거
        ImageController.DestroyImage(currentPlaceImage, _duration, _easeType);

        // 애니메이션이 끝날 때까지 기다림
        yield return new WaitForSeconds(_duration);
    }
}
