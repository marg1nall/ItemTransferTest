using Inventory.Models;

namespace Inventory.Api
{
    public interface IInventoryStateRepository
    {
        public InventoryState Get();
        public void Set(InventoryState inventoryState);
    }
}
