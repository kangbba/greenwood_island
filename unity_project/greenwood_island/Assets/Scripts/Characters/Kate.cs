using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kate : Character
{
    private int clickCount = 0;           // 클릭 횟수 추적
    private bool isVisible = false;       // 캐릭터가 현재 화면에 보이는지 여부

    private void Update()
    {
        // 스페이스바 입력 감지
        if (Input.GetKeyDown(KeyCode.Space))
        {
            clickCount++;
            Debug.Log("클릭 카운트: " + clickCount);

            if (clickCount == 1)
            {
                // 첫 클릭 시 캐릭터 등장
                ShowCharacter(true, 1f);
                isVisible = true;
                UIManager.Instance.DialoguePanel.ShowPanel(true, 1f);
                string[] dialogues = new string[] { "안녕", "난 케이트야", "왜 그러는 거야?" };
                UIManager.Instance.DialoguePanel.SetTexts(dialogues);
            }
            else if (clickCount > 1 && clickCount < 10)
            {
                // 2번째부터 9번째 클릭까지는 랜덤 이모션 실행
                int randomEmotionIndex = Random.Range(0, EmotionPlans.Count);
                string randomEmotionKey = EmotionPlans[randomEmotionIndex]._emotionKey;
                int randomSpriteIndex = Random.Range(0, EmotionPlans[randomEmotionIndex]._emotionSprites.Length);

                // 랜덤한 감정과 인덱스로 스프라이트 변경
                ChangeEmotion(randomEmotionKey, randomSpriteIndex);

                // 50% 확률로 JumpEffect 실행
                if (Random.value > 0.66f)
                {
                    JumpEffect();
                }
                else if (Random.value > 0.3f){
                    ShakeEffect(0.5f, 20f); // 지속 시간 0.5초, 강도 20f
                }
                else{
                    UIManager.Instance.DialoguePanel.ShowNext();
                }
            }
            else if (clickCount == 10)
            {
                // 10번째 클릭 시 캐릭터 퇴장
                ShowCharacter(false, 1f);
                isVisible = false;

                // 루프를 위해 클릭 카운트 초기화
                clickCount = 0;
            }
        }
    }
}
