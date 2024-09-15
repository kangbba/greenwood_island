using System.Collections;
using UnityEngine;

public class CutInClear : Element
{
    private CutInUI _cutInUI;  // 참조할 CutInUI
    private float _duration;   // 애니메이션 지속 시간

    // 생성자: 지속 시간을 받음
    public CutInClear(float duration)
    {
        _duration = duration;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // UIManager를 통해 CutInUI를 가져옴
        _cutInUI = UIManager.Instance.SystemCanvas.CutInUI;

        if (_cutInUI == null)
        {
            Debug.LogWarning("CutInUI가 존재하지 않습니다.");
            yield break;
        }

        // CutInUI 퇴장 애니메이션 실행
        _cutInUI.Show(false, _duration);

        // 애니메이션이 지속되는 동안 대기
        yield return new WaitForSeconds(_duration);

        // 퇴장 후 필요 시 UI를 초기화하거나 비활성화
        _cutInUI.gameObject.SetActive(false); // 비활성화하여 화면에서 사라지게 함
    }
}
