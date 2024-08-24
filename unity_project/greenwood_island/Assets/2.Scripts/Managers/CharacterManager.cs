using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public enum ECharacterID
{
    Ryan,
    Kate,
    Lisa,
    Joseph,
    Rachel,
}

[System.Serializable]
public class CharacterData
{
    public ECharacterID characterID;
    public Character characterPrefab;
    public string characterName_ko;
    public Color mainColor;
}

public class CharacterManager : MonoBehaviour
{
    // Singleton instance
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CharacterManager>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private List<CharacterData> _characterDatas;

    // Dictionary to hold instantiated characters
    private Dictionary<ECharacterID, Character> _instantiatedCharacters = new Dictionary<ECharacterID, Character>();

    public CharacterData GetCharacterData(ECharacterID characterID)
    {
        return _characterDatas.FirstOrDefault(data => data.characterID == characterID);
    }

    // Utility method to check if a character exists
    public bool IsExist(ECharacterID characterID)
    {
        return _instantiatedCharacters.ContainsKey(characterID);
    }

    // Utility method to get the count of active characters
    public int GetActiveCharacterCount()
    {
        return _instantiatedCharacters.Count;
    }

    // Utility method to get all active characters
    public List<Character> GetAllActiveCharacters()
    {
        return _instantiatedCharacters.Values.ToList();
    }

    public Character InstantiateCharacter(ECharacterID characterID, float screenPeroneX)
    {
        CharacterData data = GetCharacterData(characterID);

        if (data == null || data.characterPrefab == null)
        {
            Debug.LogError($"Failed to instantiate character for {characterID}. Check if the prefab is assigned.");
            return null;
        }

        if (IsExist(characterID))
        {
            Debug.LogWarning($"Character with ID {characterID} is already instantiated.");
            return _instantiatedCharacters[characterID];
        }

        Character character = Instantiate(data.characterPrefab, UIManager.Instance.WorldCanvas.CharacterLayer.transform);
        // 캐릭터를 딕셔너리에 등록
        _instantiatedCharacters.Add(characterID, character);

        // 캐릭터를 생성한 후, 즉시 이동(애니메이션 없이) 시키기 위해 CharacterMove 사용
        CharacterMove move = new CharacterMove(characterID, screenPeroneX, 0f, Ease.Linear);
        StartCoroutine(move.Execute());

        return character;
    }

    public void DestroyCharacter(ECharacterID characterID)
    {
        if (_instantiatedCharacters.TryGetValue(characterID, out Character character))
        {
            Destroy(character.gameObject);
            _instantiatedCharacters.Remove(characterID);
        }
        else
        {
            Debug.LogWarning($"No instantiated character found with ID {characterID} to destroy.");
        }
    }

    public Character GetActiveCharacter(ECharacterID characterID)
    {
        if (IsExist(characterID))
        {
            return _instantiatedCharacters[characterID];
        }
        else
        {
            Debug.LogWarning($"No active character found with ID {characterID}.");
            return null;
        }
    }

    // 캐릭터의 감정 상태를 설정하는 메서드
    public void SetCharacterEmotion(ECharacterID characterID, EEmotionID emotionID, int emotionIndex)
    {
        Character character = GetActiveCharacter(characterID);
        if (character != null)
        {
            character.ChangeEmotion(emotionID, emotionIndex, 0f);
        }
    }

    // Utility method to reset all characters
    public void ResetAllCharacters()
    {
        foreach (var character in _instantiatedCharacters.Values)
        {
            Destroy(character.gameObject);
        }
        _instantiatedCharacters.Clear();
    }
}
