using System.Collections.Generic;
using Board.Api;
using Board.Logic;
using Board.Models;
using Inventory.Api;
using Inventory.Logic;
using Inventory.Models;
using Items;
using Items.Logic;
using Items.Models;
using Items.UI;

namespace SaveLoad.UI
{
    public class GameVisualSyncService
    {
        private readonly IBoardStateRepository _boardRepository;
        private readonly IInventoryStateRepository _inventoryRepository;
        private readonly ItemViewFactory _itemViewFactory;
        private readonly InputDragService _inputDragService;
        private readonly BoardPlacementService _boardPlacementService;
        private readonly InventoryWindowService _inventoryWindowService;

        public GameVisualSyncService(IBoardStateRepository boardRepository, IInventoryStateRepository inventoryRepository,
            ItemViewFactory itemViewFactory, InputDragService inputDragService, BoardPlacementService boardPlacementService,
            InventoryWindowService inventoryWindowService)
        {
            _boardRepository = boardRepository;
            _inventoryRepository = inventoryRepository;
            _itemViewFactory = itemViewFactory;
            _inputDragService = inputDragService;
            _boardPlacementService = boardPlacementService;
            _inventoryWindowService = inventoryWindowService;
        }

        public void Rebuild()
        {
            _boardPlacementService.ClearItems();
            _inventoryWindowService.ClearAllItemViews();
            _inventoryWindowService.Refresh();

            foreach ((BoardPosition position, ItemData item) entry in _boardRepository.Get().EnumerateItems())
            {
                ItemView itemView = _itemViewFactory.Create();
                _inputDragService.Register(itemView);
                itemView.Initialize(entry.item);
                _boardPlacementService.PlaceItem(itemView, entry.position);
            }

            foreach (KeyValuePair<InventoryTabType, InventoryTabState> tabPair in _inventoryRepository.Get().Tabs)
            {
                InventoryTabState tab = tabPair.Value;
                for (int i = 0; i < tab.Slots.Count; i++)
                {
                    InventorySlotState slot = tab.Slots[i];
                    if (slot.Item == null)
                    {
                        continue;
                    }

                    ItemView itemView = _itemViewFactory.Create();
                    _inputDragService.Register(itemView);
                    itemView.Initialize(slot.Item);
                    _inventoryWindowService.PlaceItemViewInSlot(itemView, slot.Item, tabPair.Key, slot.Index);
                }
            }
        }
    }
}
