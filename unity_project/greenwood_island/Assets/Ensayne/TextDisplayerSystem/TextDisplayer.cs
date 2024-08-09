using UnityEngine;
using TMPro;
using System;
using System.Collections;

namespace Ensayne.TextDisplayerSystem
{
    public class TextDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textDisplay;
        [SerializeField] private TextDisplayerSettings _settings;

        private string[] _textsToDisplay;
        private int _currentIndex = 0;
        private bool _isTyping = false;
        private Coroutine _displayCoroutine;

        public event Action OnStartTyping;
        public event Action OnFinishTypingSentence;
        public event Action OnFinishAllSentences;
        public event Action OnSystemDestroyed;
        public event Action OnSystemStarted;

        private void Start()
        {
            OnSystemStarted?.Invoke();
            if (_settings.ShowCallbackLog)
                Debug.Log("System started");

            OnStartTyping += OnStartTyping;
            OnFinishTypingSentence += OnFinishTypingSentence;
            OnFinishAllSentences += OnFinishAllSentences;
            OnSystemDestroyed += OnSystemDestroyed;
            OnSystemStarted += OnSystemStarted;
        }

        private void OnDestroy()
        {
            OnSystemDestroyed?.Invoke();
            if (_settings.ShowCallbackLog)
                Debug.Log("System destroyed");
        }

        public void Init(string[] texts)
        {
            if (_displayCoroutine != null)
            {
                StopCoroutine(_displayCoroutine);
            }

            _textsToDisplay = texts;
            _currentIndex = 0;

            if (_textsToDisplay != null && _textsToDisplay.Length > 0)
            {
                _displayCoroutine = StartCoroutine(DisplayText());
            }
        }

        public void ShowNext()
        {
            if (!_isTyping && _currentIndex < _textsToDisplay.Length)
            {
                _displayCoroutine = StartCoroutine(DisplayText());
            }
        }

        public void SetSpeed(float speed)
        {
            _settings.TypingSpeed = speed;
        }

        public void ClearText()
        {
            if (_displayCoroutine != null)
            {
                StopCoroutine(_displayCoroutine);
            }
            _textDisplay.text = "";
            _isTyping = false;
        }

        private IEnumerator DisplayText()
        {
            _isTyping = true;
            _textDisplay.text = "";
            OnStartTyping?.Invoke();
            if (_settings.ShowCallbackLog)
                Debug.Log("Typing started");

            string currentText = _textsToDisplay[_currentIndex];
            foreach (char letter in currentText.ToCharArray())
            {
                _textDisplay.text += letter;
                yield return new WaitForSeconds(_settings.TypingSpeed);
            }

            _isTyping = false;
            _currentIndex++;
            OnFinishTypingSentence?.Invoke();
            if (_settings.ShowCallbackLog)
                Debug.Log("Sentence typing finished");

            if (_currentIndex >= _textsToDisplay.Length)
            {
                OnFinishAllSentences?.Invoke();
                if (_settings.ShowCallbackLog)
                    Debug.Log("All sentences typing finished");
            }
        }

    }
}
