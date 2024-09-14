using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ActionMenu는 버튼의 제목, 하위 버튼, 그리고 SequentialElement 형태의 액션을 포함하는 클래스입니다.
/// 여러 레벨의 버튼과 액션을 재귀적으로 관리할 수 있습니다.
/// </summary>
/// 
public class UserActionBtnContent{

    public string Title; // 버튼 제목
    public SequentialElement onClickedAction; // 버튼 클릭 시 실행할 SequentialElement 액션 (최하위 메뉴일 때만 사용)
    public List<UserActionBtnContent> SubBtnContents; // 하위 버튼 리스트 (상위 메뉴일 때만 사용)

    // 생성자 (최하위 메뉴용)
    public UserActionBtnContent(string title, SequentialElement onClickedAction)
    {
        Title = title;
        this.onClickedAction = onClickedAction;
        SubBtnContents = null; // 최하위 메뉴는 하위 버튼 리스트가 없음
    }

    // 생성자 (상위 메뉴용)
    public UserActionBtnContent(string title, List<UserActionBtnContent> subMenus)
    {
        Title = title;
        this.onClickedAction = null; // 상위 메뉴는 클릭 액션이 없음
        SubBtnContents = subMenus ?? new List<UserActionBtnContent>();
    }

}
[System.Serializable]
public class UserActionEnter : Element
{
    [SerializeField] private List<UserActionBtnContent> _userActionBtnContents = new List<UserActionBtnContent>();

    public List<UserActionBtnContent> UserActionBtnContents { get => _userActionBtnContents; }
    // 생성자 (최하위 메뉴용)
    public UserActionEnter(params UserActionBtnContent[] userActionBtnContents)
    {
        _userActionBtnContents = new List<UserActionBtnContent>(userActionBtnContents);
    }


    // ActionMenu 실행 루틴
    public override IEnumerator ExecuteRoutine()
    {
        UserActionUI userActionUI = UIManager.Instance.SystemCanvas.UserActionUI;
        // UIManager를 통해 ActionMenuUI를 초기화하고, ActionMenu를 전달하여 UI를 생성
        userActionUI.gameObject.SetActive(true);
        userActionUI.Init(_userActionBtnContents);
        userActionUI.Show(1f);
        yield return new WaitForSeconds(1f);
        // ActionMenuUI의 모든 작업이 완료될 때까지 대기
        yield return new WaitUntil(() => userActionUI.IsAllCompleted);
        userActionUI.Hide(.5f);
        yield return new WaitForSeconds(.5f);

        userActionUI.gameObject.SetActive(false);
    }
}
