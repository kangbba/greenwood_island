using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlaceEffect : Element
{
    public enum EffectType
    {
        Shake,
        ScaleZoom,
        ShowRightward,
        ShowLeftward,
        ShowUpward,
        ShowDownward,
        MoveRestore,
        PulseInfinite,
        PulseOnce,
        ScaleBounce,
        ScaleRestore,
        FadeIn,
        FadeOut,
        Vignette,
        RestoreAll
    }

    private EffectType _effectType;
    private float _duration;
    private float _strength = 1f;
    private Ease _easeType = Ease.OutQuad;

    // 단일 EffectType 생성자
    public PlaceEffect(EffectType effectType, float duration, float strength = 1f, Ease easeType = Ease.OutQuad)
    {
        _effectType = effectType;
        _duration = duration;
        _strength = strength;
        _easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        Image _currentPlaceImage = PlaceManager.Instance.CurrentPlaceImage;
        if (_currentPlaceImage == null)
        {
            Debug.LogWarning($"Current place image is null.");
            yield break;
        }

        switch (_effectType)
        {
            case EffectType.PulseInfinite:
                _currentPlaceImage.transform
                    .DOScale(Vector3.one * (1f + _strength), _duration)
                    .SetEase(Ease.InOutQuad)
                    .SetLoops(-1, LoopType.Yoyo);  // 무한히 커졌다 작아짐
                break;

            case EffectType.PulseOnce:
                _currentPlaceImage.transform
                    .DOScale(Vector3.one * (1f + _strength), _duration / 2)
                    .SetEase(Ease.InOutQuad)
                    .SetLoops(2, LoopType.Yoyo);  // 한 번만 커졌다 작아짐
                break;

            case EffectType.ScaleBounce:
                _currentPlaceImage.transform
                    .DOScale(Vector3.one * (1f + _strength), _duration / 2)
                    .SetEase(Ease.OutBounce)
                    .OnComplete(() => _currentPlaceImage.transform.DOScale(Vector3.one, _duration / 2)); // 빠르게 커졌다가 원래 크기로 돌아옴
                break;

            case EffectType.FadeIn:
                _currentPlaceImage
                    .DOFade(1f, _duration)  // 알파값을 1로 변경 (불투명)
                    .SetEase(_easeType);
                break;

            case EffectType.FadeOut:
                _currentPlaceImage
                    .DOFade(0f, _duration)  // 알파값을 0으로 변경 (투명)
                    .SetEase(_easeType);
                break;

            case EffectType.Shake:
                _currentPlaceImage.ShakeImage(_duration, _strength);
                break;

            case EffectType.ScaleZoom:
                _currentPlaceImage.ScaleImage(Vector3.one * _strength, _duration, _easeType);
                break;
            case EffectType.ShowRightward:
                _currentPlaceImage.transform.DOMoveX(_currentPlaceImage.transform.position.x - _strength, _duration).SetEase(_easeType);
                break;

            case EffectType.ShowLeftward:
                _currentPlaceImage.transform.DOMoveX(_currentPlaceImage.transform.position.x + _strength, _duration).SetEase(_easeType);
                break;

            case EffectType.ShowUpward:
                _currentPlaceImage.transform.DOMoveY(_currentPlaceImage.transform.position.y - _strength, _duration).SetEase(_easeType);
                break;

            case EffectType.ShowDownward:
                _currentPlaceImage.transform.DOMoveY(_currentPlaceImage.transform.position.y + _strength, _duration).SetEase(_easeType);
                break;

            case EffectType.MoveRestore:
                _currentPlaceImage.MoveImage(Vector2.zero, _duration, _easeType);
                break;
            case EffectType.ScaleRestore:
                _currentPlaceImage.ScaleImage(Vector3.one, _duration, _easeType);
                break;

            case EffectType.RestoreAll:
                _currentPlaceImage.FadeImage(Color.white, _duration, _easeType);
                _currentPlaceImage.ScaleImage(Vector3.one, _duration, _easeType);
                _currentPlaceImage.MoveImage(Vector2.zero, _duration, _easeType);
                break;
        }

        // 트윈 완료 대기
        yield return new WaitForSeconds(_duration);
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;  // 즉시 실행 시 _duration을 0으로 설정
        CoroutineUtils.StartCoroutine(ExecuteRoutine());
    }

    public override void Execute()
    {
        CoroutineUtils.StartCoroutine(ExecuteRoutine());
    }
}
