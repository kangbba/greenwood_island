using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlaceTransitionNode : Element
{
    private string _placeID;
    private string _previousPlaceID;
    private Vector2 _anchoredPosition;
    private List<PlaceTransitionNode> _connectedNodes;
    private PlaceTransitionGroup _groupManager;
    private Button _buttonInstance;
    private Button _backButtonInstance;

    // 생성자
    public PlaceTransitionNode(string placeID, string previousPlaceID, List<PlaceTransitionNode> connectedNodes, Vector2 anchoredPosition)
    {
        _placeID = placeID;
        _previousPlaceID = previousPlaceID;
        _connectedNodes = connectedNodes;
        _anchoredPosition = anchoredPosition;
    }

    // 그룹 매니저를 설정하는 메서드
    public void SetGroupManager(PlaceTransitionGroup groupManager)
    {
        _groupManager = groupManager;

        // 연결된 각 노드에도 그룹 매니저를 할당
        foreach (var node in _connectedNodes)
        {
            node.SetGroupManager(groupManager);
        }
    }

    // placeID 반환 메서드
    public string GetPlaceID()
    {
        return _placeID;
    }

    // 방에 진입했을 때 실행하는 루틴 (버튼 생성)
    public override IEnumerator ExecuteRoutine()
    {
        Debug.Log($"방 '{_placeID}'에 진입했습니다.");

        // UIManager에서 버튼 생성
        _buttonInstance = Object.Instantiate(UIManager.SystemCanvas.PlaceTransitionBtnPrefab, UIManager.SystemCanvas.transform);
        RectTransform buttonRectTransform = _buttonInstance.GetComponent<RectTransform>();
        buttonRectTransform.anchoredPosition = _anchoredPosition;

        // 그룹에 버튼 등록
        _groupManager.RegisterButton(_buttonInstance);

        // 연결된 방으로 이동하는 버튼들 설정
        foreach (var connectedNode in _connectedNodes)
        {
            _buttonInstance.onClick.AddListener(() =>
            {
                // 연결된 방으로 이동
                _groupManager.MoveToNode(connectedNode);
            });
        }

        // 나가기 버튼 설정 (되돌아가기)
        if (!string.IsNullOrEmpty(_previousPlaceID))
        {
            // 나가기 버튼 생성 (왼쪽 아래 고정 위치)
            _backButtonInstance = Object.Instantiate(UIManager.SystemCanvas.PlaceTransitionBtnPrefab, UIManager.SystemCanvas.transform);
            RectTransform backButtonRectTransform = _backButtonInstance.GetComponent<RectTransform>();
            backButtonRectTransform.anchoredPosition = new Vector2(-200, -200);  // 왼쪽 아래 고정 위치

            // 그룹에 나가기 버튼도 등록
            _groupManager.RegisterButton(_backButtonInstance);

            // 나가기 버튼 동작
            _backButtonInstance.onClick.AddListener(() =>
            {
                // 이전 방으로 이동
                _groupManager.MoveToNode(_groupManager.GetNodeByPlaceID(_previousPlaceID));
            });
        }

        yield return new WaitUntil(() => _buttonInstance == null);  // 버튼이 파괴되면 대기 종료
    }

    // 즉시 실행 메서드 (선택적)
    public override void ExecuteInstantly()
    {
        // 필요 시 구현
    }
}
