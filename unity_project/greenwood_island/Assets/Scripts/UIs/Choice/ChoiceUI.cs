using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;
using System.Linq;

public class ChoiceUI : MonoBehaviour
{
    [SerializeField] private Transform _choicesContainer; // 선택지 컨테이너
    [SerializeField] private ChoiceButton _choicePrefab; // 선택지 버튼 프리팹
    private float ButtonHeight => _choicePrefab.GetComponent<RectTransform>().rect.height; // 각 버튼의 높이

    private int _selectedChoice = -1;
    private System.Action<int> _onChoiceSelectedCallback;

    private void Start()
    {
        // 초기화 또는 필요시 다른 설정
    }

    public IEnumerator DisplayChoices(string question, List<string> choices, System.Action<int> onChoiceSelected)
    {
        _onChoiceSelectedCallback = onChoiceSelected;

        // 기존 선택지 UI 제거
        foreach (Transform child in _choicesContainer)
        {
            Destroy(child.gameObject);
        }

        // 컨테이너 높이 조정
        AdjustContainerHeight(choices.Count);

        // 선택지 생성 및 배치
        for (int i = 0; i < choices.Count; i++)
        {
            var choiceButton = Instantiate(_choicePrefab, _choicesContainer);
            choiceButton.SetText(choices[i]);
            choiceButton.FadeIn(0.5f);

            // 균등하게 배치하기 위해 위치 설정
            float targetHeight = 100 * choices.Count - 100;
            float perone = (choices.Count == 1) ? 0 : (float)i / (choices.Count - 1);
            float yPosition = Mathf.Lerp(-targetHeight,
                                         targetHeight,
                                         perone);
            choiceButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yPosition);

            int index = i;
            choiceButton.SetOnClickListener(() => OnChoiceSelected(index, choiceButton.gameObject));
        }

        _selectedChoice = -1;

        // 플레이어가 선택할 때까지 대기
        yield return new WaitUntil(() => _selectedChoice != -1);

        // 선택 후 애니메이션 완료까지 대기
        yield return new WaitForSeconds(1.5f);
    }

    private void AdjustContainerHeight(int choiceCount)
    {
        // 컨테이너의 높이를 선택지 개수에 맞게 조정
        RectTransform rectTransform = _choicesContainer.GetComponent<RectTransform>();
        float totalHeight = choiceCount * 150;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, totalHeight);
    }

    private void OnChoiceSelected(int choiceIndex, GameObject selectedChoice)
    {
        _selectedChoice = choiceIndex;
        _onChoiceSelectedCallback?.Invoke(choiceIndex);

        // 선택한 프리팹 외의 다른 선택지 제거
        foreach (Transform child in _choicesContainer)
        {
            if (child.gameObject != selectedChoice)
            {
                var choiceButton = child.GetComponent<ChoiceButton>();
                choiceButton.FadeOut(0.5f, 0f, () => Destroy(child.gameObject));
            }
        }

        // 선택한 프리팹은 1초 동안 유지 후 제거
        var selectedChoiceButton = selectedChoice.GetComponent<ChoiceButton>();
        selectedChoiceButton.FadeOut(0.5f, 1f, () => Destroy(selectedChoice));
    }
}
