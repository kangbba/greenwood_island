using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Ensayne.TextDisplayerSystem.TextDisplayer))]
public class TextDisplayerEditor : Editor
{
    private void OnEnable()
    {
        try
        {
            // 아이콘을 로드합니다.
            var icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Ensayne/TextDisplayerSystem/Editor/Icons/TextDisplayerIcon.png");
            if (icon == null)
            {
                Debug.LogWarning("아이콘이 제대로 로드되지 않았습니다.");
                return;
            }

            var targetObject = (Ensayne.TextDisplayerSystem.TextDisplayer)target;
            EditorGUIUtility.SetIconForObject(targetObject, icon);
        }
        catch (Exception ex)
        {
            Debug.LogError($"아이콘 로드 실패: {ex.Message}");
        }
    }

    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 GUI를 표시합니다.
        DrawDefaultInspector();
    }
}
