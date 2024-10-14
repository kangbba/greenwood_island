using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCanvas : MonoBehaviour
{
    [SerializeField] private UICursor _uiCursor;

    public UICursor UiCursor { get => _uiCursor; }
}
