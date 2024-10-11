using System.Collections;
using UnityEngine;

public class CutInEnter : Element
{
    private Sprite _highlightSprite;  // 강조할 이미지 스프라이트
    private float _highlightScaleFactor; // 강조할 이미지의 오프셋
    private Vector2 _highlightOffset; // 강조할 이미지의 오프셋
    private float _duration;          // 애니메이션 지속 시간

    // 생성자: 강조할 스프라이트, 지속 시간, 오프셋을 받음
    public CutInEnter(Sprite highlightSprite, float duration, float highlightScaleFactor, Vector2 highlightOffset)
    {
        _highlightSprite = highlightSprite;
        _duration = duration;
        _highlightScaleFactor = highlightScaleFactor;
        _highlightOffset = highlightOffset;
    }
    public override void ExecuteInstantly()
    {
    }

    public override IEnumerator ExecuteRoutine()
    {
        // UIManager를 통해 CutInUI를 가져옴
        var _cutInUI = UIManager.SystemCanvas.InstantiateCutInUI();

        const float enterDuration = .5f;
        // CutInUI 초기화
        _cutInUI.Init(_highlightSprite, _highlightScaleFactor, _highlightOffset); // 배경 색상은 기본적으로 설정된 색상 사용
        // CutInUI 등장 애니메이션 실행
        _cutInUI.FadeIn(enterDuration);

        // 애니메이션이 지속되는 동안 대기
        yield return new WaitForSeconds(_duration); 
        // CutInUI 퇴장 애니메이션 실행

        const float exitDuration = .5f;

        _cutInUI.FadeOutAndDestroy(exitDuration);
        // 애니메이션이 지속되는 동안 대기
        yield return new WaitForSeconds(exitDuration);

    }
}
