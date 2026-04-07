using Items.Models;

namespace Inventory.Models
{
    public class InventorySlotState
    {
        public int Index { get; set; }
        public ItemData Item { get; set; }
        public bool IsEmpty => Item == null;
    }
}
