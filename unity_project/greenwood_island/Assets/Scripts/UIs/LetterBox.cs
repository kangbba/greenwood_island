using UnityEngine;
using DG.Tweening; // DoTween을 사용하기 위한 네임스페이스

public class Letterbox : MonoBehaviour
{
    // 캡슐화된 상단과 하단 이미지 (UI Image를 하이어라키에 연결)
    [SerializeField] private RectTransform _topImage;
    [SerializeField] private RectTransform _bottomImage;

    private float _topImageHeight;    // 상단 이미지의 높이
    private float _bottomImageHeight; // 하단 이미지의 높이

    // Awake에서 초기화 작업 진행
    private void Awake()
    {
        // 상단, 하단 이미지의 Height 값을 저장
        _topImageHeight = _topImage.rect.height;
        _bottomImageHeight = _bottomImage.rect.height;

        // 게임이 시작될 때 레터박스를 숨긴 상태로 세팅
        SetOn(false, 0f);
    }

    // SetOn 메서드: 외부에서 호출 가능, 레터박스를 나타내거나 숨김
    public void SetOn(bool isOn, float duration)
    {
        if (isOn)
        {
            // 레터박스를 화면으로 이동 (위아래로 좁혀오는 효과)
            _topImage.DOAnchorPosY(0, duration).SetEase(Ease.OutQuad);
            _bottomImage.DOAnchorPosY(0, duration).SetEase(Ease.OutQuad);
        }
        else
        {
            // 상단 이미지를 화면 밖으로 이동 (자신의 Height만큼 위로 이동)
            _topImage.DOAnchorPosY(_topImageHeight, duration).SetEase(Ease.OutQuad);
            // 하단 이미지를 화면 밖으로 이동 (자신의 Height만큼 아래로 이동)
            _bottomImage.DOAnchorPosY(-_bottomImageHeight, duration).SetEase(Ease.OutQuad);
        }
    }
}
