using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Dialogue
{
    private ECharacterID _characterID;
    private List<Line> _lines;
    private float _screenPeroneX; // 화면 좌우 비율 (0.0f: 왼쪽, 1.0f: 오른쪽)

    public Dialogue(ECharacterID characterID, List<Line> lines, float screenPeroneX)
    {
        this._characterID = characterID;
        this._lines = lines;
        this._screenPeroneX = screenPeroneX;
    }

    public ECharacterID CharacterID { get => _characterID; }
    public List<Line> Lines { get => _lines; }
    public float ScreenPeroneX { get => _screenPeroneX; }
}