using System;

[System.Serializable]
public class Line
{
    private EmotionType _emotionType;   // EmotionType enum 사용
    private int _emotionIndex;
    private string _sentence;
    private float _playSpeed;

    // 생성자에서 emotionID 대신 EmotionType을 받도록 수정
    public Line(string sentence, EmotionType emotionType = EmotionType.Happy, int emotionIndex = 0, float playSpeed = 1200)
    {
        this._emotionType = emotionType;   // EmotionType으로 설정
        this._emotionIndex = emotionIndex;
        this._sentence = sentence;
        this._playSpeed = playSpeed;
    }

    // EmotionType enum을 반환하는 프로퍼티
    public EmotionType EmotionType { get => _emotionType; }
    public int EmotionIndex { get => _emotionIndex; }
    public string Sentence { get => _sentence; }
    public float PlaySpeed { get => _playSpeed; }
}
