using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChoiceSet : Element
{
    private string _question; // 질문 내용
    private List<ChoiceContent> _choiceContents; // 선택지 목록

    // 생성자에서 StorySavedData와 ChoiceSetID를 할당
    public ChoiceSet(string question, List<ChoiceContent> choices)
    {
        _question = question;
        _choiceContents = choices ?? new List<ChoiceContent>(); // null 방지를 위해 리스트 초기화
    }

    public override void ExecuteInstantly()
    {
        // // StorySavedData나 저장된 선택 결과가 없는 경우 바로 리턴
        // if (_storySavedData == null || !_storySavedData.SavedChoiceResult.ContainsKey(_choiceSetID))
        // {
        //     Debug.LogWarning($"ChoiceSet :: 저장된 선택 결과가 없습니다. choiceSetID: {_choiceSetID}");
        //     return;
        // }

        // int savedChoiceIndex = _storySavedData.SavedChoiceResult[_choiceSetID];

        // // 유효하지 않은 인덱스인 경우 바로 리턴
        // if (savedChoiceIndex < 0 || savedChoiceIndex >= _choiceContents.Count)
        // {
        //     Debug.LogWarning($"ChoiceSet :: 유효하지 않은 저장된 선택지 인덱스: {savedChoiceIndex}");
        //     return;
        // }

        // // 저장된 선택에 해당하는 SequentialElement를 즉시 실행
        // _choiceContents[savedChoiceIndex].SequentialElement.ExecuteInstantly();
        // new DialoguePanelClear().ExecuteInstantly();
    }


    public override IEnumerator ExecuteRoutine()
    {
        // 질문 출력
        yield return CoroutineUtils.StartCoroutine(new Dialogue("Mono", new Line(_question, playSpeedMutliplier: 2f), true).ExecuteRoutine());

        ChoiceUI choiceUI = UIManager.SystemCanvas.InstantiateChoiceUI();
        choiceUI.Init(_choiceContents);

        // 선택지 UI를 표시하고 플레이어의 선택을 기다림
        yield return choiceUI.WaitUntilUserChoice();
        
        int selectedChoiceIndex = choiceUI.SelectedChoiceIndex;
        // 선택된 인덱스를 로그로 출력
        Debug.Log($"선택된 인덱스: {selectedChoiceIndex}");

        yield return new WaitForSeconds(.5f);



        // 유효한 선택인지 확인하고, 선택한 결과에 따른 동작 실행
        if (selectedChoiceIndex >= 0 && selectedChoiceIndex < _choiceContents.Count)
        {
            // 선택한 SequentialElement 실행
            yield return CoroutineUtils.StartCoroutine(_choiceContents[selectedChoiceIndex].SequentialElement.ExecuteRoutine());
        }
        else
        {
            Debug.LogWarning("유효하지 않은 선택지 선택");
            // 유효하지 않은 선택 시 대체 동작이나 기본 행동 추가 가능
        }

        choiceUI.FadeOutAndDestroy(0f);
    }
}
