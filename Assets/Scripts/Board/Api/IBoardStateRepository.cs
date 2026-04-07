using Board.Models;

namespace Board.Api
{
    public interface IBoardStateRepository
    {
        public BoardState Get();
        public void Set(BoardState boardState);
    }
}
