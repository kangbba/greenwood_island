using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VignetteEnter : Element
{
    private string _vignetteID = "Vignette"; // Vignette의 ID
    private Color _targetColor; // 목표 컬러
    private float _duration; // 페이드 지속 시간
    private Ease _easeType;

    // _targetColor의 기본값을 Color.black으로 설정
    public VignetteEnter(Color targetColor, float duration, Ease easeType = Ease.OutQuad)
    {
        _targetColor = targetColor == default ? Color.black : targetColor; // 기본값 설정
        _duration = duration;
        _easeType = easeType;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Debug.Log("VignetteEnter: Instant execution started.");
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        Debug.Log($"VignetteEnter: Starting Vignette effect with target color {_targetColor}, duration {_duration}, and ease {_easeType}.");

        // Vignette 생성
        GameObject vignetteInstance = FXManager.Instance.SpawnFX(_vignetteID, Vector3.zero);

        if (vignetteInstance == null)
        {
            Debug.LogError("VignetteEnter: Failed to create Vignette instance.");
            yield break; // Early return if vignette creation fails
        }

        Debug.Log("VignetteEnter: Vignette instance successfully created.");

        // Vignette 이미지의 Image를 가져옴
        Image vignetteImage = vignetteInstance.GetComponent<Image>();

        if (vignetteImage == null)
        {
            Debug.LogError("VignetteEnter: Image component not found on vignette instance.");
            yield break; // Early return if no Image component is found
        }

        Debug.Log("VignetteEnter: Vignette image component found, applying fade effect.");

        // 투명하게 설정한 후 타겟 컬러로 페이드 (DOColor 사용)
        vignetteImage.color = Color.clear; // 초기 색상은 투명
        vignetteImage.DOColor(_targetColor, _duration).SetEase(_easeType); // 목표 색상으로 페이드

        // 지정된 _duration만큼 대기
        yield return new WaitForSeconds(_duration);

        Debug.Log("VignetteEnter: Fade effect completed.");
    }
}
