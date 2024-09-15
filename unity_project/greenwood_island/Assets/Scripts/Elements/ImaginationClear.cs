using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ImaginationClear 클래스는 상상의 장면을 화면에서 제거하는 역할을 합니다.
/// </summary>
public class ImaginationClear : Element
{
    private float _duration; // 페이드 아웃 지속 시간
    private Ease _easeType;  // 페이드 아웃 애니메이션 이징 타입

    // 생성자: 페이드 아웃 지속 시간과 이징 타입을 받아서 초기화
    public ImaginationClear(float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _duration = duration;
        _easeType = easeType;
    }

    // 상상의 장면을 제거하는 코루틴
    public override IEnumerator ExecuteRoutine()
    {
        Debug.Log("ImaginationClear :: 상상의 장면을 제거합니다.");

        ImaginationManager.DestroyAllImaginations(_duration, _easeType);

        // 애니메이션이 끝날 때까지 기다림
        yield return new WaitForSeconds(_duration);
    }
}
