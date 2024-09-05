using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// PlaceEffect 클래스는 현재 활성화된 장소의 색상을 직접 변경하는 Element입니다.
/// 이 클래스는 특정 장면 전환이나 연출 시, 장소 자체의 색상을 지정된 색상으로 변환하는 효과를 제공합니다.
/// 필름 오버레이와 달리 장소의 색상 자체를 변화시킨다는 점이 특징입니다.
/// 원래 색상으로 복원하는 기능은 포함되지 않으며, 외부에서 수동으로 처리할 수 있습니다.
/// </summary>
[System.Serializable]
public class PlaceFilm : Element
{
    private Color _effectColor;
    private float _duration;
    private Ease _easeType;
    

    public PlaceFilm(Color effectColor, float duration = 1f, Ease easeType = Ease.Linear)
    {
        _effectColor = effectColor;
        _duration = duration;
        _easeType = easeType;
    }

    public override IEnumerator ExecuteRoutine()
    {
        Debug.Log($"PlaceFilm {_effectColor}");

        PlaceManager.Instance.ChangeCurrentPlaceColor(_effectColor, _duration, _easeType);
        yield return new WaitForSeconds(_duration);
    }
}
