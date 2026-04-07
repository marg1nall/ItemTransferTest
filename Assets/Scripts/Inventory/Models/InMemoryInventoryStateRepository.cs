using Inventory.Api;

namespace Inventory.Models
{
    public class InMemoryInventoryStateRepository : IInventoryStateRepository
    {
        private InventoryState _inventoryState;

        public InMemoryInventoryStateRepository(InventoryState inventoryState)
        {
            _inventoryState = inventoryState;
        }

        public InventoryState Get()
        {
            return _inventoryState;
        }

        public void Set(InventoryState inventoryState)
        {
            _inventoryState = inventoryState;
        }
    }
}
