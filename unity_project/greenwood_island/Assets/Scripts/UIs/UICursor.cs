using UnityEngine;

public class UICursor : MonoBehaviour
{
    // 커서 모드를 정의하는 열거형
    public enum CursorMode
    {
        None,       // 커서 숨김
        Normal,     // 일반 커서 모드
        Magnifier   // 돋보기 커서 모드
    }

    [SerializeField] private Transform _normalCursor; // 일반 커서
    [SerializeField] private Transform _magnifierCursor; // 돋보기 커서

    private CursorMode _currentMode = CursorMode.None; // 현재 커서 모드

    private void Start()
    {
        SetCursorMode(CursorMode.Normal);
    }

    private void FixedUpdate()
    {
        // 마우스 포인터 위치를 가져와 상위 부모 위치 이동
        Vector3 mousePosition = Input.mousePosition;
        transform.position = mousePosition;

        // 커서 모드에 따라 커서의 표시 상태를 업데이트
        UpdateCursorVisibility();
    }

    /// <summary>
    /// 커서 모드를 설정합니다.
    /// </summary>
    /// <param name="mode">설정할 커서 모드</param>
    public void SetCursorMode(CursorMode mode)
    {
        _currentMode = mode; // 현재 모드 설정
        UpdateCursorVisibility(); // 커서 상태 업데이트
    }

    /// <summary>
    /// 현재 커서 모드에 따라 커서를 활성화 또는 비활성화합니다.
    /// </summary>
    private void UpdateCursorVisibility()
    {
        _normalCursor.gameObject.SetActive(_currentMode == CursorMode.Normal);
        _magnifierCursor.gameObject.SetActive(_currentMode == CursorMode.Magnifier);
    }
}
