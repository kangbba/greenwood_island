using System.Collections;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class CameraMove2DByAngle : Element
{
    private float _degree; // 이동할 방향의 각도 (0도: 오른쪽, 90도: 위, 180도: 왼쪽, 270도: 아래)
    private float _radius; // 이동 반경
    private float _duration; // 이동 시간
    private Ease _easeType; // Ease 타입

    public CameraMove2DByAngle(float degree, float radius, float duration = 1f, Ease easeType = Ease.OutQuad)
    {
        _degree = degree;
        _radius = radius;
        _duration = duration;
        _easeType = easeType;
    }

    // 각도에 따른 TargetLocalPosition 계산
    private Vector2 CalculateTargetLocalPosition()
    {
        // 각도를 라디안으로 변환하고 방향 벡터 계산
        float radian = _degree * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;

        // 현재 PlaneLayer의 localPosition에 direction * radius를 더하여 목표 위치 계산
        Vector2 currentLocalPos = new Vector2(CameraController.PlaneLayer.localPosition.x, CameraController.PlaneLayer.localPosition.y);
        return currentLocalPos + direction * _radius;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // TargetLocalPosition을 계산하여 카메라를 이동
        Vector2 targetLocalPos = CalculateTargetLocalPosition();

        // CameraMovePlane을 이용하여 카메라를 이동시키고, 이후 추가 연출 가능
        CameraController.MovePlane(targetLocalPos, _duration, _easeType);

        // 이동 시간만큼 대기
        yield return new WaitForSeconds(_duration);
    }
}
