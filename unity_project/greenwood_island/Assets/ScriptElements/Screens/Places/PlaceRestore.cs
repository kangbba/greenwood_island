using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// PlaceRestore 클래스는 현재 활성화된 장소의 특정 속성을 복구하는 Element입니다.
/// </summary>
public class PlaceRestore : Element
{
    private Vector2 _localScale;    // 복구할 스케일
    private Vector2 _anchoredPos;   // 복구할 앵커 위치
    private Color _targetColor;      // 복구할 색상
    private float _duration;         // 지속 시간
    private Ease _easeType;          // 이징 타입

    // 생성자: 복구할 스케일, 위치, 색상, 지속 시간, 이징 타입을 받아서 초기화
    public PlaceRestore(Vector2 localScale = default, Vector2 anchoredPos = default, Color targetColor = default, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _localScale = localScale == default ? Vector2.one : localScale;
        _anchoredPos = anchoredPos == default ? Vector2.zero : anchoredPos;
        _targetColor = targetColor == default ? Color.white : targetColor;
        _duration = duration;
        _easeType = easeType;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 현재 활성화된 장소 가져오기
        Image currentPlace = PlaceManager.Instance.CurrentPlaceImage;
        if (currentPlace == null)
        {
            Debug.LogWarning("PlaceRestore :: 현재 활성화된 장소를 찾지 못했습니다.");
            yield break;
        }
        new PlaceColor(_targetColor, _duration, _easeType).Execute();
        new PlaceMove(_anchoredPos, _duration, _easeType).Execute();
        new PlaceScale(_localScale, _duration, _easeType).Execute();

        // 애니메이션이 끝날 때까지 기다림
        yield return new WaitForSeconds(_duration);
    }
}
