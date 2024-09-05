using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class EmotionPlan
{
    [SerializeField] private EEmotionID _emotionID; // Emotion ID (e.g., "Happy", "Sad")
    [SerializeField] private Sprite[] _emotionSprites; // Array of sprites corresponding to the emotion

    // Encapsulated properties for access
    public EEmotionID EmotionID => _emotionID;
    public Sprite[] EmotionSprites => _emotionSprites;
}

public class EmotionPlansData : ScriptableObject
{
    const string defaultPath = "Assets/CharacterEmotionDatas";
    [SerializeField] private List<EmotionPlan> _emotionPlans; // 감정 계획 리스트를 저장

    // Encapsulated property for access
    public List<EmotionPlan> EmotionPlans => _emotionPlans;

    // 상단 메뉴에서 EmotionPlansData를 생성하도록 MenuItem 추가
    [MenuItem("GreenWoodIsland/Create Emotion Plans Data")]
    public static void CreateEmotionPlansData()
    {
        // 기본 경로 설정
        
        // 폴더가 존재하지 않으면 생성
        if (!AssetDatabase.IsValidFolder(defaultPath))
        {
            AssetDatabase.CreateFolder("Assets", "CharacterDatas");
        }

        // 파일명 설정 및 에셋 생성
        string path = AssetDatabase.GenerateUniqueAssetPath($"{defaultPath}/NewEmotionPlansData.asset");
        EmotionPlansData asset = ScriptableObject.CreateInstance<EmotionPlansData>();
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();

        // 생성된 에셋을 프로젝트 창에서 선택
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;

        Debug.Log($"새로운 EmotionPlansData가 생성되었습니다: {path}");
    }
}
