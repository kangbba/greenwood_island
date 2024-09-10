using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextDisplayer : MonoBehaviour
{
    // 텍스트 드러내는 스타일을 정의하는 enum
    public enum RevealStyle
    {
        Continuously,
        WithMouseClick
    }

    private TextMeshProUGUI mainText; // 최상위 TextMeshProUGUI

    private List<LineContainer> lineContainers = new List<LineContainer>();

    private void Awake()
    {
        // mainText를 자동으로 바인딩
        mainText = GetComponent<TextMeshProUGUI>();
    }

    public void InitText(string text){
        StopAllCoroutines();
        mainText.text = text; // 메인 텍스트에 먼저 텍스트를 설정하여 줄 정보를 가져옴
         // 기존 컨테이너 제거
        foreach (var container in lineContainers)
        {
            Destroy(container.gameObject);
        }
        lineContainers.Clear();

        // TMP 라인 정보 가져오기
        mainText.ForceMeshUpdate(); // 텍스트 레이아웃 강제 업데이트
        TMP_TextInfo textInfo = mainText.textInfo;

        // 각 줄의 컨테이너 생성 및 드러나는 데 걸리는 속도 설정
        for (int i = 0; i < textInfo.lineCount; i++)
        {
            TMP_LineInfo lineInfo = textInfo.lineInfo[i];

            // 각 줄에 해당하는 텍스트 생성 및 설정
            string lineText = mainText.text.Substring(lineInfo.firstCharacterIndex, lineInfo.characterCount);

            // LineContainer 생성 및 설정
            LineContainer lineContainer = new LineContainer(transform, lineText, mainText, lineInfo, i);
            lineContainers.Add(lineContainer);
        }

        // 메인 텍스트 비우기
        mainText.text = string.Empty;
    }
    // 텍스트를 표시하는 함수
    public IEnumerator ShowTextRoutine(float revealSpeed, RevealStyle revealStyle)
    {
        foreach (var container in lineContainers)
        {
            yield return container.RevealNextText(revealSpeed, revealStyle); // 연속적으로 텍스트를 표시
        }
    }
}
