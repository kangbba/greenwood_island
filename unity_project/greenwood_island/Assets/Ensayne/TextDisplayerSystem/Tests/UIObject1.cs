using UnityEngine;
using Ensayne.TextDisplayerSystem;
using TMPro;

public class UIObject1 : MonoBehaviour
{
    [SerializeField] private TextDisplayer _textDisplayer;

    void Start()
    {
        string[] texts = { "안녕?", "난 UI 객체 1이야", "만나서 반가워" };
        _textDisplayer.StartDisplay(texts);

        _textDisplayer.OnStartTyping += OnStartTyping;
        _textDisplayer.OnFinishTypingSentence += OnFinishTypingSentence;
        _textDisplayer.OnFinishAllSentences += OnFinishAllSentences;
        _textDisplayer.OnSystemDestroyed += OnSystemDestroyed;
        _textDisplayer.OnSystemStarted += OnSystemStarted;
    }

    void Update()
    {
        // 예시: 마우스 클릭 시 다음 텍스트 표시
        if (Input.GetMouseButtonDown(0))
        {
            _textDisplayer.ShowNext();
        }
    }

    private void OnStartTyping()
    {
        Debug.Log("UIObject1: 타이핑 시작");
    }

    private void OnFinishTypingSentence()
    {
        Debug.Log("UIObject1: 문장 타이핑 완료");
    }

    private void OnFinishAllSentences()
    {
        Debug.Log("UIObject1: 모든 문장 타이핑 완료");
    }

    private void OnSystemDestroyed()
    {
        Debug.Log("UIObject1: 시스템 파괴");
    }

    private void OnSystemStarted()
    {
        Debug.Log("UIObject1: 시스템 시작");
    }
}
