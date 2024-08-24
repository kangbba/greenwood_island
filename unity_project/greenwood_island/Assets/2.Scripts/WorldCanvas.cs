using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldCanvas : MonoBehaviour
{
    [SerializeField] private Transform _placeLayer;
    [SerializeField] private Transform _characterLayer;
    [SerializeField] private Transform _screenEffectLayer;

    [SerializeField] private Image _placeOverlayFilm;
    public Image PlaceOverlayFilm { get => _placeOverlayFilm; }

    [SerializeField] private Image _screenOverlayFilm;
    public Image ScreenOverlayFilm { get => _screenOverlayFilm; }

    

    public Transform CharacterLayer { get => _characterLayer; }
    public Transform PlaceLayer { get => _placeLayer; }
    public Transform ScreenEffectLayer { get => _screenEffectLayer; }
}
