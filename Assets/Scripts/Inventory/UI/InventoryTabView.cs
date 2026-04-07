using System;
using System.Collections.Generic;
using Inventory.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class InventoryTabView : MonoBehaviour
    {
        [SerializeField] private InventoryTabType _tabType;
        [SerializeField] private InventoryTabButtonView _tabButtonView;
        [SerializeField] private GameObject _contentRoot;
        [SerializeField] private Transform _slotsRoot;
        [SerializeField] private InventorySlotView _slotPrefab;
        [SerializeField] private Button _addSlotButton;

        private readonly List<InventorySlotView> _slotViews = new();

        public InventoryTabType TabType => _tabType;
        public InventoryTabButtonView TabButtonView => _tabButtonView;
        public IReadOnlyList<InventorySlotView> Slots => _slotViews;

        public event Action<InventoryTabType> AddSlotClicked;
        public event Action<InventoryTabType, int> SlotClicked;

        private void Awake()
        {
            if (_addSlotButton != null)
            {
                _addSlotButton.onClick.AddListener(() => AddSlotClicked?.Invoke(_tabType));
            }
        }

        public void SetActive(bool isActive)
        {
            if (_contentRoot != null)
            {
                _contentRoot.SetActive(isActive);
            }
        }

        public void SetAddSlotVisible(bool isVisible)
        {
            if (_addSlotButton != null)
            {
                _addSlotButton.gameObject.SetActive(isVisible);
            }
        }

        public void EnsureSlotCount(int count)
        {
            RemoveDestroyedSlotViews();

            while (_slotViews.Count < count)
            {
                var slotView = Instantiate(_slotPrefab, _slotsRoot);
                slotView.Clicked += OnSlotClicked;
                _slotViews.Add(slotView);
            }
        }

        private void OnSlotClicked(InventoryTabType tabType, int slotIndex)
        {
            SlotClicked?.Invoke(tabType, slotIndex);
        }

        public bool TryGetSlotAtScreenPoint(Vector2 screenPoint, out InventorySlotView slotView)
        {
            RemoveDestroyedSlotViews();

            for (var i = 0; i < _slotViews.Count; i++)
            {
                if (_slotViews[i] == null)
                {
                    continue;
                }

                if (_slotViews[i].ContainsScreenPoint(screenPoint))
                {
                    slotView = _slotViews[i];
                    return true;
                }
            }

            slotView = null;
            return false;
        }

        private void RemoveDestroyedSlotViews()
        {
            for (var i = _slotViews.Count - 1; i >= 0; i--)
            {
                if (_slotViews[i] == null)
                {
                    _slotViews.RemoveAt(i);
                }
            }
        }
    }
}
