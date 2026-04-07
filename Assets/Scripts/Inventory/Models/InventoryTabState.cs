using System.Collections.Generic;
using Items.Models;

namespace Inventory.Models
{
    public class InventoryTabState
    {
        private readonly List<InventorySlotState> _slots = new();

        public InventoryTabState(int initialCapacity, bool canExpand)
        {
            CanExpand = canExpand;

            for (var i = 0; i < initialCapacity; i++)
            {
                _slots.Add(new InventorySlotState { Index = i });
            }
        }

        public bool CanExpand { get; }
        public IReadOnlyList<InventorySlotState> Slots => _slots;

        public bool HasFreeSlot()
        {
            foreach (InventorySlotState slotState in _slots)
            {
                if (slotState.IsEmpty)
                {
                    return true;
                }
            }

            return false;
        }

        public bool TryStore(ItemData item, out int slotIndex)
        {
            slotIndex = -1;

            for (var i = 0; i < _slots.Count; i++)
            {
                if (!_slots[i].IsEmpty)
                {
                    continue;
                }

                _slots[i].Item = item;
                slotIndex = i;
                return true;
            }

            return false;
        }

        public bool TryStoreAt(ItemData item, int slotIndex)
        {
            if (item == null || slotIndex < 0 || slotIndex >= _slots.Count)
            {
                return false;
            }

            if (!_slots[slotIndex].IsEmpty)
            {
                return false;
            }

            _slots[slotIndex].Item = item;
            return true;
        }

        public bool TryTake(int slotIndex, out ItemData item)
        {
            item = null;

            if (slotIndex < 0 || slotIndex >= _slots.Count)
            {
                return false;
            }

            var slot = _slots[slotIndex];
            if (slot.IsEmpty)
            {
                return false;
            }

            item = slot.Item;
            slot.Item = null;
            return true;
        }

        public bool TryAddSlot()
        {
            if (!CanExpand)
            {
                return false;
            }

            _slots.Add(new InventorySlotState { Index = _slots.Count });
            return true;
        }
    }
}
