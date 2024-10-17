using UnityEngine;
public class UICursor : MonoBehaviour
{
    // 커서 모드를 정의하는 열거형
    public enum CursorMode
    {
        None,       // 커서 숨김
        Normal,     // 일반 커서 모드
        Magnifier,  // 돋보기 커서 모드
        Talk        // 캐릭터와 대화 중일 때 사용
    }

    [SerializeField] private Transform _normalCursor;   // 일반 커서
    [SerializeField] private Transform _magnifierCursor; // 돋보기 커서
    [SerializeField] private Transform _talkCursor;     // Talk 모드 커서

    private CursorMode _currentMode = CursorMode.None; // 현재 커서 모드

    private void Start()
    {
        SetCursorMode(CursorMode.Normal);
    }

    private void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        transform.position = mousePosition;

        UpdateCursorVisibility();
    }

    public void SetCursorMode(CursorMode mode)
    {
        _currentMode = mode;
        UpdateCursorVisibility();
    }

    private void UpdateCursorVisibility()
    {
        _normalCursor.gameObject.SetActive(_currentMode == CursorMode.Normal);
        _magnifierCursor.gameObject.SetActive(_currentMode == CursorMode.Magnifier);
        _talkCursor.gameObject.SetActive(_currentMode == CursorMode.Talk); // Talk 모드 추가
    }
}
