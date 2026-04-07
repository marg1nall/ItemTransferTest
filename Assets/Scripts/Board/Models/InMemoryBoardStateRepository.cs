using Board.Api;

namespace Board.Models
{
    public class InMemoryBoardStateRepository : IBoardStateRepository
    {
        private BoardState _boardState;

        public InMemoryBoardStateRepository(BoardState boardState)
        {
            _boardState = boardState;
        }

        public BoardState Get()
        {
            return _boardState;
        }

        public void Set(BoardState boardState)
        {
            _boardState = boardState;
        }
    }
}
