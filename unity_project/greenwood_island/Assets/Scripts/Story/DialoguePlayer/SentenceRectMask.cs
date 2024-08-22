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
    private Coroutine _revealCoroutine;

    public enum EFragmentReason
    {
        Regex,
        Overflow,
        LastFragment
    }

    public EFragmentReason FragmentReason { get; set; }
    public string Sentence { get => _sentence; }

    public void Init(string s)
    {
        _sentence = s;
        _sentenceText.SetText(_sentence);
        _sentenceText.ForceMeshUpdate();

        float preferredWidth = _sentenceText.preferredWidth;

        _sentenceText.rectTransform.sizeDelta = new Vector2(preferredWidth, 80f);

        _rectMask.rectTransform.sizeDelta = new Vector2(preferredWidth, 80f);

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
            if(_rectMask == null){
                Debug.Log("rect mask 가 null이므로 break");
                break;
            }
            elapsedTime += Time.deltaTime;
            float newPaddingRight = Mathf.Lerp(startPaddingRight, targetPaddingRight, elapsedTime / revealDuration);
            _rectMask.padding = new Vector4(0, 0, newPaddingRight, 0);
            yield return null;
        }
        if(_rectMask == null){
            Debug.Log("rect mask 가 null이므로 break");
            yield break;
        }
        // 즉시 완료 요청이 있든 없든 최종적으로 패딩을 0으로 설정하여 모든 텍스트가 보이게 함
        _rectMask.padding = new Vector4(0, 0, targetPaddingRight, 0);
    }

    public void StartReveal(float letterDelay)
    {
        _revealCoroutine = StartCoroutine(RevealMask(letterDelay));
    }

    public void RevealInstantly()
    {
        _isRevealingInstantly = true;
        // 즉시 텍스트가 보이도록 패딩을 0으로 설정
        _rectMask.padding = new Vector4(0, 0, 0, 0);

        // 만약 코루틴이 실행 중이라면 중지
        if (_revealCoroutine != null)
        {
            StopCoroutine(_revealCoroutine);
            _revealCoroutine = null;
        }
    }

    public void SetPosition(float x, float y)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x, y);
    }

    private void OnDestroy()
    {
        // 파괴되기 전에 코루틴이 있다면 중지하고 참조 해제
        if (_revealCoroutine != null)
        {
            StopCoroutine(_revealCoroutine);
            _revealCoroutine = null;
        }

        // 참조 해제 및 메모리 관리
        _rectMask = null;
        _sentenceText = null;
    }
}
