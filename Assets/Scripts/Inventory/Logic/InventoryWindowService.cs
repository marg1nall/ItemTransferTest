using System;
using System.Collections.Generic;
using Board.Logic;
using Inventory.Api;
using Inventory.Models;
using Inventory.UI;
using Items.Models;
using Items.UI;
using Zenject;

namespace Inventory.Logic
{
    public class InventoryWindowService : IInitializable, IDisposable
    {
        private readonly InventoryButtonView _buttonView;
        private readonly InventoryWindowView _windowView;
        private readonly IInventoryStateRepository _inventoryRepository;
        private readonly InventorySlotAdder _inventorySlotAdder;
        private readonly InventoryDragDropFacade _dragDropFacade;
        private readonly BoardPlacementService _boardPlacementService;
        private InventoryTabType _currentTabType = InventoryTabType.AZero;

        public InventoryWindowService(InventoryButtonView buttonView, InventoryWindowView windowView,
            IInventoryStateRepository inventoryRepository, InventorySlotAdder inventorySlotAdder,
            InventoryDragDropFacade dragDropFacade, BoardPlacementService boardPlacementService)
        {
            _buttonView = buttonView;
            _windowView = windowView;
            _inventoryRepository = inventoryRepository;
            _inventorySlotAdder = inventorySlotAdder;
            _dragDropFacade = dragDropFacade;
            _boardPlacementService = boardPlacementService;
        }

        public void Initialize()
        {
            _buttonView.Clicked += OnInventoryClicked;

            foreach (InventoryTabView tabView in _windowView.Tabs)
            {
                tabView.AddSlotClicked += OnAddSlotClicked;
                tabView.SlotClicked += OnSlotClicked;
                if (tabView.TabButtonView != null)
                {
                    tabView.TabButtonView.Clicked += OnTabButtonClicked;
                }
            }

            Refresh();
            _windowView.Hide();
        }

        public void Dispose()
        {
            _buttonView.Clicked -= OnInventoryClicked;

            foreach (InventoryTabView tabView in _windowView.Tabs)
            {
                tabView.AddSlotClicked -= OnAddSlotClicked;
                tabView.SlotClicked -= OnSlotClicked;
                if (tabView.TabButtonView != null)
                {
                    tabView.TabButtonView.Clicked -= OnTabButtonClicked;
                }
            }
        }

        public void OpenTab(InventoryTabType tabType)
        {
            _currentTabType = tabType;
            Refresh();
            _windowView.OpenTab(tabType);
        }

        public void SetInventoryFullAlert(bool isAlert)
        {
            _buttonView.SetAlertState(isAlert);
        }

        public void PlaceItemViewInSlot(ItemView itemView, ItemData item, InventoryTabType tabType, int slotIndex)
        {
            if (!_windowView.TryGetTab(tabType, out InventoryTabView tabView))
            {
                return;
            }

            tabView.EnsureSlotCount(slotIndex + 1);
            tabView.Slots[slotIndex].AttachItemView(itemView, item);
        }

        public void ClearAllItemViews()
        {
            foreach (var tabView in _windowView.Tabs)
            {
                for (var i = 0; i < tabView.Slots.Count; i++)
                {
                    tabView.Slots[i].ClearItemView();
                }
            }
        }

        public void Refresh()
        {
            InventoryState inventory = _inventoryRepository.Get();

            foreach (KeyValuePair<InventoryTabType, InventoryTabState> pair in inventory.Tabs)
            {
                if (!_windowView.TryGetTab(pair.Key, out InventoryTabView tabView))
                {
                    continue;
                }

                tabView.SetAddSlotVisible(pair.Value.CanExpand);
                tabView.EnsureSlotCount(pair.Value.Slots.Count);

                for (var i = 0; i < pair.Value.Slots.Count; i++)
                {
                    InventorySlotState slotState = pair.Value.Slots[i];
                    tabView.Slots[i].Bind(pair.Key, slotState.Index, slotState.Item);
                }
            }
        }

        private void OnInventoryClicked()
        {
            SetInventoryFullAlert(false);

            if (_windowView.IsVisible)
            {
                _windowView.Hide();
                return;
            }

            OpenTab(_currentTabType);
        }

        private void OnAddSlotClicked(InventoryTabType tabType)
        {
            if (_inventorySlotAdder.Add(tabType))
            {
                OpenTab(tabType);
            }
        }

        private void OnTabButtonClicked(InventoryTabType tabType)
        {
            OpenTab(tabType);
        }

        private void OnSlotClicked(InventoryTabType tabType, int slotIndex)
        {
            if (tabType == InventoryTabType.B)
            {
                return;
            }

            InventoryToBoardTransferResult result = _dragDropFacade.TryReturnFromInventoryByClick(tabType, slotIndex);
            if (!result.IsSuccess || result.Item == null)
            {
                return;
            }

            if (!_windowView.TryGetTab(tabType, out InventoryTabView tabView))
            {
                return;
            }

            ItemView itemView = tabView.Slots[slotIndex].DetachItemView();
            if (itemView == null)
            {
                return;
            }

            itemView.gameObject.SetActive(true);
            itemView.Initialize(result.Item);
            _boardPlacementService.PlaceItem(itemView, result.Position);
            Refresh();
        }
    }
}
