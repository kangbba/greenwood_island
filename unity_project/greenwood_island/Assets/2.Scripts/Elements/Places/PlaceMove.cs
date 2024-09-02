using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// PlaceMove 클래스는 Unity 프로젝트에서 장소 전환을 관리하는 Element입니다.
/// 이 클래스는 특정 장소로의 전환을 애니메이션과 함께 수행하며, 필요에 따라 화면 오버레이 효과를 사용하여 
/// 부드러운 전환을 제공합니다. 
/// 새로운 장소가 활성화되기 전에 기존 장소를 파괴하고, 화면 오버레이 필름을 통해 전환 효과를 
/// 시각적으로 연출합니다.
/// </summary>
public class PlaceMove : Element
{
    private EPlaceID _newPlaceID; // 이동할 새로운 장소의 ID
    private float _duration = 1.0f; // 전환에 걸리는 시간
    private Ease _transitionEase = Ease.InOutQuad; // 전환 애니메이션의 Ease 타입

    // 생성자 추가
    public PlaceMove(EPlaceID newPlaceID, float duration, Ease transitionEase)
    {
        _newPlaceID = newPlaceID;
        _duration = duration;
        _transitionEase = transitionEase;
    }

    public override IEnumerator Execute()
    {
        // 현재 활성화된 장소 가져오기
        Place currentPlace = PlaceManager.Instance.CurrentPlace;

        // 새로운 장소 인스턴스화 및 점진적 활성화
        Place newPlace = PlaceManager.Instance.InstantiatePlace(_newPlaceID);
        if (newPlace != null)
        {
            // 새로운 장소가 점진적으로 나타나는 연출 (투명도 0 -> 1)
            CanvasGroup newPlaceCanvasGroup = newPlace.GetComponent<CanvasGroup>();
            if (newPlaceCanvasGroup != null)
            {
                newPlaceCanvasGroup.alpha = 0;
                newPlaceCanvasGroup.DOFade(1, _duration).SetEase(_transitionEase);
            }

            yield return new WaitForSeconds(_duration);

            // 기존 장소 파괴
            if (currentPlace != null)
            {
                PlaceManager.Instance.DestroyPlace(currentPlace.PlaceID);
            }
        }

        yield break;
    }
}
