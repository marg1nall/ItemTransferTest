using UnityEngine;

namespace Inventory.Models
{
    [CreateAssetMenu(fileName = "InventoryTabData", menuName = "Data/Inventory Tab Data")]
    public class InventoryTabData : ScriptableObject
    {
        [SerializeField] public int AZeroCapacity;
        [SerializeField] public int APositiveCapacity;
        [SerializeField] public int BCapacity;
        
        [SerializeField] public bool CanExpandAZero;
        [SerializeField] public bool CanExpandAPositive;
        [SerializeField] public bool CanExpandB;
    }
}
