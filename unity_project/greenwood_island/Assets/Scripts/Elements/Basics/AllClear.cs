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

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        ParallelElement parallelElement = 
        new ParallelElement(
            new FXsClear(_duration),
            new SFXsClear(_duration),
            new PlaceFilmClear(_duration),
            new CameraMove2DClear(_duration),
            new CameraZoomClear(_duration),
            new AllCharactersClear(_duration),
            new PlaceOverlayFilmClear(_duration),
            new ScreenOverlayFilmClear(_duration, Ease.OutQuad, true),
            new ImaginationClear(_duration),
            new DialoguePanelClear(_duration),
            new LetterboxClear(_duration)
        );
        parallelElement.Execute();
        yield return new WaitForSeconds(_duration);
    }
}
