using System;
using System.Collections.Generic;

namespace SaveLoad.Models
{
    [Serializable]
    public class InventoryTabSnapshot
    {
        public int TabType;
        public List<InventorySlotSnapshot> Slots = new();
    }
}
