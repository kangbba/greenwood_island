using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  // DOTween 사용을 위한 네임스페이스 추가
using System.Collections.Generic;
using System.Resources;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _loadGameButton;

    private void Start()
    {
        // 새 게임 버튼 이벤트 연결
        _newGameButton.onClick.AddListener(StartNewGame);

        // 불러오기 버튼 이벤트 연결
        _loadGameButton.onClick.AddListener(UIManager.PopupCanvas.ShowLoadWindow);

        // 저장된 데이터가 없으면 로드 버튼 비활성화
        _loadGameButton.interactable = GameDataManager.GetSavedGameSlots().Count > 0;
    }

    // 새 게임 시작
    public void StartNewGame()
    {
        // 게임 플레이 씬으로 전환
        GameDataManager.LoadGameDataThenPlay(-1);
    }

}
