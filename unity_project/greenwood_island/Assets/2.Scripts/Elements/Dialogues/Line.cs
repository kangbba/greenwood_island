

[System.Serializable]
public class Line
{
    private EEmotionID _emotionID;
    private int _emotionIndex;
    private string _sentence;
    private float _playSpeed;

    public Line(EEmotionID emotionID, int emotionIndex, string sentence, float playSpeed = 30)
    {
        this._emotionID = emotionID;
        this._emotionIndex = emotionIndex;
        this._sentence = sentence;
        this._playSpeed = playSpeed;
    }

    public EEmotionID EmotionID { get => _emotionID;  }
    public int EmotionIndex { get => _emotionIndex; }
    public string Sentence { get => _sentence; }
    public float PlaySpeed { get => _playSpeed; }
}