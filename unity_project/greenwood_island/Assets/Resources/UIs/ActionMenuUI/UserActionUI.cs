using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ActionMenuUI는 ActionMenu를 기반으로 동적 버튼 UI를 생성하고 관리하는 클래스입니다.
/// </summary>
public class UserActionUI : MonoBehaviour
{
    [SerializeField] private UserActionBtn actionMenuButtonPrefab; // 버튼 프리팹
    [SerializeField] private Transform buttonContainer; // 버튼들을 담을 컨테이너

    private List<UserActionBtn> _rootBtns = new List<UserActionBtn>(); // 현재 활성화된 버튼 리스트
    private List<UserActionBtn> _allBtns = new List<UserActionBtn>(); // 현재 활성화된 버튼 리스트
    private List<UserActionBtn> _leafBtns = new List<UserActionBtn>();


    /// <summary>
    /// ActionMenu를 받아 UI를 초기화하는 메서드
    /// </summary>
    public void Init(List<UserActionBtnContent> userActionBtnContents)
    {
        ClearButtons(); // 기존 버튼 제거

        _allBtns.Clear();
        _rootBtns.Clear();
        _leafBtns.Clear();
        
        for(int i = 0 ; i < userActionBtnContents.Count ; i++){
            UserActionBtnContent btnContent = userActionBtnContents[i];
            UserActionBtn actionMenuButton = CreateActionMenuBtn(null, btnContent, i, 0);
            _rootBtns.Add(actionMenuButton);
        }

        foreach(UserActionBtn btn in _allBtns){
            if(btn.IsLeafBtn){
                _leafBtns.Add(btn);
            }
        }
    }

    public UserActionBtn CreateActionMenuBtn(UserActionBtn parentBtn, UserActionBtnContent btnContent, int index, int depth){

        UserActionBtn menuBtn = Instantiate(actionMenuButtonPrefab.gameObject).GetComponent<UserActionBtn>();
        menuBtn.Init(this, parentBtn, btnContent, index, depth);
        menuBtn.transform.SetParent((parentBtn == null) ? transform : parentBtn.transform);
        menuBtn.transform.localPosition = 300 * Vector3.right + 100 * index * Vector3.down;
        menuBtn.name = $"{btnContent.Title} {depth}";
        _allBtns.Add(menuBtn);
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
