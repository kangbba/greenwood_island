using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// PlaceEffectRestore 클래스는 현재 활성화된 장소의 색상을 원래 상태로 복원하는 Element입니다.
/// PlaceEffect로 적용된 색상을 기본적으로 Color.white로 되돌리며, 지정된 시간과 이징 타입을 사용합니다.
/// </summary>
[System.Serializable]
public class PlaceEffectRestore : Element
{
    private float _duration;
    private Ease _easeType;

    public PlaceEffectRestore(float duration = 1f, Ease easeType = Ease.Linear)
    {
        _duration = duration;
        _easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 현재 활성화된 장소 가져오기
        Place currentPlace = PlaceManager.Instance.CurrentPlace;

        if (currentPlace == null)
        {
            Debug.LogWarning("No active place to restore the effect.");
            yield break;
        }

        // 장소의 색상을 기본 색상 (Color.white)으로 되돌리기
        if (currentPlace.Image != null)
        {
            currentPlace.Image.DOColor(Color.white, _duration).SetEase(_easeType);
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer found on the current place.");
        }

        yield return new WaitForSeconds(_duration);
    }
}
