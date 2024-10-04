using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _showInputField = false; // InputField를 보여줄지 여부
    private string _storyID = "FirstKateStory";  // 기본값으로 설정
    private GUIStyle _guiStyle; // 큰 폰트 크기를 위한 GUI 스타일

    void Start()
    {
        StartCoroutine(DelayStart());

        // GUI 스타일을 설정 (폰트 크기 등)
        _guiStyle = new GUIStyle();
        _guiStyle.fontSize = 40; // 폰트 크기를 40으로 설정
        _guiStyle.normal.textColor = Color.white; // 텍스트 색상 설정
        _guiStyle.alignment = TextAnchor.MiddleCenter; // 중앙 정렬
    }

    // 스토리 시작을 지연시키는 코루틴
    private IEnumerator DelayStart()
    {
        yield return new WaitForEndOfFrame();

        if (GameDataManager.CurrentGameSaveData != null)
        {
            // 기존 저장된 데이터가 있으면 해당 storyID로 PlayStory 실행
            StoryManager.PlayStory(GameDataManager.CurrentGameSaveData.storyID);
        }
        else
        {
            // GameSaveData가 없을 경우, IMGUI로 storyID 입력받기
            _showInputField = true;
        }
    }

    // IMGUI를 사용한 개발용 스토리 ID 입력 창
    private void OnGUI()
    {
        if (_showInputField)
        {
            // 중앙에 창을 띄우기 위해 화면 너비/높이 계산
            float width = 800;
            float height = 400;
            float x = (Screen.width - width) / 2;
            float y = (Screen.height - height) / 2;

            // 입력 UI 박스 생성 (박스 스타일은 기본 스타일 사용)
            GUI.Box(new Rect(x, y, width, height), "Enter Story ID", _guiStyle);

            // 텍스트 입력 필드 생성 (폰트 크기 증가)
            GUI.skin.textField.fontSize = 30;
            _storyID = GUI.TextField(new Rect(x + 20, y + 100, width - 40, 60), _storyID, 25);

            // "Start Story" 버튼 생성 (폰트 크기 증가)
            GUI.skin.button.fontSize = 30;
            if (GUI.Button(new Rect(x + 20, y + 300, width - 40, 60), "Start Story") || Event.current.isKey && Event.current.keyCode == KeyCode.Return)
            {
                if (!string.IsNullOrEmpty(_storyID))
                {
                    StoryManager.PlayStory(_storyID);
                    _showInputField = false; // 입력 창을 닫음
                }
            }
            // "Go to Lobby" 버튼 생성
            if (GUI.Button(new Rect(x + 20, y + 400, width - 40, 60), "Go to Lobby"))
            {
                // 로비 씬으로 이동
                SceneManager.LoadScene("Lobby");
            }
        }
    }
}
