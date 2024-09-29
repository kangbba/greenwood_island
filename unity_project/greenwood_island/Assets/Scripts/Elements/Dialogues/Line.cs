

[System.Serializable]
public class Line
{
    private string _emotionID;
    private int _emotionIndex;
    private string _sentence;
    private float _playSpeed;

    public Line(string sentence, string emotionID = "Smile", int emotionIndex = 0, float playSpeed = 1500)
    {
        this._emotionID = emotionID;
        this._emotionIndex = emotionIndex;
        this._sentence = sentence;
        this._playSpeed = playSpeed;
    }

    public string EmotionID { get => _emotionID;  }
    public int EmotionIndex { get => _emotionIndex; }
    public string Sentence { get => _sentence; }
    public float PlaySpeed { get => _playSpeed; }
}