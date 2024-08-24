using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCanvas : MonoBehaviour
{
    [SerializeField] private DialoguePlayer _dialoguePlayer;
    [SerializeField] private ChoiceUI _choiceUI;
    public DialoguePlayer DialoguePlayer { get => _dialoguePlayer; }
    public ChoiceUI ChoiceUI { get => _choiceUI;  }
}
