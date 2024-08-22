using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SentenceRectMask : MonoBehaviour
{
    [SerializeField] private RectMask2D _rectMask;
    [SerializeField] private TextMeshProUGUI _sentenceText;
    private string _sentence;
    private bool _isRevealingInstantly = false;

    public enum EFragmentReason
    {
        Regex,
        Overflow,
        LastFragment
    }

    public EFragmentReason FragmentReason { get; set; }

    public void Init(string s)
    {
        _sentence = s;
        _sentenceText.SetText(_sentence);
        _sentenceText.ForceMeshUpdate();

        float preferredWidth = _sentenceText.preferredWidth;

        _sentenceText.rectTransform.sizeDelta = new Vector2(preferredWidth, 80f);

        RectTransform parentRectTransform = _rectMask.GetComponent<RectTransform>();
        parentRectTransform.sizeDelta = new Vector2(preferredWidth, 80f);

        _rectMask.padding = new Vector4(0, 0, preferredWidth, 0);
    }

    public IEnumerator RevealMask(float letterDelay)
    {
        _isRevealingInstantly = false;

        float startPaddingRight = _rectMask.padding.z;
        float targetPaddingRight = 0f;

        float revealDuration = _sentence.Length * letterDelay;
        float elapsedTime = 0f;

        while (elapsedTime < revealDuration)
        {
            if (_isRevealingInstantly)
            {
                break;
            }

            elapsedTime += Time.deltaTime;
            float newPaddingRight = Mathf.Lerp(startPaddingRight, targetPaddingRight, elapsedTime / revealDuration);
            _rectMask.padding = new Vector4(0, 0, newPaddingRight, 0);
            yield return null;
        }

        // 즉시 완료 요청이 있든 없든 최종적으로 패딩을 0으로 설정하여 모든 텍스트가 보이게 함
        _rectMask.padding = new Vector4(0, 0, targetPaddingRight, 0);
    }

    public void RevealInstantly()
    {
        _isRevealingInstantly = true;
        // 즉시 텍스트가 보이도록 패딩을 0으로 설정
        _rectMask.padding = new Vector4(0, 0, 0, 0);
    }

    public void SetPosition(float x, float y)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x, y);
    }
}
