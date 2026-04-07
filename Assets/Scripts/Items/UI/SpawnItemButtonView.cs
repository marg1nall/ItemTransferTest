using System;
using Common.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Items.UI
{
    public class SpawnItemButtonView : BaseView
    {
        public event Action Clicked;
        
        [SerializeField] private Button _button;

        protected override void Awake()
        {
            base.Awake();

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

        private void OnButtonClicked()
        {
            Clicked?.Invoke();
        }
    }
}
