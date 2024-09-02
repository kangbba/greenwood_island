

[System.Serializable]
public class Line
{
    private EEmotionID _emotionID;
    private int _emotionIndex;
    private string _sentence;

    public Line(EEmotionID emotionID, int emotionIndex, string sentence)
    {
        this._emotionID = emotionID;
        this._emotionIndex = emotionIndex;
        this._sentence = sentence;
    }

    public EEmotionID EmotionID { get => _emotionID;  }
    public int EmotionIndex { get => _emotionIndex; }
    public string Sentence { get => _sentence; }
}