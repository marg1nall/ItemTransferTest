using System;
using Inventory.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class InventoryTabButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private InventoryTabType _tabType;

        public event Action<InventoryTabType> Clicked;

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

        private void OnButtonClicked()
        {
            Clicked?.Invoke(_tabType);
        }
    }
}
