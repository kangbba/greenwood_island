using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SystemCanvas : MonoBehaviour
{
    [SerializeField] private Image _screenOverlayFilm;
    [SerializeField] private TextMeshProUGUI _intertitleText;
    [SerializeField] private Transform _fxLayer;
    [SerializeField] private Transform _sfxLayer;
    [SerializeField] private DialoguePlayer _dialoguePlayer;
    [SerializeField] private ChoiceUI _choiceUI;
    [SerializeField] private CutInUI _cutInUI;
    [SerializeField] private Transform _imaginationLayer;
    [SerializeField] private Transform _characterLayerUI;

    [SerializeField] private Letterbox _letterBox;

    [SerializeField] private UserActionWindow _userActionWindow;


    public Image ScreenOverlayFilm { get => _screenOverlayFilm; }  
    public Transform FXLayer { get => _fxLayer; }
    public Transform SFXLayer { get => _sfxLayer; }
    public DialoguePlayer DialoguePlayer { get => _dialoguePlayer; }
    public ChoiceUI ChoiceUI { get => _choiceUI;  }
    public CutInUI CutInUI { get => _cutInUI; }
    public TextMeshProUGUI IntertitleText { get => _intertitleText; }
    public Transform ImaginationLayer { get => _imaginationLayer; }
    public Letterbox LetterBox { get => _letterBox; }
    public Transform CharacterLayerUI { get => _characterLayerUI; }
    public UserActionWindow UserActionWindow { get => _userActionWindow; }
}
