using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ChoiceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private Transform _choicesContainer;
    [SerializeField] private ChoiceButton _choicePrefab;  // ChoiceButton 스크립트가 할당된 프리팹

    private int _selectedChoice = -1;
    private System.Action<int> _onChoiceSelectedCallback;

    private void Start()
    {
        _questionText.alpha = 0f; // 시작 시 질문 텍스트를 안 보이게 설정
    }

    public IEnumerator DisplayChoices(string question, List<string> choices, System.Action<int> onChoiceSelected)
    {
        _questionText.text = question;
        _questionText.DOFade(1f, 0.5f); // 질문 텍스트 페이드 인
        _onChoiceSelectedCallback = onChoiceSelected;

        // 기존 선택지 UI 제거
        foreach (Transform child in _choicesContainer)
        {
            Destroy(child.gameObject);
        }

        // 선택지 생성 및 순차적 배치
        float startY = 200f;
        float yOffset = -200f;

        for (int i = 0; i < choices.Count; i++)
        {
            var choiceButton = Instantiate(_choicePrefab, _choicesContainer);
            choiceButton.SetText(choices[i]);

            // 세로축 localPosition을 순차적으로 설정
            float yPosition = startY + (i * yOffset);
            choiceButton.GetComponent<RectTransform>().localPosition = new Vector3(0, yPosition, 0);

            // 생성 시 페이드 인 애니메이션
            choiceButton.FadeIn(0.5f);

            int index = i;
            choiceButton.SetOnClickListener(() => OnChoiceSelected(index, choiceButton.gameObject));
        }

        _selectedChoice = -1;

        // 플레이어가 선택할 때까지 대기
        yield return new WaitUntil(() => _selectedChoice != -1);

        _questionText.DOFade(0f, 0.5f); // 질문 텍스트 페이드 아웃

        // 선택 후 애니메이션 완료까지 대기
        yield return new WaitForSeconds(1.5f);
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
