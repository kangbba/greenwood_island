using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldCanvas : MonoBehaviour
{
    [SerializeField] private Transform _placeLayer;
    [SerializeField] private Transform _characterLayerWorld;

    [SerializeField] private Image _placeOverlayFilm;
    public Image PlaceOverlayFilm { get => _placeOverlayFilm; }

    

    public Transform CharacterLayerWorld { get => _characterLayerWorld; }
    public Transform PlaceLayer { get => _placeLayer; }
}
