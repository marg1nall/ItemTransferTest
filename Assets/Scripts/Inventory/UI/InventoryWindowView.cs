using System.Collections.Generic;
using Inventory.Models;
using UnityEngine;

namespace Inventory.UI
{
    public class InventoryWindowView : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private List<InventoryTabView> _tabs = new();

        public IReadOnlyList<InventoryTabView> Tabs => _tabs;
        public bool IsVisible => _root != null && _root.activeSelf;

        private void Awake()
        {
            Hide();

            for (var i = 0; i < _tabs.Count; i++)
            {
                _tabs[i].SetActive(false);
            }
        }
        

        public void OpenTab(InventoryTabType tabType)
        {
            Show();

            for (var i = 0; i < _tabs.Count; i++)
            {
                _tabs[i].SetActive(_tabs[i].TabType == tabType);
            }
        }

        public bool ContainsScreenPoint(Vector2 screenPoint)
        {
            if (_root == null || !_root.activeInHierarchy)
            {
                return false;
            }

            var rectTransform = _root.transform as RectTransform;
            if (rectTransform == null)
            {
                return false;
            }

            // todo сейчас применимо только к оверлей канвасу
            return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPoint, null);
        }

        public bool TryGetTab(InventoryTabType tabType, out InventoryTabView tabView)
        {
            for (var i = 0; i < _tabs.Count; i++)
            {
                if (_tabs[i].TabType != tabType)
                {
                    continue;
                }

                tabView = _tabs[i];
                return true;
            }

            tabView = null;
            return false;
        }

        public bool TryGetSlotAtScreenPoint(Vector2 screenPoint, out InventorySlotView slotView)
        {
            for (var i = 0; i < _tabs.Count; i++)
            {
                if (!_tabs[i].gameObject.activeInHierarchy)
                {
                    continue;
                }

                if (_tabs[i].TryGetSlotAtScreenPoint(screenPoint, out slotView))
                {
                    return true;
                }
            }

            slotView = null;
            return false;
        }
        
        public void Hide()
        {
            if (_root != null)
            {
                _root.SetActive(false);
            }
        }
        
        private void Show()
        {
            if (_root != null)
            {
                _root.SetActive(true);
            }
        }
    }
}
