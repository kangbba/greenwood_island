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
            new FXsClear(1f),
            new SFXsClear(1f),
            new PlaceClear(1f),
            new LetterboxClear(1f),
            new ImaginationClear(1f),
            new AllCharactersClear(1f),
            new DialoguePanelClear(1f),
            new PlaceOverlayFilmClear(1f),
            new ScreenOverlayFilmClear(1f, isBlackClear : false)
        );
        parallelElement.Execute();
        yield return new WaitForSeconds(_duration);
    }
}
