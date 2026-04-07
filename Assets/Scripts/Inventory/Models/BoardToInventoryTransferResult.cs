using Items.Models;

namespace Inventory.Models
{
    public class BoardToInventoryTransferResult
    {
        private BoardToInventoryTransferResult(bool isSuccess, bool inventoryFull, 
            ItemData item,
            InventoryTabType tabType,
            int slotIndex)
        {
            IsSuccess = isSuccess;
            IsInventoryFull = inventoryFull;
            Item = item;
            TabType = tabType;
            SlotIndex = slotIndex;
        }

        public bool IsSuccess { get; }
        public bool IsInventoryFull { get; }
        public ItemData Item { get; }
        public InventoryTabType TabType { get; }
        public int SlotIndex { get; }

        public static BoardToInventoryTransferResult Success(ItemData item, InventoryTabType tabType, int slotIndex)
        {
            return new BoardToInventoryTransferResult(true, false, item, tabType, slotIndex);
        }

        public static BoardToInventoryTransferResult InventoryFull(InventoryTabType tabType)
        {
            return new BoardToInventoryTransferResult(false, true, null, tabType, -1);
        }

        public static BoardToInventoryTransferResult ItemMissing()
        {
            return new BoardToInventoryTransferResult(false, false, null, default, -1);
        }
    }
}
