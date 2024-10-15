using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SystemCanvas : MonoBehaviour
{
    [SerializeField] private Transform _placeLayer;
    [SerializeField] private Image _placeOverlayFilm;
    [SerializeField] private Transform _characterLayer;
    [SerializeField] private Transform _imaginationLayer;
    [SerializeField] private TextMeshProUGUI _intertitleText;
    [SerializeField] private Transform _fxLayer;
    [SerializeField] private Transform _puzzleUILayer;
    [SerializeField] private DialoguePlayer _dialoguePlayer;
    [SerializeField] private Transform _cutInLayer;
    [SerializeField] private Transform _userActionWindowLayer;
    [SerializeField] private Transform _choiceUILayer;
    [SerializeField] private Image _screenOverlayFilm;
    [SerializeField] private Transform _sfxLayer;

/// 상호작용 UI
    [SerializeField] private UserActionWindow _userActionWindowPrefab;
    [SerializeField] private ChoiceUI _choiceUIPrefab;
    [SerializeField] private CutInUI _cutInUIPrefab;
    [SerializeField] private Button _placeTransitionBtnPrefab;



    [SerializeField] private Letterbox _letterBox;

    [SerializeField] private Button _saveBtn;

    [SerializeField] private Button _inventoryBtn;

    public Transform PlaceLayer { get => _placeLayer; }
    public Image PlaceOverlayFilm { get => _placeOverlayFilm;  }
    public Transform CharacterLayer { get => _characterLayer; }
    public Transform ImaginationLayer { get => _imaginationLayer; }
    public TextMeshProUGUI IntertitleText { get => _intertitleText; }
    public Transform FXLayer { get => _fxLayer;  }
    public Transform PuzzleUILayer { get => _puzzleUILayer; }
    public DialoguePlayer DialoguePlayer { get => _dialoguePlayer; }
    public Transform CutInLayer { get => _cutInLayer; }
    public Transform UserActionWindowLayer { get => _userActionWindowLayer; }
    public Transform ChoiceUILayer { get => _choiceUILayer;  }
    public Image ScreenOverlayFilm { get => _screenOverlayFilm; }
    public Letterbox LetterBox { get => _letterBox; }
    public Transform SFXLayer { get => _sfxLayer; }

    private void Start(){
        _saveBtn.onClick.RemoveAllListeners();
        _saveBtn.onClick.AddListener(() =>
        {
            StorySavedData tempSaveData = new StorySavedData(
                StoryManager.Instance.CurrentStoryName, 
                "퀵 세이브", 
                StoryManager.Instance.CurrentElementIndex, 
                StoryManager.Instance.CurrentStory.UpdateElements.Count
            );

            UIManager.PopupCanvas.ShowSaveWindow(tempSaveData);
        });

        _inventoryBtn.onClick.RemoveAllListeners();
        _inventoryBtn.onClick.AddListener(() =>
        {
            // 인벤토리 버튼을 눌렀을 때 동작 추가 (여기에 인벤토리 관련 로직을 넣을 수 있음)
        });
    }


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

