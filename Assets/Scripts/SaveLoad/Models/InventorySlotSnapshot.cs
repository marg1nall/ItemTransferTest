using System;

namespace SaveLoad.Models
{
    [Serializable]
    public class InventorySlotSnapshot
    {
        public int Index;
        public bool HasItem;
        public int Id;
        public int Type;
        public int N;
    }
}
