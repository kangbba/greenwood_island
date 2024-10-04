using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ChoiceWithCorrectAnswer 클래스는 플레이어에게 선택지를 제공하며,
/// 정답이 선택될 때까지 선택지를 반복하여 표시합니다.
/// 정답을 선택하면 해당 ChoiceOption의 동작을 실행하고 종료하며,
/// 오답을 선택하면 ChoiceOption의 동작을 실행한 후 다시 선택지를 표시합니다.
/// </summary>
public class ChoiceSetWithCorrectAnswer : Element
{
    private string _question; // 질문 내용
    private List<ChoiceContent> _choiceContents; // 선택지 목록
    private int _correctAnswerIndex; // 정답 인덱스

    public ChoiceSetWithCorrectAnswer(string question, int correctAnswerIndex, List<ChoiceContent> choices)
    {
        _question = question;
        _correctAnswerIndex = correctAnswerIndex;
        _choiceContents = choices ?? new List<ChoiceContent>(); // null 방지를 위해 리스트 초기화
    }

    public override IEnumerator ExecuteRoutine()
    {
        Debug.Log("ChoiceSetWithCorrectAnswer :: 시작");

        ChoiceUI choiceUI = UIManager.SystemCanvas.ChoiceUI;
        if(choiceUI == null){
            Debug.LogWarning("ChoiceSet :: choice ui prefab is null");
            yield break;
        }
        choiceUI.gameObject.SetActive(true);

        while (true)
        {
            int chosenIndex = -1;

            // 선택지 UI를 표시하고 플레이어의 선택을 기다림
            yield return CoroutineUtils.StartCoroutine(
                choiceUI.DisplayChoices(
                    _question,
                    _choiceContents.Select(choice => choice.Title).ToList(),
                    index => chosenIndex = index // 콜백으로 선택된 인덱스를 설정
                )
            );

            // 유효한 선택인지 확인
            if (chosenIndex < 0 || chosenIndex >= _choiceContents.Count)
            {
                Debug.LogWarning("유효하지 않은 선택지 선택");
                continue;
            }

            // 선택된 ChoiceOption의 동작 실행
            yield return CoroutineUtils.StartCoroutine(_choiceContents[chosenIndex].ExecuteRoutine());

            // 정답 확인 후 종료
            if (chosenIndex == _correctAnswerIndex)
            {
                Debug.Log("정답을 선택하셨습니다!");
                break;
            }

            // 틀린 선택 시 메시지 출력
            Debug.Log("틀린 선택입니다. 다시 시도하세요.");
        }
        Debug.Log("ChoiceSetWithCorrectAnswer ::  종료");
    }
}
