using UnityEngine;
using UnityEngine.EventSystems;

public static class InputUtils
{
    /// <summary>
    /// 마우스 포인터가 현재 UI 오브젝트 위에 있는지 확인합니다.
    /// </summary>
    /// <returns>UI 오브젝트 위에 있으면 true, 아니면 false</returns>
    public static bool IsPointerOverUI()
    {
        // 마우스가 UI 요소 위에 있는지 확인
        if (EventSystem.current == null)
        {
            Debug.LogWarning("EventSystem이 설정되지 않았습니다.");
            return false;
        }

        // 마우스 위치의 UI 요소가 있는지 체크
        return EventSystem.current.IsPointerOverGameObject();
    }
    
    /// <summary>
    /// 터치 입력의 경우 UI 오브젝트 위에 있는지 확인합니다.
    /// </summary>
    /// <param name="touchIndex">확인할 터치 인덱스</param>
    /// <returns>UI 오브젝트 위에 있으면 true, 아니면 false</returns>
    public static bool IsTouchOverUI(int touchIndex)
    {
        // 터치 입력이 있을 경우 UI 요소 위에 있는지 확인
        if (EventSystem.current == null)
        {
            Debug.LogWarning("EventSystem이 설정되지 않았습니다.");
            return false;
        }

        if (Input.touchCount > touchIndex && touchIndex >= 0)
        {
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(touchIndex).fingerId);
        }
        return false;
    }
}
