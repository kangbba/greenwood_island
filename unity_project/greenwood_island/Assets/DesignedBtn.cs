using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class DesignedBtn : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    [SerializeField] protected AudioSource _audioSource;  // 사운드를 재생할 AudioSource
    [SerializeField] protected AudioClip _hoverSound;     // Hover 사운드
    [SerializeField] protected AudioClip _pressedSound;   // Pressed 사운드
    [SerializeField] protected AudioClip _releaseSound;   // 버튼에서 손을 뗄 때 사운드

    protected Button _button;

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
    }

    // 마우스가 버튼 위에 올라갈 때 실행
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSound();
    }

    // 버튼을 누를 때 실행
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        PlayPressedSound();
    }

    // 마우스가 버튼에서 벗어날 때 실행
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit();  // 마우스가 벗어날 때 처리
    }

    // 버튼에서 손을 뗄 때 실행
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        PlayReleaseSound();  // 손을 뗄 때 사운드 재생
    }

    protected virtual void PlayHoverSound()
    {
        if (_audioSource != null && _hoverSound != null)
        {
            _audioSource.PlayOneShot(_hoverSound);
        }
    }

    protected virtual void PlayPressedSound()
    {
        if (_audioSource != null && _pressedSound != null)
        {
            _audioSource.PlayOneShot(_pressedSound);
        }
    }

    protected virtual void PlayReleaseSound()
    {
        if (_audioSource != null && _releaseSound != null)
        {
            _audioSource.PlayOneShot(_releaseSound);
        }
    }

    protected virtual void OnHoverExit()
    {
        Debug.Log("마우스가 버튼에서 벗어났습니다.");
    }
}
