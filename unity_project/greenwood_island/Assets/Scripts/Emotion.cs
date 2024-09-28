using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class Emotion : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;  // 감정 페이드 처리를 위한 CanvasGroup
    [SerializeField] private Image _openedEyesImg;      // 눈을 뜰 때 사용할 오버레이 이미지
    [SerializeField] private Image[] _mouthImages;      // 말할 때 입 모양 이미지 배열

    private Vector2[] _mouthImgInitialScales;
    private Vector2 _blinkIntervalRange = new Vector2(2f, 5f); // 눈 깜박임 간격
    private Vector2 _talkIntervalRange = new Vector2(.05f, .25f); // 말하는 간격

    private bool _isActivated = false;
    private bool _isBlinking = false;
    private bool _isTalking = false;

    private bool _isBlinkRoutinePlaying = false;
    private bool _isTalkingRoutinePlaying = false;

    private void Start(){
        _mouthImgInitialScales = new Vector2[_mouthImages.Length];

        for (int i = 0; i < _mouthImages.Length; i++)
        {
            if (_mouthImages[i] != null)  
            {
                _mouthImgInitialScales[i] = _mouthImages[i].transform.localScale;
            }
        }
    }
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
        _isBlinking = b;
    }

    // 말하기 시작/중지 함수
    public void StartTalking(bool b)
    {
        _isTalking = b;
    }

    // 눈 깜박임 코루틴
    private IEnumerator BlinkRoutine()
    {
        _isBlinkRoutinePlaying = true;
        while (_isBlinking && _isActivated)
        {
            SetOpenEyes(true);
            yield return new WaitForSeconds(_blinkIntervalRange.RandomValue());
            SetOpenEyes(false);
            yield return new WaitForSeconds(0.1f);
        }
        _isBlinkRoutinePlaying = false;
        SetOpenEyes(false);
    }

    private IEnumerator TalkingRoutine()
    {
        Debug.Log("TalkingRoutine");
        _isTalkingRoutinePlaying = true;

        // -1 (입을 닫은 상태)와 0부터 _mouthImages.Length - 1까지의 인덱스를 리스트로 생성
        List<int> mouthIndices = new List<int> { -1 };  // 먼저 -1을 추가 (입을 닫은 상태)
        for (int i = 0; i < _mouthImages.Length; i++)
        {
            mouthIndices.Add(i);  // 입 모양 인덱스를 추가
        }

        int currentIndex = 0;  // 리스트의 현재 인덱스

        while (_isTalking && _isActivated)
        {
            // 현재 인덱스에 맞는 입 모양 설정
            SetMouthImage(mouthIndices[currentIndex], new Vector2(Random.Range(0.95f,1.05f), Random.Range(0.95f,1.05f)));

            // 다음 인덱스 계산 (리스트를 순환)
            currentIndex = (currentIndex + 1) % mouthIndices.Count;

            yield return new WaitForSeconds(_talkIntervalRange.RandomValue());
        }

        _isTalkingRoutinePlaying = false;
        SetMouthImage(-1);  // 코루틴이 종료될 때 입을 닫은 상태로 원상복구
    }



    // 특정 인덱스의 입 모양 이미지를 활성화하는 함수 (-1이면 모두 비활성화)
    private void SetMouthImage(int index, Vector2 scale = default)
    {
        for (int i = 0; i < _mouthImages.Length; i++)
        {
            _mouthImages[i].transform.localScale = _mouthImgInitialScales[i] * scale;
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
