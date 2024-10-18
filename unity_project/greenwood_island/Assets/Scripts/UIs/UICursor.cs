using UnityEngine;

public class UICursor : MonoBehaviour
{
    // 커서 모드를 정의하는 열거형
    public enum CursorMode
    {
        None,       // 커서 숨김
        Normal,     // 일반 커서 모드
        Magnifier,   // 돋보기 커서 모드
        Talk,
    }

    // 커서 타입을 관리하는 별도의 클래스
    [System.Serializable]
    public class CursorType
    {
        [SerializeField] private CursorMode _cursorMode;       // 커서 모드 (None, Normal, Magnifier)
        [SerializeField] private Transform _cursorTransform;   // 커서에 연결된 Transform

        // 커서 모드의 getter
        public CursorMode Mode
        {
            get { return _cursorMode; }
        }

        // 커서 Transform의 getter
        public Transform CursorTransform
        {
            get { return _cursorTransform; }
        }
    }

    [SerializeField] private CursorType[] _cursorTypes; // 커서 모드와 Transform을 연결하는 배열
    private CursorMode _currentMode = CursorMode.Normal;  // 현재 커서 모드


    private void FixedUpdate()
    {
        // 마우스 위치를 가져와 커서 위치를 갱신
        Vector3 mousePosition = Input.mousePosition;
        transform.position = mousePosition;

        UpdateCursorVisibility();  // 새로운 모드에 따라 커서 상태 갱신
    }

    // 커서 모드를 설정하는 메서드
    public void SetCursorMode(CursorMode mode)
    {
        _currentMode = mode;
    }

    // 현재 커서 모드에 따라 커서를 활성화/비활성화하는 메서드
    private void UpdateCursorVisibility()
    {
        foreach (var cursorType in _cursorTypes)
        {
            // 현재 모드와 일치하는 커서만 활성화
            if(cursorType.CursorTransform != null){
                cursorType.CursorTransform.gameObject.SetActive(cursorType.Mode == _currentMode);
            }
        }
    }
}
