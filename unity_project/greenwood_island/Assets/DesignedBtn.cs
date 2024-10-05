using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class DesignedBtn : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField] protected AudioSource _audioSource;         // 사운드를 재생할 AudioSource
    [SerializeField] protected AudioClip _hoverSound;            // Hover 사운드
    [SerializeField] protected AudioClip _pressedSound;          // Pressed 사운드

    protected Button _button;

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
    }

    // 마우스가 버튼 위에 올라갈 때 실행
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSound();
    }

    // 버튼을 누를 때 실행
    public void OnPointerDown(PointerEventData eventData)
    {
        PlayPressedSound();
    }

    protected void PlayHoverSound()
    {
        if (_audioSource != null && _hoverSound != null)
        {
            _audioSource.PlayOneShot(_hoverSound);
        }
    }

    protected void PlayPressedSound()
    {
        if (_audioSource != null && _pressedSound != null)
        {
            _audioSource.PlayOneShot(_pressedSound);
        }
    }
}
