using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VignetteExit : Element
{
    private string _vignetteID = "Vignette"; // Vignette의 ID
    private float _duration; // 페이드 아웃 지속 시간

    private Ease _easeType;

    public VignetteExit(float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _duration = duration;
        _easeType = easeType;
    }

    public override void ExecuteInstantly()
    {
        _duration = 0;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        // FXManager에서 활성화된 Vignette를 가져옴
        GameObject vignetteInstance = FXManager.Instance.GetActiveFX(_vignetteID);

        if (vignetteInstance != null)
        {
            // Vignette의 Image를 가져와서 페이드 아웃 적용
            Image vignetteImage = vignetteInstance.GetComponent<Image>();

            if (vignetteImage != null)
            {
                // 투명 상태로 페이드 아웃
                vignetteImage.FadeImage(Color.clear, _duration, _easeType);
            }

            // 지정된 _duration 후에 FXManager를 통해 제거
            FXManager.Instance.FadeAndDestroyFX(vignetteInstance, _duration);
        }

        yield return new WaitForSeconds(_duration);
    }
}
