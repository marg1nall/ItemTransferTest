using System;

namespace SaveLoad.Models
{
    [Serializable]
    public class BoardItemSnapshot
    {
        public int Id;
        public int Type;
        public int N;
        public int X;
        public int Y;
    }
}
