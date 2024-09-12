using UnityEditor;
using UnityEngine;

/// <summary>
/// StoryEditorUIHelper handles the UI elements and visual feedback within the StoryEditor.
/// It provides methods for drawing folder headers, file tiles, and managing visual feedback for drag-and-drop actions.
/// </summary>
public static class StoryEditorUIHelper
{
    // Draws the header for a folder section in the editor
    public static void DrawFolderHeader(string headerTitle)
    {
        EditorGUILayout.LabelField(headerTitle, EditorStyles.boldLabel);
        EditorGUILayout.Space(5);
    }

    // Draws a file tile with a thumbnail, file name, and sprite status
    public static void DrawFileTile(StoryFileInfo fileInfo)
    {
        EditorGUILayout.BeginHorizontal("box", GUILayout.Height(80));

        // Draw thumbnail if available
        if (fileInfo.Thumbnail != null)
        {
            GUILayout.Label(fileInfo.Thumbnail, GUILayout.Width(70), GUILayout.Height(70));
        }
        else
        {
            GUILayout.Label("No Preview", GUILayout.Width(70), GUILayout.Height(70));
        }

        // Draw file name centered
        GUILayout.Label(fileInfo.FileName, new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter }, GUILayout.Width(200));

        // Display sprite status with a checkmark or cross
        GUILayout.Label(fileInfo.IsSprite ? "✔️" : "❌", GUILayout.Width(30));

        // Button to show detailed information about the file
        if (GUILayout.Button("자세히", GUILayout.Width(60)))
        {
            StoryFileDetailWindow.ShowDetail(fileInfo);
        }

        // Button to delete the file with a warning dialog
        if (GUILayout.Button("삭제", GUILayout.Width(60)))
        {
            if (EditorUtility.DisplayDialog("삭제 확인", $"{fileInfo.FileName}을(를) 정말로 삭제하시겠습니까?", "삭제", "취소"))
            {
                StoryEditorFileHandler.DeleteFile(fileInfo);
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    // Provides visual feedback during drag-and-drop actions
    public static void HandleDragVisuals(Rect dropArea, string folderName)
    {
        Event evt = Event.current;

        if (evt.type == EventType.Repaint)
        {
            // Highlight the drop area when dragging files over
            GUIStyle highlightStyle = new GUIStyle(EditorStyles.helpBox);
            highlightStyle.normal.background = EditorGUIUtility.whiteTexture;

            if (dropArea.Contains(evt.mousePosition))
            {
                Color originalColor = GUI.color;
                GUI.color = new Color(0.1f, 0.6f, 0.1f, 0.3f); // Light green tint
                GUI.Box(dropArea, $"Drop to add files to {folderName}", highlightStyle);
                GUI.color = originalColor;
            }
        }
    }
}
