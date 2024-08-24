using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StoryNode))]
public class StoryNodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        StoryNode node = (StoryNode)target;

        GUILayout.Space(10);

        // 현재 스토리의 다음 스토리 노드 설정
        EditorGUILayout.LabelField("Next Story Node", EditorStyles.boldLabel);
        node.nextStoryNode = (StoryNode)EditorGUILayout.ObjectField(node.nextStoryNode, typeof(StoryNode), false);

        if (node.nextStoryNode != null)
        {
            EditorGUILayout.HelpBox($"This story will transition to {node.nextStoryNode.storyName}.", MessageType.Info);
        }
        else
        {
            EditorGUILayout.HelpBox("No next story assigned.", MessageType.Warning);
        }
    }
}
