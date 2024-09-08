using UnityEngine;
using DG.Tweening;

/// <summary>
/// CameraController는 카메라의 이동, 줌, 흔들림을 관리하는 클래스입니다. 
/// PlaneLayer는 X, Y 축의 평면 이동을 담당하며, DepthLayer는 Z 축의 줌 효과를 담당합니다.
/// ShakeLayer는 카메라의 흔들림 효과를 담당하며, MainCamera는 항상 (0, 0, 0)에 고정된 상태를 유지하여 직접적인 위치 변동이 없도록 합니다.
/// </summary>
public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    public Transform DepthLayer { get; private set; } // 카메라의 줌 효과를 담당하는 레이어
    public Transform PlaneLayer { get; private set; } // 카메라의 평면 이동을 담당하는 레이어
    public Transform ShakeLayer { get; private set; } // 카메라의 흔들림 효과를 담당하는 레이어

    private Vector2 _zoomRange = new Vector2(-823f, -30f); // 0은 최대 줌(멀리), 1은 최소 줌(가까이)
    public float ZoomMaxZ => _zoomRange.y;
    public float ZoomMinZ => _zoomRange.x;


    private Tween _planeTween; // PlaneLayer의 현재 실행 중인 트윈
    private Tween _depthTween; // DepthLayer의 현재 실행 중인 트윈
    private Tween _shakeTween; // ShakeLayer의 현재 실행 중인 트윈

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // PlaneLayer, DepthLayer, ShakeLayer 설정
        PlaneLayer = new GameObject("CameraPlaneLayer").transform;
        DepthLayer = new GameObject("CameraDepthLayer").transform;
        ShakeLayer = new GameObject("CameraShakeLayer").transform;

        // 레이어의 계층 구조 설정
        DepthLayer.parent = PlaneLayer;
        ShakeLayer.parent = DepthLayer;

        // MainCamera 설정 및 초기화
        Camera.main.transform.parent = ShakeLayer;
        Camera.main.transform.localPosition = Vector3.zero;

        // 시작 시 카메라 위치와 줌 초기화
        PlaneLayer.localPosition = Vector3.zero;
        DepthLayer.localPosition = new Vector3(0, 0, ZoomMinZ); // 줌의 기본 위치 설정
        ShakeLayer.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 카메라의 평면 이동을 조절합니다. Z 축 이동은 포함되지 않습니다.
    /// </summary>
    /// <param name="targetLocalPos">목표 로컬 위치 (X, Y만 사용)</param>
    /// <param name="duration">이동 시간</param>
    /// <param name="easeType">Ease 타입</param>
    public void MovePlane(Vector2 targetLocalPos, float duration, Ease easeType = Ease.OutQuad)
    {
        _planeTween?.Kill();
        _planeTween = PlaneLayer.DOLocalMove(new Vector3(targetLocalPos.x, targetLocalPos.y, PlaneLayer.localPosition.z), duration).SetEase(easeType);
    }

   /// <summary>
    /// 카메라의 줌을 조절합니다. 입력된 Z 값을 사용하여 DepthLayer의 Z 축을 설정합니다.
    /// </summary>
    /// <param name="localPosZ">목표 Z 위치 (DepthLayer의 로컬 Z 값)</param>
    /// <param name="duration">줌 시간</param>
    /// <param name="easeType">Ease 타입</param>
    public void ZoomLocalPosZ(float localPosZ, float duration, Ease easeType = Ease.OutQuad)
    {
        // 입력된 Z 값이 줌 범위 내에 있는지 확인
        float targetZ = Mathf.Clamp(localPosZ, ZoomMinZ, ZoomMaxZ);
        _depthTween?.Kill();
        _depthTween = DepthLayer.DOLocalMoveZ(targetZ, duration).SetEase(easeType);
    }

    /// <summary>
    /// 카메라의 줌을 조절합니다. 0 ~ 1 사이의 비율로 ZoomRange 내에서 Z 축을 설정합니다.
    /// </summary>
    /// <param name="zoomFactor">줌 비율 (0: 최대 줌, 1: 최소 줌)</param>
    /// <param name="duration">줌 시간</param>
    /// <param name="easeType">Ease 타입</param>
    public void ZoomByFactor(float zoomFactor, float duration, Ease easeType = Ease.OutQuad)
    {
        float targetZ = Mathf.Lerp(_zoomRange.x, _zoomRange.y, Mathf.Clamp01(zoomFactor));
        _depthTween?.Kill();
        _depthTween = DepthLayer.DOLocalMoveZ(targetZ, duration).SetEase(easeType);
    }

    /// <summary>
    /// 카메라의 위치를 초기값으로 복원합니다 (PlaneLayer, DepthLayer, ShakeLayer 모두).
    /// </summary>
    /// 
    public void RestoreAll(float duration, Ease easeType = Ease.OutQuad)
    {
        _planeTween?.Kill();
        _depthTween?.Kill();
        _shakeTween?.Kill();

        PlaneLayer.DOLocalMove(Vector3.zero, duration).SetEase(easeType);
        DepthLayer.DOLocalMoveZ(ZoomMinZ, duration).SetEase(easeType); // 기본값으로 복원 (줌 비율 0)
        ShakeLayer.DOLocalMove(Vector3.zero, duration).SetEase(easeType); // 흔들림 복원
    }

    /// <summary>
    /// 카메라의 흔들림 효과를 적용합니다.
    /// </summary>
    /// <param name="duration">흔들림 시간</param>
    /// <param name="strength">흔들림 강도</param>
    /// <param name="vibrato">흔들림의 진동 수</param>
    /// <param name="randomness">흔들림의 무작위성</param>
    public void Shake(float duration, float strength = 3f, int vibrato = 10, float randomness = 90f)
    {
        _shakeTween?.Kill();
        _shakeTween = ShakeLayer.DOShakePosition(duration, strength, vibrato, randomness);
    }

}
