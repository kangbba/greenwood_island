using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string[] _availableStoryIDs; // Resources 폴더에서 가져온 storyID 목록
    private GUIStyle _guiStyle; // 큰 폰트 크기를 위한 GUI 스타일
    private float _currentGameSpeed = 1f; // 현재 게임 속도 (x1 기본)
    private bool _showAllMenus = true; // 모든 메뉴 표시 여부
    private Vector2 _scrollPosition; // 스크롤 뷰의 위치

    
    void Start()
    {
        FXManager.Initialize();
        SFXManager.Initialize();
        StoryManager.Initialize();
        PlaceManager.Initialize();
        CharacterManager.Initialize();
        ImaginationManager.Initialize();
        PhotoManager.Initialize();
        UIManager.Init();
        CameraController.Init();

        StartCoroutine(DelayStart());

        // GUI 스타일을 설정 (폰트 크기 등)
        _guiStyle = new GUIStyle();
        _guiStyle.fontSize = 40; // 폰트 크기를 40으로 설정
        _guiStyle.normal.textColor = Color.white; // 텍스트 색상 설정
        _guiStyle.alignment = TextAnchor.MiddleCenter; // 중앙 정렬

        // Resources 폴더에서 스토리 ID 목록을 가져옴
        _availableStoryIDs = ResourcePathManager.GetAvailableStoryIDs();
    }

    // 스토리 시작을 지연시키는 코루틴
    private IEnumerator DelayStart()
    {
        yield return new WaitForEndOfFrame();

        if (GameDataManager.CurrentGameSaveData != null)
        {
            // 기존 저장된 데이터가 있으면 해당 storyID로 PlayStory 실행
            StoryManager.Instance.PlayStory(GameDataManager.CurrentGameSaveData.storyID);
        }
    }

    // IMGUI를 사용한 개발용 스토리 ID 선택 및 게임 속도 조절 버튼
    private void OnGUI()
    {
        // 왼쪽 상단에 모든 메뉴를 숨기거나 표시하는 토글 버튼
        if (GUI.Button(new Rect(10, 10, 100, 40), _showAllMenus ? "Hide" : "Show"))
        {
            _showAllMenus = !_showAllMenus;
        }


        // 모든 메뉴가 보일 때만 표시
        if (_showAllMenus)
        {
            // 로비로 돌아가는 버튼
            if (GUI.Button(new Rect(10, 60, 100, 40), "Lobby"))
            {
                SceneManager.LoadScene("Lobby");
            }
            // x1, x2, x5 버튼을 가로로 배치
            float buttonWidth = 100;
            float buttonHeight = 60;
            float buttonY = 110; // 로비 버튼 아래에 배치

            // x1 속도 버튼
            if (GUI.Button(new Rect(10, buttonY, buttonWidth, buttonHeight), "x1"))
            {
                _currentGameSpeed = 1f;
                Time.timeScale = _currentGameSpeed;
            }

            // x2 속도 버튼
            if (GUI.Button(new Rect(10 + buttonWidth + 10, buttonY, buttonWidth, buttonHeight), "x2"))
            {
                _currentGameSpeed = 2f;
                Time.timeScale = _currentGameSpeed;
            }

            // x5 속도 버튼
            if (GUI.Button(new Rect(10 + (buttonWidth + 10) * 2, buttonY, buttonWidth, buttonHeight), "x5"))
            {
                _currentGameSpeed = 5f;
                Time.timeScale = _currentGameSpeed;
            }

            // 스크롤 뷰 설정 (스토리 ID 버튼 리스트)
            if (_availableStoryIDs != null && _availableStoryIDs.Length > 0)
            {
                float scrollViewY = buttonY + buttonHeight + 20; // 속도 버튼 아래에 스토리 버튼을 배치
                float scrollHeight = Screen.height - scrollViewY - 20; // 화면의 아래까지 차지
                _scrollPosition = GUI.BeginScrollView(new Rect(10, scrollViewY, buttonWidth * 3 + 20, scrollHeight), _scrollPosition, new Rect(0, 0, buttonWidth * 3, _availableStoryIDs.Length * 70));

                for (int i = 0; i < _availableStoryIDs.Length; i++)
                {
                    if (GUI.Button(new Rect(0, i * 70, buttonWidth * 3, 60), _availableStoryIDs[i], GUI.skin.button))
                    {
                        // 선택한 스토리로 스토리 시작
                        StoryManager.Instance.PlayStory(_availableStoryIDs[i]);
                    }
                }

                GUI.EndScrollView();
            }
        }
    }

}
