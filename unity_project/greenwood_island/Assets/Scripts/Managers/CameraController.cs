using UnityEngine;
using DG.Tweening;

public static class CameraController
{
    public static Transform DepthLayer { get; private set; } 
    public static Transform PlaneLayer { get; private set; } 
    public static Transform ShakeLayer { get; private set; } 

    private static Vector2 _zoomRange = new Vector2(-823f, -30f); 
    public static float ZoomMaxZ => _zoomRange.y;
    public static float ZoomMinZ => _zoomRange.x;

    private static Tween _planeTween; 
    private static Tween _depthTween; 
    private static Tween _shakeTween; 

    private static bool _isInitialized = false; 
    private static GameObject _cameraHost; 
    private static MonoBehaviour _cameraHandler; 

    static CameraController()
    {
        _cameraHost = new GameObject("CameraControllerHost");
        _cameraHandler = _cameraHost.AddComponent<CameraHandler>(); 
        Init();
        Object.DontDestroyOnLoad(_cameraHost); 
    }

    public static void Init()
    {
        if (_isInitialized)
            return;

        if (Camera.main == null)
        {
            GameObject cameraObject = new GameObject("MainCamera");
            cameraObject.AddComponent<Camera>();
            Camera.main.tag = "MainCamera";
            Object.DontDestroyOnLoad(cameraObject);
        }
        else
        {
            Object.DontDestroyOnLoad(Camera.main.gameObject);
        }

        PlaneLayer = new GameObject("CameraPlaneLayer").transform;
        DepthLayer = new GameObject("CameraDepthLayer").transform;
        ShakeLayer = new GameObject("CameraShakeLayer").transform;

        DepthLayer.parent = PlaneLayer;
        ShakeLayer.parent = DepthLayer;

        Camera.main.transform.parent = ShakeLayer;
        Camera.main.transform.localPosition = Vector3.zero;

        PlaneLayer.localPosition = Vector3.zero;
        DepthLayer.localPosition = new Vector3(0, 0, ZoomMinZ);
        ShakeLayer.localPosition = Vector3.zero;

        _isInitialized = true;
    }

    public static void MovePlane(Vector2 targetLocalPos, float duration, Ease easeType = Ease.OutQuad)
    {
        _planeTween?.Kill();
        _planeTween = PlaneLayer.DOLocalMove(new Vector3(targetLocalPos.x, targetLocalPos.y, PlaneLayer.localPosition.z), duration).SetEase(easeType);
    }

    public static void ZoomLocalPosZ(float localPosZ, float duration, Ease easeType = Ease.OutQuad)
    {
        float targetZ = Mathf.Clamp(localPosZ, ZoomMinZ, ZoomMaxZ);
        _depthTween?.Kill();
        _depthTween = DepthLayer.DOLocalMoveZ(targetZ, duration).SetEase(easeType);
    }

    public static void ZoomByFactor(float zoomPerone, float duration, Ease easeType = Ease.OutQuad)
    {
        float targetZ = Mathf.Lerp(_zoomRange.x, _zoomRange.y, Mathf.Clamp01(zoomPerone));
        _depthTween?.Kill();
        _depthTween = DepthLayer.DOLocalMoveZ(targetZ, duration).SetEase(easeType);
    }

    public static void RestoreAll(float duration, Ease easeType = Ease.OutQuad)
    {
        _planeTween?.Kill();
        _depthTween?.Kill();
        _shakeTween?.Kill();

        PlaneLayer.DOLocalMove(Vector3.zero, duration).SetEase(easeType);
        DepthLayer.DOLocalMoveZ(ZoomMinZ, duration).SetEase(easeType);
        ShakeLayer.DOLocalMove(Vector3.zero, duration).SetEase(easeType);
    }

    public static void Shake(float duration, float strength = 3f, int vibrato = 10, float randomness = 90f)
    {
        _shakeTween?.Kill();
        _shakeTween = ShakeLayer.DOShakePosition(duration, strength, vibrato, randomness);
    }

    private class CameraHandler : MonoBehaviour { }
}
