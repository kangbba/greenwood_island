using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// PlaceColor 클래스는 현재 활성화된 장소의 이미지를 색상 변경하는 역할을 합니다.
/// </summary>
public class PlaceColor : Element
{
    private Color _targetColor;    // 목표 색상
    private float _duration;       // 색상 전환 시간
    private Ease _easeType;        // 색상 전환 애니메이션 이징 타입

    // 생성자: 목표 색상, 지속 시간, 이징 타입을 받아서 초기화
    public PlaceColor(Color targetColor, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _targetColor = targetColor;
        _duration = duration;
        _easeType = easeType;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    // 장소 이미지를 색상 변경하는 코루틴
    public override IEnumerator ExecuteRoutine()
    {
        Image currentPlaceImage = PlaceManager.Instance.CurrentPlaceImage;

        if (currentPlaceImage == null)
        {
            Debug.LogError("No active place to change color.");
            yield break;
        }

        Debug.Log($"PlaceColor :: 장소의 색상을 {_targetColor}로 변경 시도합니다.");

        // ImageController를 통해 색상 변경 애니메이션 적용
        ImageController.FadeColor(currentPlaceImage, _targetColor, _duration, _easeType);

        // 애니메이션이 끝날 때까지 기다림
        yield return new WaitForSeconds(_duration);
    }
}
