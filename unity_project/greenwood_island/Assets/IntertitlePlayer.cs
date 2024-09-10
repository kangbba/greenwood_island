using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntertitlePlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textObject; // TextMeshProUGUI 오브젝트를 미리 바인딩
    [SerializeField] private RectMask2D _rectMask;       // RectMask2D를 인스펙터에서 미리 바인딩

    private Coroutine _currentRoutine;

    /// <summary>
    /// 지정된 텍스트를 설정된 시간 동안 한 줄씩 노출시킵니다.
    /// </summary>
    public void ShowIntertitle(string text, float duration)
    {
        // duration이 0일 경우 경고 로그를 발생시키고 종료
        if (duration <= 0)
        {
            Debug.LogWarning("IntertitlePlayer: Duration is set to 0 or less, which makes the intertitle display meaningless.");
            return;
        }

        // 현재 실행 중인 코루틴이 있으면 중지하고 텍스트 초기화
        if (_currentRoutine != null) StopCoroutine(_currentRoutine);
        _textObject.text = "";

        // 코루틴 시작
        _currentRoutine = StartCoroutine(RevealAndShowIntertitle(text, duration));
    }

    /// <summary>
    /// 텍스트를 한 줄씩 노출하고 설정된 지속 시간 동안 유지한 후 텍스트 초기화합니다.
    /// </summary>
    private IEnumerator RevealAndShowIntertitle(string text, float duration)
    {
        // 텍스트 노출
        yield return RevealTextLines(text);

        // 설정된 duration 만큼 대기
        yield return new WaitForSeconds(duration);

        // 텍스트 초기화
        _textObject.text = "";
    }

    /// <summary>
    /// 텍스트를 한 줄씩 노출하는 기능을 수행합니다.
    /// </summary>
    private IEnumerator RevealTextLines(string text)
    {
        // 텍스트를 라인별로 분할
        string[] lines = text.Split('\n');
        _textObject.text = "";

        // 마스크 초기 상태 설정
        float lineHeight = _textObject.fontSize * 1.2f; // 줄 간격을 폰트 크기를 기준으로 설정
        Vector4 initialPadding = _rectMask.padding;

        // 마스크 패딩을 위에서 아래로 이동시키면서 한 줄씩 노출
        for (int i = 0; i < lines.Length; i++)
        {
            _textObject.text += lines[i] + "\n";
            _rectMask.padding = new Vector4(0, -lineHeight * (i + 1), 0, 0); // 패딩을 줄여서 한 줄씩 보이게 설정

            // 한 줄씩 보여주는 효과를 위한 딜레이, 필요에 따라 조정 가능
            yield return new WaitForSeconds(0.5f);
        }

        // 마스크 패딩을 초기 상태로 되돌림
        _rectMask.padding = initialPadding;
    }
}
