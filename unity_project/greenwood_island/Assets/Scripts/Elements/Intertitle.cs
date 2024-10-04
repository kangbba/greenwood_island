using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Intertitle : Element
{
    private string _text;
    private float _enterDuration; // 텍스트가 페이드 인하는 시간
    private float _stayDuration; // 텍스트가 머무르는 시간
    private float _exitDuration; // 텍스트가 페이드 아웃하는 시간
    private Ease _ease; // 페이드 인/아웃에 적용될 이징 타입
    private float _fontSize; // 텍스트 크기

    private TextMeshProUGUI _intertitleText; // SystemCanvas의 Intertitle Text로 바인딩될 예정

    // 생성자: 텍스트, 페이드 인/아웃, 머무르는 시간, 이징 타입, 텍스트 크기를 받음
    public Intertitle(string text, float enterDuration, float stayDuration, float exitDuration, float fontSize = 55f,  Ease ease = Ease.Linear)
    {
        _text = text;
        _enterDuration = enterDuration;
        _stayDuration = stayDuration;
        _exitDuration = exitDuration;
        _ease = ease;
        _fontSize = fontSize;
    }

    // 생성자 오버로드: 하나의 duration을 받아서 3등분하는 방식, 텍스트 크기 포함
    public Intertitle(string text, float totalDuration, float fontSize = 55f, Ease ease = Ease.Linear)
    {
        _text = text;
        _enterDuration = totalDuration / 3f;
        _stayDuration = totalDuration / 3f;
        _exitDuration = totalDuration / 3f;
        _ease = ease;
        _fontSize = fontSize;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // SystemCanvas 안에 바인딩된 Intertitle TextMeshProUGUI를 가져옴
        _intertitleText = UIManager.SystemCanvas.IntertitleText;

        // 텍스트가 비어있을 경우 경고 로그를 발생시키고 종료
        if (string.IsNullOrEmpty(_text))
        {
            Debug.LogWarning("Intertitle: Text is empty, cannot display intertitle.");
            yield break;
        }

        // 텍스트 설정 및 초기화
        _intertitleText.gameObject.SetActive(true);
        _intertitleText.text = _text;
        _intertitleText.fontSize = _fontSize;
        _intertitleText.alpha = 0f;

        // 페이드 인
        _intertitleText.DOFade(1f, _enterDuration).SetEase(_ease);
        yield return new WaitForSeconds(_enterDuration);

        // 텍스트가 화면에 머무르는 시간
        yield return new WaitForSeconds(_stayDuration);

        // 페이드 아웃
        _intertitleText.DOFade(0f, _exitDuration).SetEase(_ease);

        // 페이드 아웃 동안 대기
        yield return new WaitForSeconds(_exitDuration);

        // 텍스트 비활성화
        _intertitleText.gameObject.SetActive(false);
    }
}
