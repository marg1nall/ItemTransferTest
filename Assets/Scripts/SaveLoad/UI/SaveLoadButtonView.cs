using System;
using UnityEngine;
using UnityEngine.UI;

namespace SaveLoad.UI
{
    public class SaveLoadButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public event Action Clicked;

        private void Awake()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(OnButtonClicked);
            }
        }

        private void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(OnButtonClicked);
            }
        }

        public void SetInteractable(bool isInteractable)
        {
            if (_button != null)
            {
                _button.interactable = isInteractable;
            }
        }

        private void OnButtonClicked()
        {
            Clicked?.Invoke();
        }
    }
}
