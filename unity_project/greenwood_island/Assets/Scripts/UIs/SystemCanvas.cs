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

/// 상호작용 UI
    [SerializeField] private Transform _userActionWindowLayer;
    [SerializeField] private Transform _choiceUILayer;
    [SerializeField] private Transform _cutInLayer;
    [SerializeField] private UserActionWindow _userActionWindowPrefab;
    [SerializeField] private ChoiceUI _choiceUIPrefab;
    [SerializeField] private CutInUI _cutInUIPrefab;


    [SerializeField] private Transform _imaginationLayer;
    [SerializeField] private Transform _characterLayerUI;

    [SerializeField] private Letterbox _letterBox;

    [SerializeField] private SaveBtn _saveBtn;


    public Image ScreenOverlayFilm { get => _screenOverlayFilm; }  
    public Transform FXLayer { get => _fxLayer; }
    public Transform SFXLayer { get => _sfxLayer; }
    public DialoguePlayer DialoguePlayer { get => _dialoguePlayer; }
    public TextMeshProUGUI IntertitleText { get => _intertitleText; }
    public Transform ImaginationLayer { get => _imaginationLayer; }
    public Letterbox LetterBox { get => _letterBox; }
    public Transform CharacterLayerUI { get => _characterLayerUI; }
    public SaveBtn SaveBtn { get => _saveBtn; }

    // UserActionWindow 인스턴스화 메소드
    public UserActionWindow InstantiateUserActionWindow()
    {
        return Instantiate(_userActionWindowPrefab, _userActionWindowLayer);
    }

    // ChoiceUI 인스턴스화 메소드
    public ChoiceUI InstantiateChoiceUI()
    {
        return Instantiate(_choiceUIPrefab, _choiceUILayer);
    }

    // CutInUI 인스턴스화 메소드
    public CutInUI InstantiateCutInUI()
    {
        return Instantiate(_cutInUIPrefab, _cutInLayer);
    }
}

