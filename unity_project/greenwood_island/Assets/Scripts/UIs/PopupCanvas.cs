using UnityEngine;
using System.Collections.Generic;

public class PopupCanvas : MonoBehaviour
{
    [SerializeField] private YesNoPopup _yesNoPopupPrefab;
    [SerializeField] private OkPopup _okPopupPrefab;
    [SerializeField] private SaveLoadWindow _saveLoadWindowPrefab;
    [SerializeField] private Transform popupParent;

    public bool IsPoppedUp => popupParent.childCount > 0;
    private SaveLoadWindow _loadGameWindow;  // 인스턴스화된 불러오기 창

    // 예/아니오 팝업 호출 메서드
    public void ShowYesNoPopup(string message, string yesText, string noText, System.Action onYesAction)
    {
        YesNoPopup popupInstance = Instantiate(_yesNoPopupPrefab, popupParent);
        popupInstance.Init(message, yesText, noText, onYesAction);
    }

    // 확인 팝업 호출 메서드
    public void ShowOkPopup(string message, string okText, System.Action onOkAction)
    {
        OkPopup popupInstance = Instantiate(_okPopupPrefab, popupParent);
        popupInstance.Init(message, okText, onOkAction);
    }

    // Save/Load 팝업 호출 메서드 (단일 인스턴스만 허용)
    public void ShowLoadWindow()
    {
        // 이미 인스턴스화된 창이 없으면 새로 인스턴스화
        if (_loadGameWindow == null)
        {
        }
        _loadGameWindow = Instantiate(_saveLoadWindowPrefab, popupParent);
        _loadGameWindow.Init(isSaveMode: false, null);  // 로드 모드
    }
    // Save/Load 팝업 호출 메서드 (단일 인스턴스만 허용)
    public void ShowSaveWindow(StorySavedData storySavedData)
    {
        // 이미 인스턴스화된 창이 없으면 새로 인스턴스화
        if (_loadGameWindow == null)
        {
        }
        _loadGameWindow = Instantiate(_saveLoadWindowPrefab, popupParent);
        _loadGameWindow.Init(isSaveMode: true, storySavedData);  // 로드 모드
    }

    public void Clear(){
        popupParent.DestroyAllChildren();
    }
}
