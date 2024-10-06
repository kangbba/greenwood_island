
using UnityEngine;
using UnityEngine.UI;

public class PhotoManager : SingletonManager<PhotoManager>
{
    private bool _isPhotoTaken = false;
    private bool _isPhotoCancelled = false;
    private Image _imageToPhoto;  // 이미지의 RectTransform
    private float _moveSpeed = 100f;  // 이미지 이동 속도

    // 이미지 초기화
    public void Init(Image imageToPhoto)
    {
        _imageToPhoto = imageToPhoto;
        _isPhotoTaken = false;
        _isPhotoCancelled = false;
    }

    // 사진 촬영 메서드
    private void TakePhoto()
    {
        Debug.Log("찰칵! 사진이 찍혔습니다.");  // 사진 촬영 소리 (현재는 로그로 처리)
        _isPhotoTaken = true;  // 사진 촬영 완료 플래그 설정
    }

    // 사진 촬영 취소 메서드
    private void CancelPhoto()
    {
        Debug.Log("사진 촬영이 취소되었습니다.");
        _isPhotoCancelled = true;  // 사진 취소 플래그 설정
    }

    // 사진 촬영이 완료되었는지 확인하는 메서드
    public bool IsPhotoTaken()
    {
        return _isPhotoTaken;
    }

    // 사진 촬영이 취소되었는지 확인하는 메서드
    public bool IsPhotoCancelled()
    {
        return _isPhotoCancelled;
    }
}
