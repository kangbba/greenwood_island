using UnityEditor;
using UnityEngine;

public class StoryFileDetailWindow : EditorWindow
{
    private StoryFileInfo _fileInfo;

    public static void ShowDetail(StoryFileInfo fileInfo)
    {
        var window = GetWindow<StoryFileDetailWindow>("파일 상세 정보");
        window._fileInfo = fileInfo;
        window.Show();
    }

    private void OnGUI()
    {
        if (_fileInfo == null)
        {
            GUILayout.Label("선택된 파일이 없습니다.");
            return;
        }

        GUILayout.Label("파일 이름:", EditorStyles.boldLabel);
        GUILayout.Label(_fileInfo.FileName, EditorStyles.largeLabel);

        GUILayout.Space(10);

        GUILayout.Label("미리보기:", EditorStyles.boldLabel);
        GUILayout.Label(_fileInfo.Thumbnail, GUILayout.Width(200), GUILayout.Height(200));

        GUILayout.Space(10);

        GUILayout.Label("Sprite 여부:", EditorStyles.boldLabel);
        GUILayout.Label(_fileInfo.IsSprite ? "✔️ 이 파일은 Sprite입니다." : "❌ 이 파일은 Sprite가 아닙니다.");
    }
}
