using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string _characterID;         // 캐릭터 ID
    [SerializeField] private string _characterName_Ko;    // 캐릭터 이름 (한국어)
    [SerializeField] private Color _characterColor;       // 캐릭터 대표 색상

    // 프로퍼티를 통해 캡슐화된 필드 읽기 전용 접근
    public string CharacterID => _characterID;

    public string CharacterName_Ko => _characterName_Ko;

    public Color CharacterColor => _characterColor;
}
