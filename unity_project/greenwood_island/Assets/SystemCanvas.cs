using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCanvas : MonoBehaviour
{
    [SerializeField] private DialoguePlayer _dialoguePlayer;
    public DialoguePlayer DialoguePlayer { get => _dialoguePlayer; }
}
