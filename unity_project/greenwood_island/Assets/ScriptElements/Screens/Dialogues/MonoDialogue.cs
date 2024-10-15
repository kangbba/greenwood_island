using System;
using System.Collections;

public class MonoDialogue : Element
{
    private string _sentence;

    // 생성자에서 emotionID 대신 EmotionType을 받도록 수정
    public MonoDialogue(string sentence)
    {
        _sentence = sentence;
    }

    public override void ExecuteInstantly()
    {
    }

    public override IEnumerator ExecuteRoutine()
    {
        yield return CoroutineUtils.StartCoroutine(new Dialogue("Mono", new Line(_sentence), afterPanelDown : true).ExecuteRoutine());
    }
}
