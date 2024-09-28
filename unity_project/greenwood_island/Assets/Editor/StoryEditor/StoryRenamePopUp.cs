using UnityEngine;
using UnityEditor;

public class StoryRenamePopUp : EditorWindow
{
    private string _newName;
    private string _oldName;
    private StoryEditor _editorWindow;

    public static void ShowWindow(StoryEditor editorWindow, string oldName)
    {
        StoryRenamePopUp window = ScriptableObject.CreateInstance<StoryRenamePopUp>();
        window._oldName = oldName;
        window._editorWindow = editorWindow;
        window.titleContent = new GUIContent("Rename Folder");
        window.ShowUtility();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("새로운 이름을 입력하세요:", EditorStyles.wordWrappedLabel);
        _newName = EditorGUILayout.TextField("새 이름:", _newName);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("확인"))
        {
            if (!string.IsNullOrEmpty(_newName) && _newName != _oldName)
            {
                _editorWindow.RenameStoryFolder(_oldName, _newName);
                Close();
            }
            else
            {
                EditorUtility.DisplayDialog("이름 오류", "이름이 유효하지 않거나 기존 이름과 동일합니다.", "확인");
            }
        }

        if (GUILayout.Button("취소"))
        {
            Close();
        }

        EditorGUILayout.EndHorizontal();
    }
}
