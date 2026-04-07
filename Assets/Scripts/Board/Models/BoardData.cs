using UnityEngine;
using UnityEngine.UI;

namespace Board.Models
{
    [CreateAssetMenu(fileName = "BoardData", menuName = "Data/BoardData")]
    public class BoardData : ScriptableObject
    {
        public int Width;
        public int Height;
        public GridLayoutGroup.Constraint Constraint;
        public Vector2 CellSize; 
        public Color EvenCellColor;
        public Color OddCellColor;
    }
}
