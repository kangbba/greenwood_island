using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;  // UI 요소를 사용하기 위해 추가

public class Place : MonoBehaviour
{
    public EPlaceID PlaceID;  // 장소의 ID를 할당할 필드
    private Image[] _images;  // 여러 UI 이미지 컴포넌트를 받을 배열

    private void Awake()
    {
        // 게임 오브젝트의 모든 Image 컴포넌트를 배열로 가져옴
        _images = GetComponents<Image>();

        if (_images == null || _images.Length == 0)
        {
            Debug.LogError("Image components are missing.");
        }
    }

    // 색상을 변경하는 유틸리티 함수
    public Tween SetColor(Color color, float duration, Ease easeType)
    {
        // 배열에 있는 모든 이미지의 색상을 변경
        if (_images != null && _images.Length > 0)
        {
            Sequence sequence = DOTween.Sequence();  // 모든 트윈을 묶는 시퀀스 생성

            foreach (var image in _images)
            {
                // 각 이미지의 색상을 변경하는 트윈을 시퀀스에 추가
                sequence.Join(image.DOColor(color, duration).SetEase(easeType));
            }

            return sequence;
        }

        return null;
    }
}
