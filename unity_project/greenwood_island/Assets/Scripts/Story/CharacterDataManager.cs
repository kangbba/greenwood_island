using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
}

public class CharacterDataManager : MonoBehaviour
{
    // Singleton instance
    private static CharacterDataManager _instance;
    public static CharacterDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CharacterDataManager>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private List<CharacterData> _characterDatas;

    public CharacterData GetCharacterData(ECharacterID characterID)
    {
        return _characterDatas.FirstOrDefault(data => data.characterID == characterID);
    }
}
