using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ChoiceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private Transform _choicesContainer;
    [SerializeField] private GameObject _choicePrefab;

    private int _selectedChoice = -1;
    private System.Action<int> _onChoiceSelectedCallback; // 콜백 함수

    public IEnumerator DisplayChoices(string question, List<string> choices, System.Action<int> onChoiceSelected)
    {
        _questionText.text = question;
        _onChoiceSelectedCallback = onChoiceSelected; // 콜백 설정

        // 기존 선택지 UI 제거
        foreach (Transform child in _choicesContainer)
        {
            Destroy(child.gameObject);
        }

        // 선택지 생성 및 순차적 배치
        float startY = 200f; // 시작 Y 좌표 (간격을 기존의 두 배로 설정)
        float yOffset = -200f; // 버튼 간의 간격

        for (int i = 0; i < choices.Count; i++)
        {
            var choiceObject = Instantiate(_choicePrefab, _choicesContainer);
            var choiceText = choiceObject.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = choices[i];

            // 세로축 localPosition을 순차적으로 설정
            float yPosition = startY + (i * yOffset);
            choiceObject.GetComponent<RectTransform>().localPosition = new Vector3(0, yPosition, 0);

            // 생성 시 페이드 인 애니메이션
            choiceObject.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).From(0f);

            int index = i; // 로컬 변수로 캡처하여 람다에서 사용
            choiceObject.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(index, choiceObject));
        }

        _selectedChoice = -1; // 선택 초기화

        // 플레이어가 선택할 때까지 대기
        yield return new WaitUntil(() => _selectedChoice != -1);
    }

    private void OnChoiceSelected(int choiceIndex, GameObject selectedChoice)
    {
        _selectedChoice = choiceIndex; // 선택된 인덱스를 설정하여 대기 중인 코루틴을 진행시킴
        _onChoiceSelectedCallback?.Invoke(choiceIndex); // 콜백 호출

        // 선택한 프리팹 외의 다른 선택지 제거
        foreach (Transform child in _choicesContainer)
        {
            if (child.gameObject != selectedChoice)
            {
                child.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(() => Destroy(child.gameObject));
            }
        }

        // 선택한 프리팹은 1초 동안 유지 후 제거
        selectedChoice.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetDelay(1f).OnComplete(() => Destroy(selectedChoice));
    }
}
