using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LineContainer
{
    public GameObject gameObject;
    private List<RectMask2D> _rectMasks = new List<RectMask2D>(); // 여러 개의 텍스트를 관리하는 리스트
    private List<TextMeshProUGUI> textMeshes = new List<TextMeshProUGUI>(); // 여러 개의 텍스트를 관리하는 리스트
    private List<bool> isPunctuationList = new List<bool>(); // 각 TextMeshProUGUI가 구두점에 의해 나뉘어진 것인지 여부
    private int currentTextIndex = 0; // 현재 보여줄 텍스트 인덱스

    private bool _isCompleted = false;

    public void SetCompleted(){
        _isCompleted = true;
    }

    // 생성자: LineContainer의 생성 및 설정
    public LineContainer(Transform parent, string lineText, TextMeshProUGUI mainText, TMP_LineInfo lineInfo, int index)
    {
        // 텍스트 컨테이너 동적 생성
        gameObject = new GameObject("TextContainer");
        gameObject.transform.SetParent(parent, false); // 부모 설정
        RectTransform containerRectTransform = gameObject.AddComponent<RectTransform>();

        // SplitTextWithPunctuation 함수를 호출하여 텍스트를 구두점과 함께 분리
        List<string> splitTexts = SplitTextWithPunctuation(lineText);

        // 텍스트와 구두점을 처리하는 함수 호출
        ProcessTextChunks(splitTexts, mainText, containerRectTransform, lineInfo, index);
    }

    // 텍스트 덩어리들을 처리하고 RectMask2D와 TextMeshProUGUI를 생성하는 함수
    private void ProcessTextChunks(List<string> splitTexts, TextMeshProUGUI mainText, RectTransform containerRectTransform, TMP_LineInfo lineInfo, int index)
    {
        float currentXPosition = 0; // 텍스트를 가로로 배치하기 위한 X 위치
        _rectMasks.Clear();
        textMeshes.Clear();
        foreach (var text in splitTexts)
        {
            // RectMask2D를 가지는 부모 객체 생성
            GameObject maskObject = new GameObject("RectMask");
            maskObject.transform.SetParent(gameObject.transform, false); // 부모 설정
            RectMask2D rectMask = maskObject.AddComponent<RectMask2D>(); // RectMask2D 추가
            RectTransform maskRectTransform = maskObject.GetComponent<RectTransform>();

            // TextMeshProUGUI 자식으로 추가 및 설정
            GameObject textObject = new GameObject("LineText");
            textObject.transform.SetParent(maskObject.transform, false); // RectMask2D의 자식 설정
            TextMeshProUGUI textMesh = textObject.AddComponent<TextMeshProUGUI>();
            textMesh.text = text; // 원문 텍스트 추가
            textMesh.fontSize = mainText.fontSize;
            textMesh.font = mainText.font;
            textMesh.alignment = TextAlignmentOptions.TopLeft;
            textMesh.color = mainText.color;
            textMesh.enableWordWrapping = false; // 자동 줄바꿈 비활성화

            // 텍스트의 크기를 계산하여 설정
            RectTransform textRectTransform = textMesh.GetComponent<RectTransform>();
            textRectTransform.sizeDelta = new Vector2(textMesh.preferredWidth, textMesh.preferredHeight);
            textRectTransform.anchorMin = new Vector2(0, 1);
            textRectTransform.anchorMax = new Vector2(0, 1);
            textRectTransform.pivot = new Vector2(0, 1);
            textRectTransform.anchoredPosition = Vector2.zero; // 텍스트는 RectMask 내에서 시작

            // RectMask2D의 크기와 위치를 설정
            maskRectTransform.sizeDelta = new Vector2(textMesh.preferredWidth, textMesh.preferredHeight);
            maskRectTransform.anchorMin = new Vector2(0, 1);
            maskRectTransform.anchorMax = new Vector2(0, 1);
            maskRectTransform.pivot = new Vector2(0, 1);
            maskRectTransform.anchoredPosition = new Vector2(currentXPosition, 0);

            // RectMask2D의 right padding을 텍스트의 너비로 초기화
            rectMask.padding = new Vector4(0, 0, textMesh.preferredWidth, 0);

            // 구두점 여부를 리스트에 추가
            isPunctuationList.Add(PunctuationUtils.IsCustomPunctuation(text));

            // 다음 텍스트의 위치 계산
            currentXPosition += textMesh.preferredWidth;

            // 리스트에 텍스트 추가
            _rectMasks.Add(rectMask);
            textMeshes.Add(textMesh);
        }

        // 컨테이너 크기 설정
        containerRectTransform.sizeDelta = new Vector2(currentXPosition, textMeshes[0].preferredHeight);
        containerRectTransform.anchorMin = new Vector2(0, 1);
        containerRectTransform.anchorMax = new Vector2(0, 1);
        containerRectTransform.pivot = new Vector2(0, 1);
        containerRectTransform.anchoredPosition = new Vector2(lineInfo.marginLeft, -lineInfo.lineHeight * index);
    }
    private List<string> SplitTextWithPunctuation(string text)
    {
        List<string> result = new List<string>();
        string currentChunk = string.Empty;
        int i = 0;

        while (i < text.Length)
        {
            string longestMatch = string.Empty;

            // PrioritizedPunctuations 배열에 따라 탐지
            foreach (string punctuation in PunctuationUtils.PrioritizedPunctuations)
            {
                if (i + punctuation.Length <= text.Length && text.Substring(i, punctuation.Length) == punctuation)
                {
                    longestMatch = punctuation;
                    break; // 가장 우선 순위가 높은 구두점을 찾으면 종료
                }
            }

            // 가장 긴 구두점을 찾은 경우
            if (!string.IsNullOrEmpty(longestMatch))
            {
                // 현재 텍스트 덩어리가 있다면 추가
                if (!string.IsNullOrEmpty(currentChunk))
                {
                    result.Add(currentChunk.TrimEnd());
                    currentChunk = string.Empty;
                }

                result.Add(longestMatch); // 구두점 추가
                i += longestMatch.Length; // 구두점 길이만큼 인덱스 이동
            }
            else
            {
                // 구두점이 아닌 경우 덩어리에 추가
                currentChunk += text[i];
                i++;
            }
        }

        // 마지막 덩어리가 남아있다면 추가
        if (!string.IsNullOrEmpty(currentChunk))
        {
            result.Add(currentChunk);
        }

        return result;
    }

    public IEnumerator RevealNextText(float speed, IEnumerator waitForInputCoroutine = null, System.Action onLineStarted = null, System.Action onLineComplete = null)
    {
        while (currentTextIndex < textMeshes.Count)
        {
            onLineStarted?.Invoke();  // 라인 시작 콜백 호출

            yield return RevealOneText(_rectMasks[currentTextIndex], textMeshes[currentTextIndex], speed);

            bool isTextEnd = isPunctuationList[currentTextIndex];
            if (isTextEnd)
            {   
                onLineComplete?.Invoke();  // 라인 완료 콜백 호출
                if (waitForInputCoroutine != null)
                {
                    yield return waitForInputCoroutine;
                }
            }
            currentTextIndex++;
        }
    }


    // 개별 텍스트를 드러나게 하는 코루틴
    private IEnumerator RevealOneText(RectMask2D rectMask, TextMeshProUGUI textMesh, float speed)
    {
        float initialPadding = rectMask.padding.z; // 초기 패딩 값 (텍스트의 너비)
        if (speed <= 0) // revealSpeed가 0일 때 즉시 드러나도록 설정
        {
            rectMask.padding = new Vector4(0, 0, 0, 0);
            yield break;
        }

        float timeToReveal = initialPadding / speed; // 텍스트가 완전히 드러나는 데 걸리는 총 시간
        float elapsedTime = 0f;

        while (elapsedTime < timeToReveal && !_isCompleted)
        {
            elapsedTime += Time.deltaTime; // 시간 흐름에 영향을 받지 않는 DeltaTime 사용
            float progress = Mathf.Clamp01(elapsedTime / timeToReveal);
            rectMask.padding = new Vector4(0, 0, initialPadding * (1 - progress), 0);
            yield return null;
        }

        // 최종적으로 완전히 드러난 상태로 설정
        rectMask.padding = new Vector4(0, 0, 0, 0);
    }

}
