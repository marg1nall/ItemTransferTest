using Board.Api;
using Board.Models;
using Inventory.Api;
using Inventory.Models;

namespace Inventory.Logic
{
    public class InventoryToBoardTransfer
    {
        private readonly IBoardStateRepository _boardRepository;
        private readonly IInventoryStateRepository _inventoryRepository;

        public InventoryToBoardTransfer(IBoardStateRepository boardRepository, IInventoryStateRepository inventoryRepository)
        {
            _boardRepository = boardRepository;
            _inventoryRepository = inventoryRepository;
        }
        
        public InventoryToBoardTransferResult ReturnToAnyFreeCell(InventoryTabType tabType, int slotIndex)
        {
            var board = _boardRepository.Get();

            for (var y = 0; y < board.Height; y++)
            {
                for (var x = 0; x < board.Width; x++)
                {
                    var position = new BoardPosition(x, y);
                    if (!board.IsFree(position))
                    {
                        continue;
                    }

                    return Transfer(tabType, slotIndex, position);
                }
            }

            return InventoryToBoardTransferResult.TargetUnavailable(null);
        }
        
        private InventoryToBoardTransferResult Transfer(InventoryTabType tabType, int slotIndex, BoardPosition targetPosition)
        {
            var board = _boardRepository.Get();
            var inventory = _inventoryRepository.Get();

            if (!inventory.TryTake(tabType, slotIndex, out var item))
            {
                return InventoryToBoardTransferResult.ItemMissing();
            }

            if (board.TryPlace(item, targetPosition))
            {
                return InventoryToBoardTransferResult.Success(item, targetPosition);
            }

            inventory.TryStore(item, out _, out _);
            return InventoryToBoardTransferResult.TargetUnavailable(item);
        }
    }
}
