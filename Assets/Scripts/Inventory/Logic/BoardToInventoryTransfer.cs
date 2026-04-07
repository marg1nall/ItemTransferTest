using Board.Api;
using Board.Models;
using Inventory.Api;
using Inventory.Models;
using Items.Models;

namespace Inventory.Logic
{
    public class BoardToInventoryTransfer
    {
        private readonly IBoardStateRepository _boardRepository;
        private readonly IInventoryStateRepository _inventoryRepository;

        public BoardToInventoryTransfer(IBoardStateRepository boardRepository, IInventoryStateRepository inventoryRepository)
        {
            _boardRepository = boardRepository;
            _inventoryRepository = inventoryRepository;
        }

        public BoardToInventoryTransferResult TransferToFirstAvailableSlot(BoardPosition sourcePosition)
        {
            BoardState board = _boardRepository.Get();
            InventoryState inventory = _inventoryRepository.Get();

            if (!board.TryRemove(sourcePosition, out ItemData item))
            {
                return BoardToInventoryTransferResult.ItemMissing();
            }

            if (inventory.TryStore(item, out InventoryTabType tabType, out int slotIndex))
            {
                return BoardToInventoryTransferResult.Success(item, tabType, slotIndex);
            }

            board.TryPlace(item, sourcePosition);
            return BoardToInventoryTransferResult.InventoryFull(item.TargetTab);
        }

        public BoardToInventoryTransferResult TransferToSpecificSlot(BoardPosition sourcePosition, InventoryTabType tabType, int slotIndex)
        {
            BoardState board = _boardRepository.Get();
            InventoryState inventory = _inventoryRepository.Get();

            if (!board.TryRemove(sourcePosition, out var item))
            {
                return BoardToInventoryTransferResult.ItemMissing();
            }

            if (inventory.TryStoreAt(item, tabType, slotIndex))
            {
                return BoardToInventoryTransferResult.Success(item, tabType, slotIndex);
            }

            board.TryPlace(item, sourcePosition);
            return BoardToInventoryTransferResult.InventoryFull(item.TargetTab);
        }
    }
}
