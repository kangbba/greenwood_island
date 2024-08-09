using System.Collections;
using System.Collections.Generic;
using Ensayne.TextDisplayerSystem;
using UnityEngine;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField] private TextDisplayer _textDisplayer;

    void Start()
    {
        string[] texts = { "... 갑자기 왜 그래?", "난 UI 객체 1이야", "만나서 반가워" };
        _textDisplayer.Init(texts);
    }
}
