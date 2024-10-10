using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceTransitionGroup : Element
{
    private List<PlaceTransitionNode> _nodes;  // 방 리스트
    private List<Button> _allButtons = new List<Button>(); // 그룹 내 모든 버튼을 관리하는 리스트

    // 생성자 - 노드 리스트를 받아서 그룹을 자동으로 관리
    public PlaceTransitionGroup(List<PlaceTransitionNode> nodes)
    {
        _nodes = nodes;

        // 그룹 매니저를 각 노드에 자동으로 할당
        foreach (var node in _nodes)
        {
            node.SetGroupManager(this);  // 그룹 매니저로서 자신을 할당
        }
    }

    // 방 이동을 시작하는 루틴
    public override IEnumerator ExecuteRoutine()
    {
        if (_nodes.Count > 0)
        {
            // 첫 번째 방으로 이동
            CoroutineUtils.StartCoroutine(_nodes[0].ExecuteRoutine());
        }
        yield return null;
    }

    // 그룹 내에서 다른 노드로 이동하는 메서드
    public void MoveToNode(PlaceTransitionNode targetNode)
    {
        DestroyAllButtons();  // 노드를 이동하기 전에 모든 버튼을 파괴
        CoroutineUtils.StartCoroutine(targetNode.ExecuteRoutine());
    }

    // placeID로 특정 노드를 반환하는 메서드
    public PlaceTransitionNode GetNodeByPlaceID(string placeID)
    {
        foreach (var node in _nodes)
        {
            if (node.GetPlaceID() == placeID)
            {
                return node;
            }
        }
        return null;  // 해당하는 노드를 찾지 못한 경우
    }

    // 버튼을 그룹에 등록하는 메서드
    public void RegisterButton(Button button)
    {
        _allButtons.Add(button);
    }

    // 모든 버튼을 파괴하는 메서드
    public void DestroyAllButtons()
    {
        foreach (var button in _allButtons)
        {
            if (button != null)
            {
                Object.Destroy(button.gameObject);  // 각 버튼 인스턴스를 파괴
            }
        }
        _allButtons.Clear();  // 리스트 초기화
    }

    // 즉시 실행 메서드
    public override void ExecuteInstantly()
    {
        // 필요 시 구현
    }
}
