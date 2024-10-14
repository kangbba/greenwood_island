using UnityEditor;
using UnityEngine;

public static class UIManager
{
    // Static fields for WorldCanvas, SystemCanvas, and prefabs
    private const string pathPrefix = "UIs";
    private const string suffix = "Prefab";
    private static WorldCanvas _worldCanvas;
    private static SystemCanvas _systemCanvas;
    private static PopupCanvas _popupCanvas;
    private static CursorCanvas _cursorCanvas;


    static UIManager()
    {
        Debug.Log("UIMANAGER CALLED");

        if (_popupCanvas != null)
        {
            GameObject.Destroy(_popupCanvas.gameObject);
        }
        _popupCanvas = InstantiateCanvas<PopupCanvas>("Canvas/PopupCanvas");

        // PopupCanvas가 씬 전환 시 파괴되지 않도록 설정
        GameObject.DontDestroyOnLoad(_popupCanvas.gameObject);


        if (_cursorCanvas != null)
        {
            GameObject.Destroy(_cursorCanvas);
        }
        _cursorCanvas = InstantiateCanvas<CursorCanvas>("Canvas/CursorCanvas");
        GameObject.DontDestroyOnLoad(_cursorCanvas.gameObject);
    }

    // Init 메서드: Resources/UIs 경로에서 프리팹들을 로드하고 캔버스들을 하이어라키에 인스턴스화
    public static void Initialize()
    {
        // 캔버스를 하이어라키에 인스턴스화
        if(_worldCanvas != null){
            GameObject.Destroy(_worldCanvas.gameObject);
        }
        _worldCanvas = InstantiateCanvas<WorldCanvas>("Canvas/WorldCanvas");
        
        if(_systemCanvas != null){
            GameObject.Destroy(_systemCanvas.gameObject);
        }
        _systemCanvas = InstantiateCanvas<SystemCanvas>("Canvas/SystemCanvas");

        Debug.Log("UIManager initialized with prefabs and instantiated canvases.");
    }

    // Generic method to load prefabs from Resources/UIs/{prefabName}
    private static T LoadPrefab<T>(string prefabName) where T : Object
    {
        string path = $"{pathPrefix}/{prefabName}Prefab";
        T prefab = Resources.Load<T>(path);
        if (prefab == null)
        {
            Debug.LogError($"{prefabName} is missing in the Resources/UIs folder!");
        }
        return prefab;
    }

    // Generic method to instantiate canvas prefabs in the hierarchy
    private static T InstantiateCanvas<T>(string prefabName) where T : Object
    {
        T prefab = LoadPrefab<T>(prefabName);
        if (prefab != null)
        {
            T instance = Object.Instantiate(prefab);  // 인스턴스화하여 하이어라키에 추가
            return instance;
        }
        return null;
    }

    // Public getters for accessing the prefabs and canvas elements
    public static WorldCanvas WorldCanvas => _worldCanvas;
    public static SystemCanvas SystemCanvas => _systemCanvas;
    public static PopupCanvas PopupCanvas => _popupCanvas;

    public static CursorCanvas CursorCanvas { get => _cursorCanvas; }
}
