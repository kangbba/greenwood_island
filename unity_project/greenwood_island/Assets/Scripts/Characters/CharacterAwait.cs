using System.Collections;
using UnityEngine;

public class CharacterAwait : Element
{
    private CharacterEnter _characterEnter;
    private bool _isClicked;
    private float _detectionRadius = 100f; // 캐릭터 주변 감지할 반경

    // CharacterEnter를 파라미터로 받는 생성자
    public CharacterAwait(CharacterEnter characterEnter)
    {
        _characterEnter = characterEnter;
        _isClicked = false;
    }

    public override void ExecuteInstantly()
    {
        _isClicked = true;
        Execute();
    }

    public override IEnumerator ExecuteRoutine()
    {
        yield return _characterEnter.ExecuteRoutine();

        var activeCharacter = CharacterManager.Instance.GetActiveCharacter(_characterEnter.CharacterID);

        if (activeCharacter == null)
        {
            Debug.LogError($"Character with ID {_characterEnter.CharacterID} not found.");
            yield break;
        }
        UICursor uiCursor = UIManager.CursorCanvas.UiCursor;
        // 캐릭터가 클릭될 때까지 대기
        while (!_isClicked)
        {
            Vector2 mousePosition = Input.mousePosition;

            // 캐릭터와 마우스 간의 거리 계산
            float distance = Vector2.Distance(mousePosition, activeCharacter.transform.position.ModifiedY(0));
            Debug.Log(mousePosition +"/" + activeCharacter.transform.position);
            // 캐릭터와의 거리가 detectionRadius 안에 있으면 Talk 모드로 전환
            if (distance <= _detectionRadius)
            {
                uiCursor.SetCursorMode(UICursor.CursorMode.Talk);
            }
            else
            {
                uiCursor.SetCursorMode(UICursor.CursorMode.Normal);
            }

            // 좌클릭을 감지
            if (Input.GetMouseButtonDown(0) && distance <= _detectionRadius)
            {
                Debug.Log("Character clicked within radius!");
                activeCharacter.Jump(100f, 0.5f); // 점프 호출
                _isClicked = true;
                uiCursor.SetCursorMode(UICursor.CursorMode.Normal); // 클릭 후 Normal 모드로 전환
            }

            yield return null;
        }
    }
}
