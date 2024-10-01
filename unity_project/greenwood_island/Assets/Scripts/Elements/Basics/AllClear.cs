using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AllClear : Element
{
    private float _duration;

    // AllClear 생성자에서 duration 값을 받음
    public AllClear(float duration)
    {
        _duration = duration;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 각 Clear 작업을 ParallelElement로 감싼다.
        ParallelElement parallelElement = new ParallelElement(
            new FXsClear(_duration),
            new SFXsClear(_duration),
            new PlaceFilmClear(_duration),
            new CameraMove2DClear(_duration),
            new CameraZoomClear(_duration),
            new AllCharactersClear(_duration),
            new CutInClear(_duration),
            new PlaceOverlayFilmClear(_duration),
            new ScreenOverlayFilmClear(_duration, Ease.OutQuad, true),
            new AllImaginationsClear(_duration),
            new DialoguePanelClear(_duration),
            new LetterboxClear(_duration)
        );

        // ParallelElement의 실행을 시작하고 모든 작업이 완료될 때까지 기다린다.
        parallelElement.Execute();
        // 모든 작업 완료 후 _duration 만큼 대기
        yield return new WaitForSeconds(_duration);
    }
}
