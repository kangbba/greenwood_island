using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvas : MonoBehaviour
{
    [SerializeField] private Transform _backgroundLayer;
    [SerializeField] private Transform _characterLayer;
    [SerializeField] private Transform _screenEffectLayer;

    public Transform CharacterLayer { get => _characterLayer; }
    public Transform BackgroundLayer { get => _backgroundLayer; }
    public Transform ScreenEffectLayer { get => _screenEffectLayer; }
}
