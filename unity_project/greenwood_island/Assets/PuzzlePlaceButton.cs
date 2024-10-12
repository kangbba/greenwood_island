using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PuzzlePlaceButton : MonoBehaviour
{
    public enum PuzzleButtonType
    {
        Move,    // 특정 장소로 이동
        Event    // 이벤트 발생
    }

    [SerializeField] private string btnID;
    [SerializeField] private PuzzleButtonType _buttonType;  // 버튼 타입
    [SerializeField] private PuzzlePlace _puzzlePlaceToMove;  // 이동할 PuzzlePlace
    [SerializeField] private Image _graphic;  // 그래픽 이미지
    [SerializeField] private Image _graphicBackground;  // 배경 이미지

    private Button _button;
    private Puzzle _parentPuzzle;  // 부모 Puzzle 인스턴스

    // **프로퍼티들** (게터와 세터)
    public PuzzleButtonType ButtonType
    {
        get => _buttonType;
        set => _buttonType = value;
    }

    public PuzzlePlace PuzzlePlaceToMove
    {
        get => _puzzlePlaceToMove;
        set => _puzzlePlaceToMove = value;
    }

    public Image Graphic
    {
        get => _graphic;
        set => _graphic = value;
    }

    public Image GraphicBackground
    {
        get => _graphicBackground;
        set => _graphicBackground = value;
    }

    // 초기화 메서드 (리스너에 자기 자신 전달)
    public void Initialize(Puzzle parentPuzzle, System.Action<PuzzlePlaceButton> onClickAction)
    {
        _parentPuzzle = parentPuzzle;
        _button = GetComponent<Button>();

        // 버튼 클릭 시 리스너에 자신을 전달
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => onClickAction(this));
    }
}
