using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlaceEffect : Element
{
    public enum EffectType
    {
        Shake,
        ZoomIn,
        ZoomOut,
        ShowRightward,
        ShowLeftward,
        ShowUpward,
        ShowDownward,
        PulseInfinite,   // 무한 반복하는 Pulse 효과
        PulseOnce,       // 한 번만 실행되는 Pulse 효과
        ScaleBounce,     // 스케일 반동 효과
        FadeIn,          // 페이드 인 효과
        FadeOut          // 페이드 아웃 효과
    }

    private Image _currentPlaceImage;
    private List<EffectType> _effectTypes;
    private float _duration;
    private float _strength = 1f;
    private Ease _easeType = Ease.OutQuad;

    // 단일 EffectType 생성자
    public PlaceEffect(EffectType effectType, float duration, float strength = 1f, Ease easeType = Ease.OutQuad)
    {
        _effectTypes = new List<EffectType> { effectType };
        _duration = duration;
        _strength = strength;
        _easeType = easeType;
        _currentPlaceImage = PlaceManager.Instance.CurrentPlaceImage;
    }

    // 다중 EffectType 생성자
    public PlaceEffect(List<EffectType> effectTypes, float duration, float strength = 1f, Ease easeType = Ease.OutQuad)
    {
        _effectTypes = effectTypes;
        _duration = duration;
        _strength = strength;
        _easeType = easeType;
        _currentPlaceImage = PlaceManager.Instance.CurrentPlaceImage;
    }

    public override IEnumerator ExecuteRoutine()
    {
        if (_currentPlaceImage == null)
        {
            Debug.LogError("Current place image is null.");
            yield break;
        }

        List<Tween> tweens = new List<Tween>();

        // 각 효과를 동시에 적용
        foreach (var effectType in _effectTypes)
        {
            switch (effectType)
            {
                case EffectType.PulseInfinite:
                    tweens.Add(_currentPlaceImage.transform
                        .DOScale(Vector3.one * (1f + _strength), _duration / 2)
                        .SetEase(Ease.InOutQuad)
                        .SetLoops(-1, LoopType.Yoyo));  // 무한히 커졌다 작아짐
                    break;

                case EffectType.PulseOnce:
                    tweens.Add(_currentPlaceImage.transform
                        .DOScale(Vector3.one * (1f + _strength), _duration / 2)
                        .SetEase(Ease.InOutQuad)
                        .SetLoops(2, LoopType.Yoyo));  // 한 번만 커졌다 작아짐
                    break;

                case EffectType.ScaleBounce:
                    tweens.Add(_currentPlaceImage.transform
                        .DOScale(Vector3.one * (1f + _strength), _duration / 3)
                        .SetEase(Ease.OutBounce)
                        .OnComplete(() => _currentPlaceImage.transform.DOScale(Vector3.one, _duration / 3))); // 빠르게 커졌다가 원래 크기로 돌아옴
                    break;

                case EffectType.FadeIn:
                    tweens.Add(_currentPlaceImage
                        .DOFade(1f, _duration)  // 알파값을 1로 변경 (불투명)
                        .SetEase(_easeType));
                    break;

                case EffectType.FadeOut:
                    tweens.Add(_currentPlaceImage
                        .DOFade(0f, _duration)  // 알파값을 0으로 변경 (투명)
                        .SetEase(_easeType));
                    break;

                case EffectType.Shake:
                    tweens.Add(_currentPlaceImage.ShakeImage(_duration, _strength));
                    break;

                case EffectType.ZoomIn:
                    tweens.Add(_currentPlaceImage.ScaleImage(Vector3.one * (1f + _strength), _duration, _easeType));
                    break;

                case EffectType.ZoomOut:
                    tweens.Add(_currentPlaceImage.ScaleImage(Vector3.one * (1f - _strength), _duration, _easeType));
                    break;

                case EffectType.ShowRightward:
                    tweens.Add(_currentPlaceImage.transform.DOMoveX(_currentPlaceImage.transform.position.x - _strength, _duration).SetEase(_easeType));
                    break;

                case EffectType.ShowLeftward:
                    tweens.Add(_currentPlaceImage.transform.DOMoveX(_currentPlaceImage.transform.position.x + _strength, _duration).SetEase(_easeType));
                    break;

                case EffectType.ShowUpward:
                    tweens.Add(_currentPlaceImage.transform.DOMoveY(_currentPlaceImage.transform.position.y - _strength, _duration).SetEase(_easeType));
                    break;

                case EffectType.ShowDownward:
                    tweens.Add(_currentPlaceImage.transform.DOMoveY(_currentPlaceImage.transform.position.y + _strength, _duration).SetEase(_easeType));
                    break;
            }
        }

        // 모든 트윈이 완료될 때까지 대기
        yield return DOTween.Sequence().AppendInterval(_duration);
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
