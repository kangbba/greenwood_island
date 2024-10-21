using System;

public class Line
{
    private string _sentence;
    private float _playSpeedMutliplier;

    // 생성자에서 emotionID 대신 EmotionType을 받도록 수정
    public Line(string sentence, float playSpeedMutliplier = 1)
    {
        this._sentence = sentence;
        this._playSpeedMutliplier = playSpeedMutliplier;
    }

    public string Sentence { get => _sentence; }
    public float PlaySpeedMultiplier { get => _playSpeedMutliplier; }
}
