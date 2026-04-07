using System;
using System.Collections.Generic;

namespace SaveLoad.Models
{
    [Serializable]
    public class GameStateSnapshot
    {
        public List<BoardItemSnapshot> BoardItems = new();
        public List<InventoryTabSnapshot> InventoryTabs = new();
        public int NextItemId;
    }
}
