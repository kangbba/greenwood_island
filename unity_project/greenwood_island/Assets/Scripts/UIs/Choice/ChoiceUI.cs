using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour
{
    [SerializeField] private Transform _choicesContainer; // 선택지 컨테이너
    [SerializeField] private ChoiceButton _choicePrefab; // 선택지 버튼 프리팹
    [SerializeField] private CanvasGroup _canvasGroup; // CanvasGroup을 직접 바인딩

    private int _selectedChoiceIndex = -1;

    private List<ChoiceContent> _choiceContents;
    private List<ChoiceButton> _choiceBtns = new List<ChoiceButton>();

    public int SelectedChoiceIndex { get => _selectedChoiceIndex; }

    public ChoiceContent SelectedChoiceContent
    {
        get
        {
            if (_selectedChoiceIndex >= 0 && _selectedChoiceIndex < _choiceContents.Count)
            {
                return _choiceContents[_selectedChoiceIndex];
            }
            return null;
        }
    }

    // Init 메서드: 선택지 목록을 받음
    public void Init(List<ChoiceContent> choiceContents)
    {
        if (choiceContents == null)
        {
            Debug.LogWarning("choice contents null");
            return;
        }
        _selectedChoiceIndex = -1;
        _choiceContents = choiceContents;

        // 기존 선택지 UI 제거
        foreach (Transform child in _choicesContainer)
        {
            Destroy(child.gameObject);
        }
        _choiceBtns.Clear();

        // 컨테이너 높이 조정
        AdjustContainerHeight(_choiceContents.Count);

        // 선택지 생성 및 배치
        int cnt = _choiceContents.Count;
        for (int i = 0; i < cnt; i++)
        {
            ChoiceContent choiceContent = choiceContents[i];
            var choiceButton = Instantiate(_choicePrefab, _choicesContainer);
            choiceButton.SetText(choiceContent.Title);
            choiceButton.FadeIn(0.5f);

            // 균등하게 배치하기 위해 위치 설정
            float targetHeight = 100 * cnt - 100;
            float perone = (cnt == 1) ? 0 : (float)i / (cnt - 1);
            float yPosition = Mathf.Lerp(-targetHeight,
                                         targetHeight,
                                         perone);
            choiceButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yPosition);

            int index = i;
            choiceButton.SetOnClickListener(() => OnChoiceSelected(index, choiceButton));
            _choiceBtns.Add(choiceButton);
        }
    }

    public IEnumerator WaitUntilUserChoice()
    {
        // 플레이어가 선택할 때까지 대기
        yield return new WaitUntil(() => _selectedChoiceIndex != -1);
    }

    private void AdjustContainerHeight(int choiceCount)
    {
        // 컨테이너의 높이를 선택지 개수에 맞게 조정
        RectTransform rectTransform = _choicesContainer.GetComponent<RectTransform>();
        float totalHeight = choiceCount * 150;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, totalHeight);
    }

    private void OnChoiceSelected(int choiceIndex, ChoiceButton selectedChoiceBtn)
    {
        _selectedChoiceIndex = choiceIndex;

        // 선택한 프리팹 외의 다른 선택지 제거
        foreach (ChoiceButton btn in _choiceBtns)
        {
            btn.Button.interactable = false;
            if (btn != selectedChoiceBtn)
            {
                btn.FadeOut(0.5f, 0f, () => Destroy(btn.gameObject));
            }
        }

        selectedChoiceBtn.Button.interactable = false;
        // 선택한 프리팹은 1초 동안 유지 후 제거
        selectedChoiceBtn.FadeOut(0.5f, 1f, () => Destroy(selectedChoiceBtn.gameObject));
    }

    // FadeOutAndDestroy 메서드 추가
    public void FadeOutAndDestroy(float duration)
    {
        // DOTween을 사용하여 페이드 아웃 후 오브젝트 파괴
        _canvasGroup.DOFade(0, duration).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
