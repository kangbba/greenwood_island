using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChoiceSet : Element
{
    private string _question; // 질문 내용
    private List<ChoiceContent> _choiceContents; // 선택지 목록

    public ChoiceSet(string question, List<ChoiceContent> choices)
    {
        _question = question;
        _choiceContents = choices ?? new List<ChoiceContent>(); // null 방지를 위해 리스트 초기화
    }

    public override IEnumerator ExecuteRoutine()
    {
        yield return new DialoguePanelClear().ExecuteRoutine();
        ChoiceUI choiceUI = UIManager.Instance.SystemCanvas.ChoiceUI;
        if(choiceUI == null){
            Debug.LogWarning("ChoiceSet :: choice ui prefab is null");
            yield break;
        }
        choiceUI.gameObject.SetActive(true);

        int chosenIndex = -1;

        // 선택지 UI를 표시하고 플레이어의 선택을 기다림
        yield return CoroutineUtils.StartCoroutine(
            choiceUI.DisplayChoices(
                _question,
                _choiceContents.Select(choice => choice.Title).ToList(),
                index => chosenIndex = index // 콜백으로 선택된 인덱스를 설정
            )
        );

        // 선택된 인덱스를 로그로 출력
        Debug.Log($"선택된 인덱스: {chosenIndex}");

        // 유효한 선택인지 확인하고, 선택한 결과에 따른 동작 실행
        if (chosenIndex >= 0 && chosenIndex < _choiceContents.Count)
        {
            // 선택된 Option의 콜백 실행
            _choiceContents[chosenIndex].OnSelected?.Invoke();

            // 선택한 SequentialElement 실행
            yield return CoroutineUtils.StartCoroutine(_choiceContents[chosenIndex].ExecuteRoutine());
        }
        else
        {
            Debug.LogWarning("유효하지 않은 선택지 선택");
            // 유효하지 않은 선택 시 대체 동작이나 기본 행동 추가 가능
        }
        choiceUI.gameObject.SetActive(false);
    }

}
