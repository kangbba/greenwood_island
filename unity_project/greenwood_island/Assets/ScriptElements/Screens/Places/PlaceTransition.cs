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
    private PlaceEnter _placeEnter; // 새로운 장소 ID
    private List<PlaceEffect> _placeEffects; // 여러 PlaceEffect를 받는 리스트
    private Color _overlayColor; // 오버레이 색상
    private Ease _easeType; // 이징 타입
    private float _enterOverlayDuration; // 진입 시 오버레이 지속 시간
    private float _exitOverlayDuration; // 종료 시 오버레이 지속 시간

    // 다중 PlaceEffect 생성자
    public PlaceTransition(PlaceEnter placeEnter, Color overlayColor, List<PlaceEffect> placeEffects = null, float enterOverlayDuration = 1f, float exitOverlayDuration = 1f, Ease easeType = Ease.OutQuad)
    {
        _placeEnter = placeEnter;
        _placeEffects = placeEffects ?? new List<PlaceEffect>(); // placeEffects가 null이면 빈 리스트 할당
        _overlayColor = overlayColor == default ? Color.black : overlayColor; // 기본값은 Color.black
        _enterOverlayDuration = enterOverlayDuration; // 기본값 1초
        _exitOverlayDuration = exitOverlayDuration; // 기본값 1초
        _easeType = easeType;
    }

    // 단일 PlaceEffect 생성자
    public PlaceTransition(PlaceEnter placeEnter, Color overlayColor, PlaceEffect placeEffect, float enterOverlayDuration = 1f, float exitOverlayDuration = 1f, Ease easeType = Ease.OutQuad)
    {
        _placeEnter = placeEnter;
        _placeEffects = (placeEffect != null) ? new List<PlaceEffect> { placeEffect } : new List<PlaceEffect>(); // 단일 PlaceEffect를 리스트에 담아 초기화
        _overlayColor = overlayColor == default ? Color.black : overlayColor; // 기본값은 Color.black
        _enterOverlayDuration = enterOverlayDuration; // 기본값 1초
        _exitOverlayDuration = exitOverlayDuration; // 기본값 1초
        _easeType = easeType;
    }

    public override void ExecuteInstantly()
    {
        _enterOverlayDuration = 0;
        _exitOverlayDuration = 0;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 현재 활성화된 장소 이미지 가져오기
        Image previousPlace = PlaceManager.Instance.CurrentPlaceImage;

        // 오버레이 생성 (진입 시)
        yield return CoroutineUtils.StartCoroutine(new ScreenOverlayFilm(_overlayColor, _enterOverlayDuration, _easeType).ExecuteRoutine());

        // 새로운 장소 생성
        _placeEnter.Execute();

        // 이전 장소 제거
        if (previousPlace != null)
        {
            Object.Destroy(previousPlace.gameObject);
        }

        // 여러 PlaceEffect를 처리하는 ParallelElement 생성
        List<Element> effectElements = new List<Element> { new ScreenOverlayFilmClear(_exitOverlayDuration, _easeType) };
        Debug.Log(_placeEffects.Count);
        foreach (var effect in _placeEffects)
        {
            effectElements.Add(effect);
        }

        ParallelElement parallelElement = new ParallelElement(effectElements.ToArray());
        yield return CoroutineUtils.StartCoroutine(parallelElement.ExecuteRoutine());
    }
}
