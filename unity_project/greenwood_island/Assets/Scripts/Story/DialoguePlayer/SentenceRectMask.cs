using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SentenceRectMask : MonoBehaviour
{
    // RectMask2D 컴포넌트를 위한 SerializeField
    [SerializeField] private RectMask2D _rectMask;

    // TextMeshProUGUI 컴포넌트를 위한 SerializeField
    [SerializeField] private TextMeshProUGUI _sentenceText;
    private string _sentence; // Init에서 전달된 텍스트를 저장하는 변수
    private bool _isRevealingInstantly = false; // 즉시 완료 플래그

    // 조각난 이유를 설명하는 Enum
    public enum EFragmentReason
    {
        Regex,       // Regex로 인한 것
        Overflow,    // Overflow로 인한 것
        LastFragment // 마지막 조각
    }

    public EFragmentReason FragmentReason { get; set; }

    // 초기화 메서드
    public void Init(string s)
    {
        _sentence = s;

        _sentenceText.SetText(_sentence);
        _sentenceText.ForceMeshUpdate(); // 텍스트의 프리퍼드 사이즈를 정확히 가져오기 위해 강제 업데이트

        float preferredWidth = _sentenceText.preferredWidth;

        // 텍스트의 크기를 업데이트, 높이는 항상 80으로 설정
        _sentenceText.rectTransform.sizeDelta = new Vector2(preferredWidth, 80f);

        // 부모의 RectTransform도 동일하게 크기를 설정, 높이는 80으로 고정
        RectTransform parentRectTransform = _rectMask.GetComponent<RectTransform>();
        parentRectTransform.sizeDelta = new Vector2(preferredWidth, 80f);

        // RectTransform의 패딩 설정
        _rectMask.padding = new Vector4(0, 0, preferredWidth, 0); // 오른쪽 패딩을 preferredWidth로 설정
    }

    // RevealMask 코루틴
    public IEnumerator RevealMask(float letterDelay)
    {
        _isRevealingInstantly = false; // 즉시 완료 플래그 초기화

        float startPaddingRight = _rectMask.padding.z;
        float targetPaddingRight = 0f;

        float revealDuration = _sentence.Length * letterDelay;
        float elapsedTime = 0f;

        while (elapsedTime < revealDuration)
        {
            if (_isRevealingInstantly)
            {
                break; // 즉시 완료 요청이 들어오면 루프 중단
            }

            elapsedTime += Time.deltaTime;
            float newPaddingRight = Mathf.Lerp(startPaddingRight, targetPaddingRight, elapsedTime / revealDuration);
            _rectMask.padding = new Vector4(0, 0, newPaddingRight, 0);
            yield return null;
        }

        // 즉시 완료 요청이 있든 없든 최종적으로 패딩을 0으로 설정하여 모든 텍스트가 보이게 함
        _rectMask.padding = new Vector4(0, 0, targetPaddingRight, 0);
    }

    // 즉시 완료 메서드
    public void RevealInstantly()
    {
        _isRevealingInstantly = true;
    }

    // SetPosition 메서드
    public void SetPosition(float x, float y)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x, y);
    }
}
