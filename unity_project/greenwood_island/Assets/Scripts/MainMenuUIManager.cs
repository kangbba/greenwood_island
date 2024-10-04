using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  // DOTween 사용을 위한 네임스페이스 추가
using System.Collections.Generic;
using System.Resources;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _loadGameButton;
    private SaveLoadWindow _loadGameWindow;  // 인스턴스화된 불러오기 창

    private void Start()
    {
        // 새 게임 버튼 이벤트 연결
        _newGameButton.onClick.AddListener(StartNewGame);

        // 불러오기 버튼 이벤트 연결
        _loadGameButton.onClick.AddListener(OpenLoadGameWindow);

        // 저장된 데이터가 없으면 로드 버튼 비활성화
        _loadGameButton.interactable = GameDataManager.GetSavedGameSlots().Count > 0;
    }

    // 새 게임 시작
    public void StartNewGame()
    {
        // 게임 플레이 씬으로 전환
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
    }

    // 불러오기 창 열기
    public void OpenLoadGameWindow()
    {
        // 이미 인스턴스화된 창이 없으면 새로 인스턴스화
        if (_loadGameWindow == null)
        {
            var _saveLoadWindowPrefab = UIManager.SaveLoadWindowPrefab;
            _loadGameWindow = Instantiate(_saveLoadWindowPrefab, transform);  // MainMenu의 자식으로 생성
            _loadGameWindow.Init(isSaveMode: false);  // 로드 모드로 초기화
        }

        // 불러오기 창을 활성화한 뒤 페이드 인 애니메이션 적용
        _loadGameWindow.gameObject.SetActive(true);
        _loadGameWindow.Init(false);
    }

}
