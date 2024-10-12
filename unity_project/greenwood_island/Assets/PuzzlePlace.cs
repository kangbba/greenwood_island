using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PuzzlePlace : MonoBehaviour
{
    [SerializeField] private string eventID;
    private Image _placeImg;  // 자식 버튼 목록
    private CanvasGroup _canvasGroup;  // 이 PuzzlePlace의 CanvasGroup
    private List<PuzzlePlaceButton> _placeButtons;  // 자식 버튼 목록
    private Puzzle _parentPuzzle;  // 부모 Puzzle 인스턴스

    public PuzzlePlace ParentPlace { get; private set; }  // 상위 PuzzlePlace
    public string EventID { get => eventID; }

    private void Awake(){

        _placeImg = GetComponent<Image>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _placeImg.raycastTarget = false;
        _canvasGroup.ignoreParentGroups = true;
        ParentPlace = transform.parent.GetComponent<PuzzlePlace>();
        _placeButtons = new List<PuzzlePlaceButton>(GetComponentsInChildren<PuzzlePlaceButton>(true));
        Show(false, 0f);
    }
    // 부모 Puzzle을 전달받아 초기화
    public void Initialize(Puzzle parentPuzzle)
    {
        _parentPuzzle = parentPuzzle;
        // 자식 버튼들 초기화
        foreach (var button in _placeButtons)
        {
            button.Initialize(parentPuzzle, HandleButtonAction);
        }
    }

   public void Show(bool isVisible, float duration, System.Action onComplete = null)
    {
        if (isVisible)
        {
            // Fade-In 애니메이션 및 완료 시 Raycast와 Interactable 설정
            _canvasGroup.DOFade(1f, duration).OnComplete(() =>
            {
                _canvasGroup.blocksRaycasts = true;
                _canvasGroup.interactable = true;
                SetButtonsInteractable(true);

                // 추가 작업 실행 (onComplete 콜백)
                onComplete?.Invoke();
            });
        }
        else
        {
            // Raycast 및 Interactable 비활성화 후 Fade-Out 애니메이션
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            SetButtonsInteractable(false);

            _canvasGroup.DOFade(0f, duration).OnComplete(() =>
            {
                // 추가 작업 실행 (onComplete 콜백)
                onComplete?.Invoke();
            });
        }
    }

    // 모든 버튼의 Interactable 상태를 설정하는 메서드
    private void SetButtonsInteractable(bool interactable)
    {
        foreach (var button in _placeButtons)
        {
            button.GetComponent<Button>().interactable = interactable;
        }
    }

    // 버튼의 동작을 처리하는 메서드 (기존 ExecuteAction 로직 이동)
    public void HandleButtonAction(PuzzlePlaceButton button)
    {
        Debug.Log($"버튼 클릭됨: {button.name}");

        switch (button.ButtonType)
        {

            case PuzzlePlaceButton.PuzzleButtonType.Move:
                if (button.PuzzlePlaceToMove != null)
                {
                    _parentPuzzle.MoveToPlace(button.PuzzlePlaceToMove);
                }
                else
                {
                    Debug.LogWarning($"{button.name}: 이동할 PuzzlePlace가 설정되지 않았습니다.");
                }
                break;
            case PuzzlePlaceButton.PuzzleButtonType.Event:
             //   ItemManager.AddItem(button.ItemGetID, GameDataManager.CurrentStorySavedData);
                break;
        }
    }
}
