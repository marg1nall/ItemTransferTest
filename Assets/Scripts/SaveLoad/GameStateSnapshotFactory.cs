using System.Collections.Generic;
using Board.Api;
using Board.Models;
using Inventory.Api;
using Inventory.Models;
using Items.Api;
using Items.Models;
using SaveLoad.Models;

namespace SaveLoad
{
    public class GameStateSnapshotFactory
    {
        private readonly IBoardStateRepository _boardRepository;
        private readonly IInventoryStateRepository _inventoryRepository;
        private readonly IItemIdGenerator _itemIdGenerator;

        public GameStateSnapshotFactory(IBoardStateRepository boardRepository, IInventoryStateRepository inventoryRepository,
            IItemIdGenerator itemIdGenerator)
        {
            _boardRepository = boardRepository;
            _inventoryRepository = inventoryRepository;
            _itemIdGenerator = itemIdGenerator;
        }

        public GameStateSnapshot Create()
        {
            GameStateSnapshot snapshot = new GameStateSnapshot
            {
                NextItemId = _itemIdGenerator.PeekNext()
            };

            foreach ((BoardPosition position, ItemData item) entry in _boardRepository.Get().EnumerateItems())
            {
                snapshot.BoardItems.Add(new BoardItemSnapshot
                {
                    Id = entry.item.Id,
                    Type = (int)entry.item.Type,
                    N = entry.item.N,
                    X = entry.position.X,
                    Y = entry.position.Y
                });
            }

            foreach (KeyValuePair<InventoryTabType, InventoryTabState> pair in _inventoryRepository.Get().Tabs)
            {
                InventoryTabSnapshot tabSnapshot = new InventoryTabSnapshot
                {
                    TabType = (int)pair.Key
                };

                foreach (InventorySlotState slot in pair.Value.Slots)
                {
                    tabSnapshot.Slots.Add(new InventorySlotSnapshot
                    {
                        Index = slot.Index,
                        HasItem = !slot.IsEmpty,
                        Id = slot.Item?.Id ?? 0,
                        Type = (int)(slot.Item?.Type ?? 0),
                        N = slot.Item?.N ?? 0
                    });
                }

                snapshot.InventoryTabs.Add(tabSnapshot);
            }

            return snapshot;
        }
    }
}
