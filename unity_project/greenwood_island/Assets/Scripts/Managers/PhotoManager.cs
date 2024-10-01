
using UnityEngine;
using UnityEngine.UI;

public static class PhotoManager
{
    private static bool _isPhotoTaken = false;
    private static bool _isPhotoCancelled = false;
    private static Image _imageToPhoto;  // 이미지의 RectTransform
    private static float _moveSpeed = 100f;  // 이미지 이동 속도

    // 이미지 초기화
    public static void Init(Image imageToPhoto)
    {
        _imageToPhoto = imageToPhoto;
        _isPhotoTaken = false;
        _isPhotoCancelled = false;
    }

    // 사용자의 입력을 처리하여 이미지를 이동 및 사진 찍기
    public static void Update()
    {
        if (_imageToPhoto == null)
        {
            Debug.LogError("Image RectTransform is not set.");
            return;
        }

        // 상하좌우 키 입력을 받아 이미지 이동
        Vector2 movement = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement.y += _moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement.y -= _moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x -= _moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement.x += _moveSpeed * Time.deltaTime;
        }

        // 이미지 위치 업데이트
        _imageToPhoto.rectTransform.anchoredPosition += movement;

        // 스페이스바를 눌러 사진 촬영
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakePhoto();
        }

        // ESC 키를 눌러 촬영 취소
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelPhoto();
        }
    }

    // 사진 촬영 메서드
    private static void TakePhoto()
    {
        Debug.Log("찰칵! 사진이 찍혔습니다.");  // 사진 촬영 소리 (현재는 로그로 처리)
        _isPhotoTaken = true;  // 사진 촬영 완료 플래그 설정
    }

    // 사진 촬영 취소 메서드
    private static void CancelPhoto()
    {
        Debug.Log("사진 촬영이 취소되었습니다.");
        _isPhotoCancelled = true;  // 사진 취소 플래그 설정
    }

    // 사진 촬영이 완료되었는지 확인하는 메서드
    public static bool IsPhotoTaken()
    {
        return _isPhotoTaken;
    }

    // 사진 촬영이 취소되었는지 확인하는 메서드
    public static bool IsPhotoCancelled()
    {
        return _isPhotoCancelled;
    }
}
