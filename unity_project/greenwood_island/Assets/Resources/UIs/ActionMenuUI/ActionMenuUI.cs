using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ActionMenuUI는 ActionMenu를 기반으로 동적 버튼 UI를 생성하고 관리하는 클래스입니다.
/// </summary>
public class ActionMenuUI : MonoBehaviour
{
    [SerializeField] private ActionMenuButton actionMenuButtonPrefab; // 버튼 프리팹
    [SerializeField] private Transform buttonContainer; // 버튼들을 담을 컨테이너

    private List<ActionMenuButton> _rootBtns = new List<ActionMenuButton>(); // 현재 활성화된 버튼 리스트
    private ActionMenu _rootMenu; // UI에 전달된 루트 메뉴

    /// <summary>
    /// ActionMenu를 받아 UI를 초기화하는 메서드
    /// </summary>
    public void Init(ActionMenu rootMenu)
    {
        ClearButtons(); // 기존 버튼 제거
        _rootMenu = rootMenu; // 루트 메뉴 저장

        for(int i = 0 ; i < rootMenu.SubMenus.Count ; i++){
            ActionMenu subMenu = rootMenu.SubMenus[i];
            ActionMenuButton actionMenuButton = CreateActionMenuBtn(null, subMenu, i, 0);
            _rootBtns.Add(actionMenuButton);
        }

    }

    public ActionMenuButton CreateActionMenuBtn(ActionMenuButton parentBtn, ActionMenu actionMenu, int index, int depth){

        ActionMenuButton menuBtn = Instantiate(actionMenuButtonPrefab.gameObject).GetComponent<ActionMenuButton>();
        menuBtn.Init(this, parentBtn, actionMenu, index, depth);
        menuBtn.transform.SetParent((parentBtn == null) ? transform : parentBtn.transform);
        menuBtn.transform.localPosition = 300 * Vector3.right + 100 * index * Vector3.down;
        menuBtn.name = $"{actionMenu.Title} {depth}";
        return menuBtn;
    }

    // 버튼 UI 클리어 메서드
    public void ClearButtons()
    {
        foreach (var button in _rootBtns)
        {
            Destroy(button.gameObject);
        }
        _rootBtns.Clear();
    }
}
