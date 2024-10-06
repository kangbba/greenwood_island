using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OkPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _popupText;         // 팝업에 표시될 메시지
    [SerializeField] private Button _okButton;       // "확인" 버튼

    // 팝업을 초기화하는 메서드
    public void Init(string message, string okText, System.Action onOkAction)
    {
        // 메시지 및 버튼 텍스트 설정
        _popupText.text = message;
        _okButton.GetComponentInChildren<TextMeshProUGUI>().text = okText;

        // "확인" 버튼 클릭 리스너 설정
        _okButton.onClick.RemoveAllListeners();  // 기존 리스너 제거
        _okButton.onClick.AddListener(() =>
        {
            onOkAction?.Invoke();  // 확인 액션 실행
            Destroy(gameObject);   // 팝업 파괴
        });
    }
}
