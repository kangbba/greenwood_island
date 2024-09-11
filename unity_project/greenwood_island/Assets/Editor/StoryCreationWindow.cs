using UnityEditor;
using UnityEngine;

public class StoryCreationWindow : EditorWindow
{
    private string _newStoryName = "";
    private StoryEditor _parentEditor;

    // StoryEditor를 인자로 받아 초기화
    public static void ShowWindow(StoryEditor parent)
    {
        var window = GetWindow<StoryCreationWindow>("새로운 스토리 생성");
        window._parentEditor = parent; // 부모 에디터를 설정하여 CreateStoryFolders를 호출할 수 있게 함
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("새로운 스토리 이름을 입력하세요:", EditorStyles.boldLabel);
        _newStoryName = EditorGUILayout.TextField("스토리 이름:", _newStoryName);

        if (GUILayout.Button("생성", GUILayout.Height(30)))
        {
            if (string.IsNullOrEmpty(_newStoryName))
            {
                EditorUtility.DisplayDialog("오류", "스토리 이름을 입력해주세요.", "확인");
            }
            else
            {
                // 부모 에디터의 CreateStoryFolders 메서드를 호출하여 스토리 생성
                _parentEditor.CreateStoryFolders(_newStoryName);
                Close();
            }
        }
    }
}
