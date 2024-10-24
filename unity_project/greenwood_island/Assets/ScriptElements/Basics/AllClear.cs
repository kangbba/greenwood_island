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
            new BGMsClear(_duration),
            new VignetteExit(_duration),
            new PlaceClear(_duration),
            new LetterboxClear(_duration),
            new ImaginationClear(_duration),
            new AllCharactersClear(_duration),
            new DialoguePanelClear(_duration),
            new PlaceOverlayFilmClear(_duration),
            new ScreenOverlayFilmClear(_duration, isBlackClear : false)
        );
        parallelElement.Execute();
        yield return new WaitForSeconds(_duration);
    }
}
