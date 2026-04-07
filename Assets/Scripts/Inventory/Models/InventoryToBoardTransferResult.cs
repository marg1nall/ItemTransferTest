using Board.Models;
using Items.Models;

namespace Inventory.Models
{
    public class InventoryToBoardTransferResult
    {
        private InventoryToBoardTransferResult(bool isSuccess, ItemData item, BoardPosition position)
        {
            IsSuccess = isSuccess;
            Item = item;
            Position = position;
        }

        public bool IsSuccess { get; }
        public ItemData Item { get; }
        public BoardPosition Position { get; }

        public static InventoryToBoardTransferResult Success(ItemData item, BoardPosition position)
        {
            return new InventoryToBoardTransferResult(true, item, position);
        }

        public static InventoryToBoardTransferResult TargetUnavailable(ItemData item)
        {
            return new InventoryToBoardTransferResult(false, item, default);
        }

        public static InventoryToBoardTransferResult ItemMissing()
        {
            return new InventoryToBoardTransferResult(false, null, default);
        }
    }
}
