using UnityEditor;
using UnityEngine;
using System.IO;

public class StoryCreationWindow : EditorWindow
{
    private string _newStoryName = string.Empty;
    private StoryEditor _parentEditor;

    public static void ShowWindow(StoryEditor parentEditor)
    {
        var window = GetWindow<StoryCreationWindow>("새로운 스토리 생성");
        window._parentEditor = parentEditor;
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("새로운 스토리 이름을 입력하세요:", EditorStyles.boldLabel);
        _newStoryName = EditorGUILayout.TextField("스토리 이름", _newStoryName);

        if (GUILayout.Button("스토리 생성"))
        {
            if (string.IsNullOrEmpty(_newStoryName))
            {
                EditorUtility.DisplayDialog("오류", "스토리 이름을 입력하세요.", "확인");
                return;
            }

            // 스토리 폴더 생성 메서드 호출
            _parentEditor.CreateStoryFolders(_newStoryName);
            Close();
        }
    }
}
