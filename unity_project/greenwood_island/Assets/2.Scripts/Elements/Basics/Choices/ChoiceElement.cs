using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChoiceElement : Element
{
    private string _question; // 질문 내용
    private List<ChoiceOption> _choices; // 선택지 목록

    public ChoiceElement(string question, List<ChoiceOption> choices)
    {
        _question = question;
        _choices = choices ?? new List<ChoiceOption>(); // null 방지를 위해 리스트 초기화
    }


    public override IEnumerator Execute()
    {
        int chosenIndex = -1;

        // 선택지 UI를 표시하고 플레이어의 선택을 기다림
        yield return CoroutineRunner.Instance.StartCoroutine(UIManager.Instance.SystemCanvas.ChoiceUI.DisplayChoices(
            _question, 
            _choices.Select(choice => choice.Title).ToList(), 
            index => chosenIndex = index // 콜백으로 선택된 인덱스를 설정
        ));

        // 선택한 결과에 따른 Elements 실행
        if (chosenIndex >= 0 && chosenIndex < _choices.Count)
        {
            yield return CoroutineRunner.Instance.StartCoroutine(_choices[chosenIndex].Execute());
        }
        else
        {
            Debug.LogWarning("유효하지 않은 선택지 선택");
        }
    }
}
