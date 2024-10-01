
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
public class LetterboxClear : Element
{
    private float _duration;

    // 생성자에서 기본값 1을 설정
    public LetterboxClear(float duration = 1f)
    {
        _duration = duration;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // 1. 레터박스 초기화 및 활성화
        Letterbox letterbox = UIManager.Instance.SystemCanvas.LetterBox;
        letterbox.gameObject.SetActive(true);
        letterbox.SetOn(false, _duration);  // _duration을 전달

        yield return new WaitForSeconds(_duration);  // _duration 동안 대기
    }
}