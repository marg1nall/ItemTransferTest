using Inventory.Api;
using Inventory.Models;

namespace Inventory.Logic
{
    public class InventorySlotAdder
    {
        private readonly IInventoryStateRepository _inventoryRepository;

        public InventorySlotAdder(IInventoryStateRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public bool Add(InventoryTabType tabType)
        {
            return _inventoryRepository.Get().TryAddSlot(tabType);
        }
    }
}
