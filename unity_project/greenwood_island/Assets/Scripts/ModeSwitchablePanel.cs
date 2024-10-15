using System.Collections.Generic;
using UnityEngine;

public abstract class ModeSwitchablePanel : MonoBehaviour
{
    [SerializeField] private List<PanelState> _panelStates;

    private string _currentState = "";

    [System.Serializable]
    public class PanelState
    {
        public string state;
        public List<GameObject> panels;
    }

    /// <summary>
    /// 가상 메서드로 설정된 상태를 변경합니다.
    /// </summary>
    public virtual void SetState(string state)
    {
        _currentState = state;
        Debug.Log($"[ModeSwitchablePanel] 상태가 '{state}'로 변경되었습니다.");
        UpdatePanelVisibility();
    }

    private void UpdatePanelVisibility()
    {
        foreach (var panelState in _panelStates)
        {
            bool isActive = panelState.state == _currentState;
            SetPanelVisibility(panelState.panels, isActive);
        }
    }

    private void SetPanelVisibility(List<GameObject> panels, bool isVisible)
    {
        foreach (var panel in panels)
        {
            panel.SetActive(isVisible);
        }
    }
}
