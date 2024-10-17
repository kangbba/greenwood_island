using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// PlaceTransition 클래스는 장소 간의 전환 시 오버레이 색상을 사용하는 Element입니다.
/// </summary>
public class PlaceTransition : Element
{
    private string _newPlaceID; // 새로운 장소 ID
    private float _duration; // 전환 지속 시간
    private Ease _easeType; // 이징 타입
    private Color _overlayColor; // 오버레이 색상
    private List<PlaceEffect> _placeEffects; // 여러 PlaceEffect를 받는 리스트

    // 다중 PlaceEffect 생성자
    public PlaceTransition(string newPlaceID, float duration, Color overlayColor, List<PlaceEffect> placeEffects = null, Ease easeType = Ease.OutQuad)
    {
        _newPlaceID = newPlaceID;
        _duration = duration;
        _overlayColor = overlayColor == default ? Color.black : overlayColor; // 기본값은 Color.black
        _easeType = easeType;
        _placeEffects = placeEffects ?? new List<PlaceEffect>(); // placeEffects가 null이면 빈 리스트 할당
    }

    // 단일 PlaceEffect 생성자
    public PlaceTransition(string newPlaceID, float duration, Color overlayColor, PlaceEffect placeEffect, Ease easeType = Ease.OutQuad)
    {
        _newPlaceID = newPlaceID;
        _duration = duration;
        _overlayColor = overlayColor == default ? Color.black : overlayColor; // 기본값은 Color.black
        _easeType = easeType;
        _placeEffects = (placeEffect != null) ? new List<PlaceEffect> { placeEffect } : new List<PlaceEffect>(); // 단일 PlaceEffect를 리스트에 담아 초기화
    }

    public override void ExecuteInstantly()
    {
        _duration = 0; // 즉시 실행
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 현재 활성화된 장소 이미지 가져오기
        Image previousPlace = PlaceManager.Instance.CurrentPlaceImage;

        // 오버레이 생성
        yield return CoroutineUtils.StartCoroutine(new ScreenOverlayFilm(_overlayColor, _duration / 2f, _easeType).ExecuteRoutine());

        // 새로운 장소 생성
        new PlaceEnter(_newPlaceID, 0f).Execute();
        // 이전 장소 제거
        if (previousPlace != null)
        {
            Object.Destroy(previousPlace.gameObject);
        }

        // 여러 PlaceEffect를 처리하는 ParallelElement 생성
        List<Element> effectElements = new List<Element> { new ScreenOverlayFilmClear(_duration / 2f, _easeType) };
        Debug.Log(_placeEffects.Count);
        foreach (var effect in _placeEffects)
        {
            effectElements.Add(effect);
        }

        ParallelElement parallelElement = new ParallelElement(effectElements.ToArray());
        yield return CoroutineUtils.StartCoroutine(parallelElement.ExecuteRoutine());
    }
}
