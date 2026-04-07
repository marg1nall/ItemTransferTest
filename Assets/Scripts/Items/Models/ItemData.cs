using Inventory.Models;

namespace Items.Models
{
    public class ItemData
    {
        public ItemData(int id, ItemType type, int n)
        {
            Id = id;
            Type = type;
            N = n;
        }

        public int Id { get; }
        public ItemType Type { get; }
        public int N { get; }

        public ItemCategory Category
        {
            get
            {
                if (Type == ItemType.B)
                {
                    return ItemCategory.B;
                }

                if (N == 0)
                {
                    return ItemCategory.AZero;
                }

                return ItemCategory.APositive;
            }
        }

        public InventoryTabType TargetTab
        {
            get
            {
                if (Type == ItemType.B)
                {
                    return InventoryTabType.B;
                }

                if (N == 0)
                {
                    return InventoryTabType.AZero;
                }

                return InventoryTabType.APositive;
            }
        }
    }
}
