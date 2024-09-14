using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ActionMenuUI는 ActionMenu를 기반으로 동적 버튼 UI를 생성하고 관리하는 클래스입니다.
/// </summary>
public class UserActionUI : MonoBehaviour
{
    [SerializeField] private Image _backgroundBorderImg;
    [SerializeField] private Image _backgroundImg;
    [SerializeField] private UserActionBtn _userActionBtnPrefab; // 버튼 프리팹
    [SerializeField] private Transform _btnParent; // 버튼들을 담을 컨테이너

    private const float WIDTH_PER_DEPTH = 300;

    private List<UserActionBtn> _rootBtns = new List<UserActionBtn>(); // 현재 활성화된 버튼 리스트
    private List<UserActionBtn> _allBtns = new List<UserActionBtn>(); // 현재 활성화된 버튼 리스트
    private List<UserActionBtn> _leafBtns = new List<UserActionBtn>();

    private int _targetDepth;
    
    
    public bool IsAllCompleted
    {
        get
        {
            // 모든 leaf 버튼의 IsCompleted가 true인지 확인
            return _leafBtns.All(btn => btn.IsCompleted);
        }
    }

    private Dictionary<int, List<UserActionBtn>> _depthBtnDictionary = new Dictionary<int, List<UserActionBtn>>();

    public void Hide(float duration){
        // 배경 테두리 이미지 숨기기
        _backgroundBorderImg.DOFade(0, duration).SetEase(Ease.InOutQuad);
        // 배경 이미지 숨기기
        _backgroundImg.DOFade(0, duration).SetEase(Ease.InOutQuad);
        FoldAll(duration);
        foreach(UserActionBtn rootBtn in _rootBtns){
            rootBtn.Hide(duration);
        }
    }
    public void Show(float duration){
        // 배경 테두리 이미지 숨기기
        _backgroundBorderImg.DOFade(1, duration).SetEase(Ease.InOutQuad);
        // 배경 이미지 숨기기
        _backgroundImg.DOFade(.5f, duration).SetEase(Ease.InOutQuad);
        foreach(UserActionBtn rootBtn in _rootBtns){
            rootBtn.Show(duration);
        }
    }

    public void SetBackgroundSize(float targetWidth, float duration)
    {
        // _backgroundBorderImg의 가로 길이 설정
        _backgroundBorderImg.rectTransform.DOSizeDelta(new Vector2(targetWidth, _backgroundBorderImg.rectTransform.sizeDelta.y), duration)
            .SetEase(Ease.InOutQuad);

        // _backgroundImg의 가로 길이 설정
        _backgroundImg.rectTransform.DOSizeDelta(new Vector2(targetWidth, _backgroundImg.rectTransform.sizeDelta.y), duration)
            .SetEase(Ease.InOutQuad);
    }

    private void FoldAll(float duration){
        foreach(UserActionBtn rootBtn in _rootBtns){
            rootBtn.Fold(duration);
        }
    }


    /// <summary>
    /// ActionMenu를 받아 UI를 초기화하는 메서드
    /// </summary>
    public void Init(List<UserActionBtnContent> userActionBtnContents)
    {
        ClearButtons(); // 기존 버튼 제거

        _depthBtnDictionary.Clear();
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
        _targetDepth = 0;

        SetBackgroundSize(WIDTH_PER_DEPTH, 0f);
    }

    public void OnBtnClicked(UserActionBtn userActionBtn){

        const float animatingSec = .3f;
        if(_targetDepth != userActionBtn.Depth){
            _targetDepth = userActionBtn.Depth;
             SetBackgroundSize(WIDTH_PER_DEPTH * _targetDepth, animatingSec);
        }
        if (userActionBtn.IsLeafBtn)
        {
            if (userActionBtn.IsExecuting)
            {
                Debug.Log($"Button '{userActionBtn.BtnContent.Title}' is already executing.");
                return;
            }
            // 최하위 버튼의 액션 실행
            if (userActionBtn.BtnContent.onClickedAction != null)
            {
                StartCoroutine(ExecuteAction(userActionBtn));
            }
        }
        else
        {
            List<UserActionBtn> identicalDepthBtns = _depthBtnDictionary.GetValueOrDefault(userActionBtn.Depth);
            for(int i = 0 ; i < identicalDepthBtns.Count ; i++){
                UserActionBtn btn = identicalDepthBtns[i];
                if(btn == userActionBtn){
                    Debug.Log("눌림");
                    if(btn.IsExpanded){
                        btn.Fold(animatingSec);
                    }
                    else{
                        btn.Expand(animatingSec);
                    }
                }
                else{
                    btn.Fold(animatingSec);
                }
            }
        }
    }
    // 액션을 실행하고 완료된 후 상태를 갱신하는 메서드
    public IEnumerator ExecuteAction(UserActionBtn userActionBtn)
    {
        Hide(.5f);
        userActionBtn.IsExecuting = true;
        yield return userActionBtn.BtnContent.onClickedAction.ExecuteRoutine();
        userActionBtn.IsExecuting = false;
        userActionBtn.SetCompleted(true);
        Debug.Log($"Action '{userActionBtn.BtnContent.Title}' completed.");
        if(!IsAllCompleted){
            Show(.5f);
        }
    }


    public UserActionBtn CreateActionMenuBtn(UserActionBtn parentBtn, UserActionBtnContent btnContent, int index, int depth){

        UserActionBtn instBtn = Instantiate(_userActionBtnPrefab.gameObject, (parentBtn == null) ? _btnParent : parentBtn.transform).GetComponent<UserActionBtn>();
        instBtn.Init(this, parentBtn, btnContent, index, depth);
        instBtn.transform.SetParent((parentBtn == null) ? transform : parentBtn.transform);
        instBtn.transform.localPosition = WIDTH_PER_DEPTH * Vector3.right + 100 * index * Vector3.down;
        instBtn.name = $"{btnContent.Title} {depth}";
        _allBtns.Add(instBtn);
        
        // depth에 따른 딕셔너리에 추가
        if (!_depthBtnDictionary.ContainsKey(depth))
        {
            _depthBtnDictionary[depth] = new List<UserActionBtn>();
        }
        _depthBtnDictionary[depth].Add(instBtn);

        return instBtn;
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
