using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Emotion : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;  // 감정 페이드 처리를 위한 CanvasGroup
    [SerializeField] private Image _openedEyesImg;      // 눈을 뜰 때 사용할 오버레이 이미지
    [SerializeField] private Image[] _mouthImages;      // 말할 때 입 모양 이미지 배열
    [SerializeField] private float _blinkInterval = 3f; // 눈 깜박임 간격
    [SerializeField] private float _talkInterval = 0.2f; // 말하는 간격

    private bool _isActivated = false;
    private bool _isBlinking = false;
    private bool _isTalking = false;

    private bool _isBlinkRoutinePlaying = false;
    private bool _isTalkingRoutinePlaying = false;

    private void Update()
    {
        if(!_isActivated){
            return;
        }
        if (_isBlinking && !_isBlinkRoutinePlaying)
        {
            StartCoroutine(BlinkRoutine());
        }
        if (_isTalking && !_isTalkingRoutinePlaying)
        {
            StartCoroutine(TalkingRoutine());
        }
    }

    // 눈 깜박임 시작/중지 함수
    public void StartBlink(bool b)
    {
        Debug.Log($"StartBlink {b}");
        _isBlinking = b;
    }

    // 말하기 시작/중지 함수
    public void StartTalking(bool b)
    {
        Debug.Log($"StartTalking {b}");
        _isTalking = b;
    }

    // 눈 깜박임 코루틴
    private IEnumerator BlinkRoutine()
    {
        _isBlinkRoutinePlaying = true;
        while (_isBlinking && _isActivated)
        {
            SetOpenEyes(true);
            yield return new WaitForSeconds(_blinkInterval);
            SetOpenEyes(false);
            yield return new WaitForSeconds(0.1f);
        }
        _isBlinkRoutinePlaying = false;
        SetOpenEyes(false);
    }

    // 말하기 코루틴
    private IEnumerator TalkingRoutine()
    {
        Debug.Log("TalkingRoutine");
        _isTalkingRoutinePlaying = true;
        int mouthIndex = 0;
        while (_isTalking && _isActivated)
        {
            SetMouthImage(-1);  // 입을 닫음
            yield return new WaitForSeconds(_talkInterval * 0.5f);  // 잠시 입을 닫음

            // 입 모양 전환
            mouthIndex = (mouthIndex + 1) % _mouthImages.Length;
            SetMouthImage(mouthIndex);  // 입 모양 이미지를 활성화
            yield return new WaitForSeconds(_talkInterval);
        }
        _isTalkingRoutinePlaying = false;
        // 코루틴이 종료될 때 입을 닫은 상태로 원상복구
        SetMouthImage(-1);
    }

    // 특정 인덱스의 입 모양 이미지를 활성화하는 함수 (-1이면 모두 비활성화)
    private void SetMouthImage(int index)
    {
        for (int i = 0; i < _mouthImages.Length; i++)
        {
            _mouthImages[i].gameObject.SetActive(i == index); // 인덱스가 일치하면 활성화, 아니면 비활성화
        }
    }

    // 눈 뜨기/감기
    private void SetOpenEyes(bool b)
    {
        _openedEyesImg.gameObject.SetActive(b);  
    }

    // 감정 활성화/비활성화 함수
    public void Activate(bool b, float duration)
    {
        _isActivated = b;
        if (b)
        {
            StartBlink(true);  // 눈 깜박임 시작
            // 활성화될 때 페이드 인
            _canvasGroup.DOFade(1f, duration).SetEase(Ease.OutQuad).OnStart(() =>
            {
            });
        }
        else
        {
            StartBlink(false);  // 눈 깜박임 중지
            StartTalking(false);  // 말하기 중지
            // 비활성화될 때 페이드 아웃
            _canvasGroup.DOFade(0f, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
            });
        }
    }
}
