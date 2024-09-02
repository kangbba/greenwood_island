using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    private Camera _mainCamera;
    private Tween _currentTween; // 현재 실행 중인 트윈을 추적

    // 초기 카메라 상태를 저장하는 변수들
    private float _initialFOV;
    private Vector3 _initialPosition;

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
        }

        _mainCamera = Camera.main;
        if (_mainCamera != null)
        {
            // 카메라의 초기 상태 저장
            _initialFOV = _mainCamera.fieldOfView;
            _initialPosition = _mainCamera.transform.position;
        }
    }

    /// <summary>
    /// 카메라의 줌 효과를 적용하는 메서드.
    /// </summary>
    public void ZoomTo(float targetFOV, float duration, Ease easeType = Ease.Linear)
    {
        if (_mainCamera == null)
        {
            Debug.LogWarning("Main camera is not assigned.");
            return;
        }

        // 현재 실행 중인 트윈을 중지하고 새로운 줌 애니메이션을 시작
        _currentTween?.Kill();
        _currentTween = _mainCamera.DOFieldOfView(targetFOV, duration).SetEase(easeType);
    }

    /// <summary>
    /// 카메라의 위치를 이동시키는 메서드.
    /// </summary>
    public void MoveTo(Vector3 targetPosition, float duration, Ease easeType = Ease.Linear)
    {
        if (_mainCamera == null)
        {
            Debug.LogWarning("Main camera is not assigned.");
            return;
        }

        // 현재 실행 중인 트윈을 중지하고 새로운 이동 애니메이션을 시작
        _currentTween?.Kill();
        _currentTween = _mainCamera.transform.DOMove(targetPosition, duration).SetEase(easeType);
    }

    /// <summary>
    /// 카메라의 흔들림 효과를 적용하는 메서드.
    /// </summary>
    public void Shake(float duration, float strength = 3f, int vibrato = 10, float randomness = 90f)
    {
        if (_mainCamera == null)
        {
            Debug.LogWarning("Main camera is not assigned.");
            return;
        }

        // 현재 실행 중인 트윈을 중지하고 새로운 흔들림 애니메이션을 시작
        _currentTween?.Kill();
        _currentTween = _mainCamera.DOShakePosition(duration, strength, vibrato, randomness);
    }

    /// <summary>
    /// 카메라의 FOV를 초기값으로 복원하는 메서드.
    /// </summary>
    public void ZoomRestore(float duration, Ease easeType = Ease.Linear)
    {
        if (_mainCamera == null)
        {
            Debug.LogWarning("Main camera is not assigned.");
            return;
        }

        // 현재 실행 중인 트윈을 중지하고 초기 FOV로 복원하는 애니메이션을 시작
        _currentTween?.Kill();
        _currentTween = _mainCamera.DOFieldOfView(_initialFOV, duration).SetEase(easeType);
    }

    /// <summary>
    /// 카메라의 위치를 초기값으로 복원하는 메서드.
    /// </summary>
    public void MoveRestore(float duration, Ease easeType = Ease.Linear)
    {
        if (_mainCamera == null)
        {
            Debug.LogWarning("Main camera is not assigned.");
            return;
        }

        // 현재 실행 중인 트윈을 중지하고 초기 위치로 복원하는 애니메이션을 시작
        _currentTween?.Kill();
        _currentTween = _mainCamera.transform.DOMove(_initialPosition, duration).SetEase(easeType);
    }
}
