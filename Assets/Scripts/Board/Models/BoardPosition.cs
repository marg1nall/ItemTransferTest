namespace Board.Models
{
    public readonly struct BoardPosition
    {
        public BoardPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }
}
