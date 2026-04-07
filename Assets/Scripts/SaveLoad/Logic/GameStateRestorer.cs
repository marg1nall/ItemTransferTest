using Board.Api;
using Board.Models;
using Inventory.Api;
using Inventory.Models;
using Items.Api;
using Items.Models;
using SaveLoad.Models;

namespace SaveLoad.Logic
{
    public class GameStateRestorer
    {
        private readonly IBoardStateRepository _boardRepository;
        private readonly IInventoryStateRepository _inventoryRepository;
        private readonly InventoryTabData _configurationTabs;
        private readonly IItemIdGenerator _itemIdGenerator;

        public GameStateRestorer(IBoardStateRepository boardRepository, IInventoryStateRepository inventoryRepository,
            InventoryTabData configurationTabs, IItemIdGenerator itemIdGenerator)
        {
            _boardRepository = boardRepository;
            _inventoryRepository = inventoryRepository;
            _configurationTabs = configurationTabs;
            _itemIdGenerator = itemIdGenerator;
        }

        public void Restore(GameStateSnapshot snapshot)
        {
            BoardState currentBoard = _boardRepository.Get();
            BoardState board = new BoardState(currentBoard.Width, currentBoard.Height);
            InventoryState inventory = new InventoryState(_configurationTabs);

            foreach (BoardItemSnapshot boardItem in snapshot.BoardItems)
            {
                ItemData item = new ItemData(boardItem.Id, (ItemType)boardItem.Type, boardItem.N);
                board.TryPlace(item, new BoardPosition(boardItem.X, boardItem.Y));
            }

            foreach (InventoryTabSnapshot tabSnapshot in snapshot.InventoryTabs)
            {
                InventoryTabType tabType = (InventoryTabType)tabSnapshot.TabType;
                InventoryTabState tab = inventory.GetTab(tabType);

                while (tab.Slots.Count < tabSnapshot.Slots.Count)
                {
                    tab.TryAddSlot();
                }

                foreach (InventorySlotSnapshot slotSnapshot in tabSnapshot.Slots)
                {
                    if (!slotSnapshot.HasItem)
                    {
                        continue;
                    }

                    ItemData item = new ItemData(slotSnapshot.Id, (ItemType)slotSnapshot.Type, slotSnapshot.N);
                    tab.TryStoreAt(item, slotSnapshot.Index);
                }
            }

            _boardRepository.Set(board);
            _inventoryRepository.Set(inventory);
            _itemIdGenerator.Reset(snapshot.NextItemId);
        }
    }
}
