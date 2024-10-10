using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YesNoPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _popupText;         // 팝업에 표시될 메시지
    [SerializeField] private Button _yesButton;       // "예" 버튼
    [SerializeField] private Button _noButton;        // "아니오" 버튼

    // 팝업을 초기화하는 메서드
    public void Init(string message, string yesText, string noText, System.Action onYesAction, System.Action onNoAction = null)
    {
        // 메시지 및 버튼 텍스트 설정
        _popupText.text = message;
        _yesButton.GetComponentInChildren<TextMeshProUGUI>().text = yesText;
        _noButton.GetComponentInChildren<TextMeshProUGUI>().text = noText;

        // "예" 버튼 클릭 리스너 설정
        _yesButton.onClick.RemoveAllListeners();  // 기존 리스너 제거
        _yesButton.onClick.AddListener(() =>
        {
            onYesAction?.Invoke();  // 예 액션 실행
            Destroy(gameObject);    // 팝업 파괴
        });

        // "아니오" 버튼 클릭 리스너 설정
        _noButton.onClick.RemoveAllListeners();   // 기존 리스너 제거
        _noButton.onClick.AddListener(() =>
        {
            onNoAction?.Invoke();  // 아니오 액션 실행
            Destroy(gameObject);   // 팝업 파괴
        });
    }
}
