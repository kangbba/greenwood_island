using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(PuzzlePlaceButton))]
public class PuzzlePlaceButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 대상 버튼 가져오기
        PuzzlePlaceButton button = (PuzzlePlaceButton)target;

        // 항상 표시되는 필드: 그래픽 이미지와 배경 이미지
        button.Graphic = (Image)EditorGUILayout.ObjectField(
            "Graphic", button.Graphic, typeof(Image), true);
        button.GraphicBackground = (Image)EditorGUILayout.ObjectField(
            "Graphic Background", button.GraphicBackground, typeof(Image), true);

        // 버튼 타입 선택 (Enum Popup)
        button.ButtonType = (PuzzlePlaceButton.PuzzleButtonType)
            EditorGUILayout.EnumPopup("Button Type", button.ButtonType);

        // 조건부 필드 표시
        ShowConditionalFields(button);

        // 변경 사항이 있을 때 처리
        if (GUI.changed)
        {
            ResetUnusedFields(button);  // 필요 없는 필드 초기화
            EditorUtility.SetDirty(button);  // 변경 사항 저장
        }
    }

    // 버튼 타입에 따른 조건부 필드 표시
    private void ShowConditionalFields(PuzzlePlaceButton button)
    {
        switch (button.ButtonType)
        {
            case PuzzlePlaceButton.PuzzleButtonType.Move:
                // 이동할 PuzzlePlace를 설정하는 필드
                button.PuzzlePlaceToMove = (PuzzlePlace)EditorGUILayout.ObjectField(
                    "Puzzle Place To Move",
                    button.PuzzlePlaceToMove,
                    typeof(PuzzlePlace),
                    true
                );
                break;
        }
    }

    // 사용하지 않는 필드 초기화
    private void ResetUnusedFields(PuzzlePlaceButton button)
    {
        // switch (button.ButtonType)
        // {
        //     case PuzzlePlaceButton.PuzzleButtonType.FindItem:
        //     case PuzzlePlaceButton.PuzzleButtonType.SpendItem:
        //         // 아이템 관련 버튼에서는 이동 필드 초기화
        //         button.PuzzlePlaceToMove = null;
        //         break;

        //     case PuzzlePlaceButton.PuzzleButtonType.Move:
        //         // 이동 버튼에서는 아이템 ID를 초기화
        //         button.ItemGetID = string.Empty;
        //         break;
        // }
    }

    // 씬 뷰에 기즈모 표시 (버튼 정보를 라벨로 표시)
    private void OnSceneGUI()
    {
        PuzzlePlaceButton button = (PuzzlePlaceButton)target;

        // 버튼 위치 위쪽에 라벨 표시
        Vector3 labelPosition = button.transform.position + Vector3.up * 0.5f;

        // 버튼 타입에 따라 기즈모 라벨 표시
        switch (button.ButtonType)
        {
            // case PuzzlePlaceButton.PuzzleButtonType.FindItem:
            //     DrawGizmoLabel(labelPosition, $"+ {button.ItemGetID}", Color.green);
            //     break;

            // case PuzzlePlaceButton.PuzzleButtonType.SpendItem:
            //     DrawGizmoLabel(labelPosition, $"- {button.ItemGetID}", Color.red);
            //     break;

            case PuzzlePlaceButton.PuzzleButtonType.Move:
                string placeName = button.PuzzlePlaceToMove != null
                    ? button.PuzzlePlaceToMove.gameObject.name
                    : "None";
                DrawGizmoLabel(labelPosition, $"→ {placeName}", Color.cyan);
                break;
        }
    }

    // 기즈모 라벨을 그리는 헬퍼 함수
    private void DrawGizmoLabel(Vector3 position, string text, Color color)
    {
        GUIStyle style = new GUIStyle
        {
            normal = new GUIStyleState { textColor = color },
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };

        Handles.Label(position, text, style);  // 라벨을 씬 뷰에 표시
    }
}
