using Board.Models;
using Inventory.Api;
using Inventory.Models;
using Items.Models;

namespace Inventory.Logic
{
    public class InventoryDragDropFacade
    {
        private readonly BoardToInventoryTransfer _boardToInventoryTransfer;
        private readonly InventoryToBoardTransfer _inventoryToBoardTransfer;
        private readonly IInventoryStateRepository _inventoryRepository;

        public InventoryDragDropFacade(BoardToInventoryTransfer boardToInventoryTransfer, 
            InventoryToBoardTransfer inventoryToBoardTransfer, IInventoryStateRepository inventoryRepository)
        {
            _boardToInventoryTransfer = boardToInventoryTransfer;
            _inventoryToBoardTransfer = inventoryToBoardTransfer;
            _inventoryRepository = inventoryRepository;
        }

        public BoardToInventoryTransferResult TryMoveFromBoardToFirstAvailableSlot(BoardPosition sourcePosition)
        {
            return _boardToInventoryTransfer.TransferToFirstAvailableSlot(sourcePosition);
        }

        public BoardToInventoryTransferResult TryMoveFromBoardToSpecificSlot(BoardPosition sourcePosition, InventoryTabType tabType, int slotIndex)
        {
            return _boardToInventoryTransfer.TransferToSpecificSlot(sourcePosition, tabType, slotIndex);
        }

        public InventoryToBoardTransferResult TryReturnFromInventoryByClick(InventoryTabType tabType, int slotIndex)
        {
            return _inventoryToBoardTransfer.ReturnToAnyFreeCell(tabType, slotIndex);
        }

        public bool HasSpaceFor(ItemData item)
        {
            if (item == null)
            {
                return false;
            }

            return _inventoryRepository.Get().HasSpaceFor(item);
        }
    }
}
