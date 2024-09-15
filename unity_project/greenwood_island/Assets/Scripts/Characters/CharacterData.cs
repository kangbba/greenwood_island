using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SpriteWithSettings
{
    [SerializeField] private Sprite _sprite; // 스프라이트 이미지
    [SerializeField] private Vector2 _offset; // 스프라이트의 오프셋

    // Encapsulated properties for access
    public Sprite Sprite => _sprite;
    public Vector2 Offset => _offset;
}

[System.Serializable]
public class EmotionPlan
{
    [SerializeField] private string _emotionID; // Emotion ID (e.g., "Happy", "Sad")
    [SerializeField] private SpriteWithSettings[] _emotionSprites; // 스프라이트와 설정 배열

    // Encapsulated properties for access
    public string EmotionID => _emotionID;
    public SpriteWithSettings[] EmotionSprites => _emotionSprites;
}

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "GreenwoodIsland/Character Data")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string _characterID; // 캐릭터의 ID
    [SerializeField] private string _characterName_KO; // 캐릭터의 이름 (한국어)
    [SerializeField] private Color _mainColor; // 캐릭터의 메인 색상
    [SerializeField] private List<EmotionPlan> _emotionPlans; // 감정 계획 리스트

    // Encapsulated properties for access
    public string CharacterID => _characterID;
    public string CharacterName_KO => _characterName_KO;
    public Color MainColor => _mainColor;
    public List<EmotionPlan> EmotionPlans => _emotionPlans;
}
