using UnityEngine;

namespace Inventory.Models
{
    [CreateAssetMenu(fileName = "InventoryTabCapacities", menuName = "Data/Inventory Tab Capacities")]
    public class InventoryTabCapacitiesData : ScriptableObject
    {
        [SerializeField] public int AZeroCapacity;
        [SerializeField] public int APositiveCapacity;
        [SerializeField] public int BCapacity;
    }
}
