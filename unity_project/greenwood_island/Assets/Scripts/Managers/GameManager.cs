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

    private bool _isTest = true;
    private string _initialStoryID;

    void Awake()
    {
        FXManager.Initialize();
        SFXManager.Initialize();
        StoryManager.Initialize();
        PlaceManager.Initialize();
        CharacterManager.Initialize();
        ImaginationManager.Initialize();
        UIManager.Initialize();
        PuzzleManager.Initialize();

        StartCoroutine(DelayStart(GameDataManager.CurrentStorySavedData));

        // GUI 스타일 설정
        _guiStyle = new GUIStyle
        {
            fontSize = 40,                      // 폰트 크기 설정
            normal = { textColor = Color.white }, // 텍스트 색상 설정
            alignment = TextAnchor.MiddleCenter  // 중앙 정렬
        };

        // Resources 폴더에서 스토리 ID 목록 가져오기
        _availableStoryIDs = ResourcePathManager.GetAvailableStoryIDs();
    }

    // 명시적으로 StorySavedData를 전달받아 스토리를 시작하는 코루틴
    public IEnumerator DelayStart(StorySavedData savedData)
    {
        yield return new WaitForEndOfFrame();

        if (savedData == null)
        {
            Debug.LogError("저장된 데이터가 없습니다. 게임을 시작할 수 없습니다.");
            yield break;  // 저장된 데이터가 없으면 종료
        }

        Debug.Log("이어 하기 발동");
        // 전달된 StorySavedData로 스토리 실행
        StoryManager.Instance.PlayStory(savedData.StoryID, savedData.RecentPlayedElementIndex);
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

            // x1, x2, x5 속도 버튼 배치
            float buttonWidth = 100;
            float buttonHeight = 60;
            float buttonY = 110;

            // 게임 속도 조절 버튼들
            if (GUI.Button(new Rect(10, buttonY, buttonWidth, buttonHeight), "x1"))
            {
                SetGameSpeed(1f);
            }
            if (GUI.Button(new Rect(10 + buttonWidth + 10, buttonY, buttonWidth, buttonHeight), "x2"))
            {
                SetGameSpeed(2f);
            }
            if (GUI.Button(new Rect(10 + (buttonWidth + 10) * 2, buttonY, buttonWidth, buttonHeight), "x5"))
            {
                SetGameSpeed(5f);
            }

            // 스크롤 뷰 설정 (스토리 ID 목록 표시)
            if (_availableStoryIDs != null && _availableStoryIDs.Length > 0)
            {
                float scrollViewY = buttonY + buttonHeight + 20;
                float scrollHeight = Screen.height - scrollViewY - 20;

                _scrollPosition = GUI.BeginScrollView(
                    new Rect(10, scrollViewY, buttonWidth * 3 + 20, scrollHeight),
                    _scrollPosition,
                    new Rect(0, 0, buttonWidth * 3, _availableStoryIDs.Length * 70)
                );

                for (int i = 0; i < _availableStoryIDs.Length; i++)
                {
                    if (GUI.Button(new Rect(0, i * 70, buttonWidth * 3, 60), _availableStoryIDs[i]))
                    {
                        // 스토리 버튼 클릭 시 GameDataManager의 LoadGameDataThenPlay 호출
                        GameDataManager.StartNewGame(_availableStoryIDs[i]);
                    }
                }

                GUI.EndScrollView();
            }
        }
    }

    // 게임 속도 설정 메서드
    private void SetGameSpeed(float speed)
    {
        _currentGameSpeed = speed;
        Time.timeScale = _currentGameSpeed;
    }
}
