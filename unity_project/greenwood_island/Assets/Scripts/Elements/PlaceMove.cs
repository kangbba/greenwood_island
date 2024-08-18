using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum PlaceAnimType
{
    FadeIn,
    FadeOut,
    ScreenFilmBlackIn,       // 검은색 필름에서 페이드 인
    ScreenFilmBlackOut,      // 검은색 필름으로 페이드 아웃
    ScreenFilmWhiteIn,       // 흰색 필름에서 페이드 인
    ScreenFilmWhiteOut,      // 흰색 필름으로 페이드 아웃
    Blackout,                // 실제 Place의 색상을 검게 바꾸면서 페이드 아웃
    BlackIn,                 // 실제 Place의 색상을 검게 바꿨다가 페이드 인
    Overlay,                 // 오버레이 효과로 등장
}

[System.Serializable]
public class PlaceTransition
{
    public EPlaceID PlaceID { get; private set; }
    public PlaceAnimType AnimType { get; private set; }
    public float Duration { get; private set; }
    public Ease EaseType { get; private set; }

    public PlaceTransition(EPlaceID placeID, PlaceAnimType animType, float duration, Ease easeType)
    {
        PlaceID = placeID;
        AnimType = animType;
        Duration = duration;
        EaseType = easeType;
    }
}

[System.Serializable]
public class PlaceMove : Element
{
    private List<PlaceTransition> _transitions;

    // ScreenOverlayFilm 참조를 람다식으로 사용
    private Image ScreenOverlayFilm => PlaceManager.Instance.ScreenOverlayFilm;

    public PlaceMove(List<PlaceTransition> transitions)
    {
        _transitions = transitions;
    }

    public override IEnumerator Execute()
    {
        foreach (var transition in _transitions)
        {
            Place place = PlaceManager.Instance.GetActivePlace(transition.PlaceID);
            if (place == null)
            {
                place = PlaceManager.Instance.InstantiatePlace(transition.PlaceID);
                if (place == null)
                {
                    Debug.LogWarning($"Failed to instantiate place with ID: {transition.PlaceID}");
                    yield break;
                }
                InitializePlaceState(place, transition);
            }

            yield return PerformAnimation(place, transition);
        }
    }

    private void InitializePlaceState(Place place, PlaceTransition transition)
    {
        switch (transition.AnimType)
        {
            case PlaceAnimType.ScreenFilmBlackIn:
            case PlaceAnimType.ScreenFilmWhiteIn:
                ScreenOverlayFilm.color = (transition.AnimType == PlaceAnimType.ScreenFilmBlackIn) ? Color.black : Color.white;
                ScreenOverlayFilm.gameObject.SetActive(true);
                ScreenOverlayFilm.DOFade(1f, 0f);
                break;

            case PlaceAnimType.BlackIn:
                place.SetColor(Color.black, 0f);
                place.SetVisibility(true, 0f);
                break;

            case PlaceAnimType.Overlay:
                place.SetColor(Color.white, 0f);
                place.SetVisibility(false, 0f);
                break;

            default:
                Debug.LogWarning($"Unsupported PlaceAnimType: {transition.AnimType}");
                break;
        }
    }

    private IEnumerator PerformAnimation(Place place, PlaceTransition transition)
    {
        switch (transition.AnimType)
        {
            case PlaceAnimType.ScreenFilmBlackIn:
                yield return ScreenOverlayFilm.DOFade(0f, transition.Duration).SetEase(transition.EaseType).WaitForCompletion();
                ScreenOverlayFilm.gameObject.SetActive(false);
                place.SetVisibility(true, 0f);
                break;

            case PlaceAnimType.ScreenFilmWhiteIn:
                yield return ScreenOverlayFilm.DOFade(0f, transition.Duration).SetEase(transition.EaseType).WaitForCompletion();
                ScreenOverlayFilm.gameObject.SetActive(false);
                place.SetVisibility(true, 0f);
                break;

            case PlaceAnimType.ScreenFilmBlackOut:
                place.SetVisibility(true, 0f);
                place.SetColor(Color.white, 0f);
                yield return ScreenOverlayFilm.DOFade(1f, transition.Duration).SetEase(transition.EaseType).WaitForCompletion();
                break;

            case PlaceAnimType.ScreenFilmWhiteOut:
                place.SetVisibility(true, 0f);
                place.SetColor(Color.clear, 0f);
                yield return ScreenOverlayFilm.DOFade(1f, transition.Duration).SetEase(transition.EaseType).WaitForCompletion();
                break;

            case PlaceAnimType.Blackout:
                place.SetVisibility(true, 0f);
                yield return place.SetColor(Color.black, transition.Duration).SetEase(transition.EaseType).WaitForCompletion();
                break;

            case PlaceAnimType.BlackIn:
                place.SetColor(Color.black, 0f);
                place.SetVisibility(true, 0f);
                yield return place.SetColor(Color.white, transition.Duration).SetEase(transition.EaseType).WaitForCompletion();
                break;

            case PlaceAnimType.FadeIn:
                yield return place.SetVisibility(true, transition.Duration).SetEase(transition.EaseType).WaitForCompletion();
                break;

            case PlaceAnimType.FadeOut:
                yield return place.SetVisibility(false, transition.Duration).SetEase(transition.EaseType).WaitForCompletion();
                break;

            case PlaceAnimType.Overlay:
                yield return place.SetVisibility(true, transition.Duration).SetEase(transition.EaseType).WaitForCompletion();
                break;

            default:
                Debug.LogWarning($"Unsupported PlaceAnimType: {transition.AnimType}");
                yield break;
        }

        // 오버레이 필름 제거
        if (transition.AnimType == PlaceAnimType.ScreenFilmBlackOut || transition.AnimType == PlaceAnimType.ScreenFilmWhiteOut)
        {
            ScreenOverlayFilm.color = Color.clear;
            ScreenOverlayFilm.DOFade(0f, 0.5f).OnComplete(() => ScreenOverlayFilm.gameObject.SetActive(false));
        }
    }
}
