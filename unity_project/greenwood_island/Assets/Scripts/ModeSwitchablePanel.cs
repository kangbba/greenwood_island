using System.Collections.Generic;
using UnityEngine;

public abstract class ModeSwitchablePanel : MonoBehaviour
{
    // 모드별로 관리할 패널들
    [SerializeField] private List<PanelState> _panelStates;

    private string _currentState = ""; // 현재 상태를 string으로 저장

    [System.Serializable]
    public class PanelState
    {
        public string state;  // 상태 값 (string)
        public List<GameObject> panels;  // 해당 상태에서 활성화할 패널들
    }

    /// <summary>
    /// 모드를 설정합니다 (string 기반).
    /// </summary>
    public void SetState(string state)
    {
        _currentState = state;
        Debug.Log($"[ModeSwitchablePanel] 상태가 '{state}'로 변경되었습니다.");
        UpdatePanelVisibility();  // 상태에 따른 패널 표시 업데이트
    }

    /// <summary>
    /// 현재 상태에 따라 패널을 활성화 또는 비활성화합니다.
    /// </summary>
    private void UpdatePanelVisibility()
    {
        foreach (var panelState in _panelStates)
        {
            bool isActive = panelState.state == _currentState;
            SetPanelVisibility(panelState.panels, isActive);
        }
    }

    /// <summary>
    /// 패널 리스트의 표시 상태를 설정합니다.
    /// </summary>
    private void SetPanelVisibility(List<GameObject> panels, bool isVisible)
    {
        foreach (var panel in panels)
        {
            panel.SetActive(isVisible);
        }
    }
}
