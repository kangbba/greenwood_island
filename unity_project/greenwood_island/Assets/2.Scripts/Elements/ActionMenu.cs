using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ActionMenu는 버튼의 제목, 하위 버튼, 그리고 SequentialElement 형태의 액션을 포함하는 클래스입니다.
/// 여러 레벨의 버튼과 액션을 재귀적으로 관리할 수 있습니다.
/// </summary>
[System.Serializable]
public class ActionMenu : Element
{
    public string Title; // 버튼 제목
    public SequentialElement onClickedAction; // 버튼 클릭 시 실행할 SequentialElement 액션 (최하위 메뉴일 때만 사용)
    public List<ActionMenu> SubMenus; // 하위 버튼 리스트 (상위 메뉴일 때만 사용)

    // 생성자 (최하위 메뉴용)
    public ActionMenu(string title, SequentialElement onClickedAction)
    {
        Title = title;
        this.onClickedAction = onClickedAction;
        SubMenus = null; // 최하위 메뉴는 하위 버튼 리스트가 없음
    }

    // 생성자 (상위 메뉴용)
    public ActionMenu(string title, List<ActionMenu> subMenus)
    {
        Title = title;
        this.onClickedAction = null; // 상위 메뉴는 클릭 액션이 없음
        SubMenus = subMenus ?? new List<ActionMenu>();
    }

    // ActionMenu 실행 루틴
    public override IEnumerator ExecuteRoutine()
    {
        // UIManager를 통해 ActionMenuUI를 초기화하고, ActionMenu를 전달하여 UI를 생성
        UIManager.Instance.SystemCanvas.ActionMenuUI.gameObject.SetActive(true);
        UIManager.Instance.SystemCanvas.ActionMenuUI.Init(this);

        // ActionMenuUI의 모든 작업이 완료될 때까지 대기
        yield return new WaitUntil(() => false);

        Debug.Log($"ActionMenu '{Title}' 작업이 완료되었습니다.");
        UIManager.Instance.SystemCanvas.ActionMenuUI.gameObject.SetActive(false);
    }
}
