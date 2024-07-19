using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ensayne.TextDisplaySystem.TextDisplayer))]
public class TextDisplayerEditor : Editor
{
    private static Texture2D _icon;

    static TextDisplayerEditor()
    {
        // 아이콘을 로드합니다.
        _icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Icons/TextDisplayerIcon.png");
    }

    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 GUI를 표시합니다.
        DrawDefaultInspector();
    }

    [InitializeOnLoadMethod]
    private static void InitializeIcon()
    {
        // 유니티 에디터에서 아이콘을 설정합니다.
        var editorGUIUtilityType = typeof(EditorGUIUtility);
        var setIconForObjectMethod = editorGUIUtilityType.GetMethod("SetIconForObject", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

        if (setIconForObjectMethod != null && _icon != null)
        {
            foreach (var obj in Resources.FindObjectsOfTypeAll<Ensayne.TextDisplaySystem.TextDisplayer>())
            {
                setIconForObjectMethod.Invoke(null, new object[] { obj, _icon });
            }
        }
    }
}
