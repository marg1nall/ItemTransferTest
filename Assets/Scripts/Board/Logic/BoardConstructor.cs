using Board.Models;
using Board.UI;
using Common.Api;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Board.Logic
{
    public class BoardConstructor : IInitializable
    {
        private readonly BoardData _boardData;
        private readonly GridLayoutGroup _gridRoot;
        private readonly IViewFactory _viewFactory;
        
        public BoardConstructor(IViewFactory viewFactory, BoardData boardData, GridLayoutGroup gridRoot)
        {
            _viewFactory = viewFactory;
            _boardData = boardData;
            _gridRoot =  gridRoot;
        }

        public void Initialize()
        {
            _gridRoot.cellSize = _boardData.CellSize;
            _gridRoot.constraint = _boardData.Constraint;
            _gridRoot.constraintCount = _boardData.Height;
        }

        public CellView[,] BuildBoard()
        {
            CellView[,] board = new CellView[_boardData.Height, _boardData.Width];
            
            for (int y = 0; y < _boardData.Height; y++)
            {
                for (int x = 0; x < _boardData.Width; x++)
                {
                    CellView cellView = _viewFactory.Create() as CellView;
                    Color color = (x + y) % 2 == 0 ? _boardData.EvenCellColor : _boardData.OddCellColor;
                    
                    if (cellView != null)
                    {
                        cellView.SetColor(color);
                        board[y, x] = cellView;
                    }
                }
            }
            
            return board;
        }
    }
}
