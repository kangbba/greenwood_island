using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Singleton instance
    private static UIManager _instance;

    // Public property to access the singleton instance
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Find an existing instance in the scene or create a new one
                _instance = FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }

    public DialoguePanel DialoguePanel { get => _dialoguePanel; }

    // Awake method to ensure only one instance exists
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance when loading new scenes
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // Destroy this instance if it's a duplicate
        }
    }

    // Serialized fields for UI elements
    [SerializeField] private Canvas _worldCanvas;
    [SerializeField] private Canvas _systemCanvas;
    [SerializeField] private DialoguePanel _dialoguePanel;

    // Additional methods for UIManager can be added here...
}
