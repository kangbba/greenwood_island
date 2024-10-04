using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Static fields for WorldCanvas, SystemCanvas, and prefabs
    private static WorldCanvas _worldCanvas;
    private static SystemCanvas _systemCanvas;
    private static PopupCanvas _popupCanvas;
    private static SaveLoadWindow _saveLoadWindowPrefab;
    private static GameSlot _gameSlotPrefab;
    private static YesNoPopup _yesNoPopupPrefab;

    // Public getters for accessing the canvas elements
    public static WorldCanvas WorldCanvas
    {
        get
        {
            if (_worldCanvas == null)
            {
                // 씬에서 WorldCanvas를 찾고 캐싱
                _worldCanvas = FindObjectOfType<WorldCanvas>();
                if (_worldCanvas == null)
                {
                    Debug.LogError("WorldCanvas is missing in the scene! Make sure to add a WorldCanvas to the scene.");
                }
            }
            return _worldCanvas;
        }
    }

    public static SystemCanvas SystemCanvas
    {
        get
        {
            if (_systemCanvas == null)
            {
                // 씬에서 SystemCanvas를 찾고 캐싱
                _systemCanvas = FindObjectOfType<SystemCanvas>();
                if (_systemCanvas == null)
                {
                    Debug.LogError("SystemCanvas is missing in the scene! Make sure to add a SystemCanvas to the scene.");
                }
            }
            return _systemCanvas;
        }
    }

    public static PopupCanvas PopupCanvas
    {
        get
        {
            if (_popupCanvas == null)
            {
                // 씬에서 SystemCanvas를 찾고 캐싱
                _popupCanvas = FindObjectOfType<PopupCanvas>();
                if (_popupCanvas == null)
                {
                    Debug.LogError("SystemCanvas is missing in the scene! Make sure to add a SystemCanvas to the scene.");
                }
            }
            return _popupCanvas;
        }
    }

    public static SaveLoadWindow SaveLoadWindowPrefab
    {
        get
        {
            if (_saveLoadWindowPrefab == null)
            {
                // Resources에서 SaveLoadWindowPrefab을 로드하고 캐싱
                _saveLoadWindowPrefab = Resources.Load<SaveLoadWindow>("UIs/SaveLoadWindowUI/SaveLoadWindowPrefab");
                if (_saveLoadWindowPrefab == null)
                {
                    Debug.LogError("SaveLoadWindowPrefab is missing in the Resources folder!");
                }
            }
            return _saveLoadWindowPrefab;
        }
    }

    public static GameSlot GameSlotPrefab
    {
        get
        {
            if (_gameSlotPrefab == null)
            {
                // Resources에서 GameSlotPrefab을 로드하고 캐싱
                _gameSlotPrefab = Resources.Load<GameSlot>("UIs/SaveLoadWindowUI/GameSlotPrefab");
                if (_gameSlotPrefab == null)
                {
                    Debug.LogError("GameSlotPrefab is missing in the Resources folder!");
                }
            }
            return _gameSlotPrefab;
        }
    }

    public static YesNoPopup YesNoPopupPrefab
    {
        get
        {
            if (_yesNoPopupPrefab == null)
            {
                // Resources에서 YesNoPopupPrefab을 로드하고 캐싱
                _yesNoPopupPrefab = Resources.Load<YesNoPopup>("UIs/SaveLoadWindowUI/YesNoPopupPrefab");
                if (_yesNoPopupPrefab == null)
                {
                    Debug.LogError("YesNoPopupPrefab is missing in the Resources folder!");
                }
            }
            return _yesNoPopupPrefab;
        }
    }
}
