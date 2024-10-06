using UnityEngine;

public static class UIManager
{
    // Static fields for WorldCanvas, SystemCanvas, and prefabs
    private const string pathPrefix = "UIs";
    private const string suffix = "Prefab";
    private static WorldCanvas _worldCanvas;
    private static SystemCanvas _systemCanvas;
    private static PopupCanvas _popupCanvas;


    private static SaveLoadWindow _saveLoadWindowPrefab;
    private static GameSlot _gameSlotPrefab;
    private static YesNoPopup _yesNoPopupPrefab;
    private static OkPopup _okPopupPrefab;

    static UIManager()
    {
        // Prefab들을 Resources/UIs 경로에서 로드
        _saveLoadWindowPrefab = LoadPrefab<SaveLoadWindow>("SaveLoadWindow");
        _gameSlotPrefab = LoadPrefab<GameSlot>("GameSlot");
        _yesNoPopupPrefab = LoadPrefab<YesNoPopup>("YesNoPopup");
        _okPopupPrefab = LoadPrefab<OkPopup>("OkPopup");
    }

    // Init 메서드: Resources/UIs 경로에서 프리팹들을 로드하고 캔버스들을 하이어라키에 인스턴스화
    public static void Init()
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

        if(_popupCanvas != null){
            GameObject.Destroy(_popupCanvas.gameObject);
        }
        _popupCanvas = InstantiateCanvas<PopupCanvas>("Canvas/PopupCanvas");


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
    public static SaveLoadWindow SaveLoadWindowPrefab => _saveLoadWindowPrefab;
    public static GameSlot GameSlotPrefab => _gameSlotPrefab;
    public static YesNoPopup YesNoPopupPrefab => _yesNoPopupPrefab;
    public static OkPopup OkPopupPrefab => _okPopupPrefab;

    // 예/아니오 팝업 호출 메서드
    public static void ShowYesNoPopup(Transform spawnTr, string message, string yesText, string noText, System.Action onYesAction)
    {
        YesNoPopup popupInstance = Object.Instantiate(YesNoPopupPrefab, spawnTr);
        popupInstance.Init(message, yesText, noText, onYesAction);
    }

    // 확인 팝업 호출 메서드
    public static void ShowOkPopup(Transform spawnTr, string message, string okText, System.Action onOkAction)
    {
        OkPopup popupInstance = Object.Instantiate(OkPopupPrefab, spawnTr);
        popupInstance.Init(message, okText, onOkAction);
    }
}
