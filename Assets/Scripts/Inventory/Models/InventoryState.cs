using System.Collections.Generic;
using Items.Models;

namespace Inventory.Models
{
    public class InventoryState
    {
        private readonly Dictionary<InventoryTabType, InventoryTabState> _tabs;

        public InventoryState(InventoryTabData inventoryTabData)
        {
            _tabs = new Dictionary<InventoryTabType, InventoryTabState>
            {
                [InventoryTabType.AZero] = new(inventoryTabData.AZeroCapacity, inventoryTabData.CanExpandAZero),
                [InventoryTabType.APositive] = new(inventoryTabData.APositiveCapacity, inventoryTabData.CanExpandAPositive),
                [InventoryTabType.B] = new(inventoryTabData.BCapacity, inventoryTabData.CanExpandB)
            };
        }

        public IReadOnlyDictionary<InventoryTabType, InventoryTabState> Tabs => _tabs;

        public InventoryTabState GetTab(InventoryTabType type)
        {
            return _tabs[type];
        }

        public bool HasSpaceFor(ItemData item)
        {
            return GetTab(item.TargetTab).HasFreeSlot();
        }

        public bool TryStore(ItemData item, out InventoryTabType tabType, out int slotIndex)
        {
            tabType = item.TargetTab;
            return GetTab(tabType).TryStore(item, out slotIndex);
        }

        public bool TryStoreAt(ItemData item, InventoryTabType tabType, int slotIndex)
        {
            if (item == null || item.TargetTab != tabType)
            {
                return false;
            }

            return GetTab(tabType).TryStoreAt(item, slotIndex);
        }

        public bool TryTake(InventoryTabType tabType, int slotIndex, out ItemData item)
        {
            return GetTab(tabType).TryTake(slotIndex, out item);
        }

        public bool TryAddSlot(InventoryTabType tabType)
        {
            return GetTab(tabType).TryAddSlot();
        }
    }
}
