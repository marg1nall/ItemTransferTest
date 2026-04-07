using Board.Models;
using Board.UI;
using Common.Api;
using UnityEngine;
using UnityEngine.UI;

namespace Board.Logic
{
    public class BoardConstructor
    {
        private readonly BoardData _boardData;
        private readonly IViewFactory _viewFactory;
        
        public BoardConstructor(IViewFactory viewFactory, BoardData boardData, GridLayoutGroup gridRoot)
        {
            _viewFactory = viewFactory;
            _boardData = boardData;

            ConfigureGrid(gridRoot, boardData);
        }

        private void ConfigureGrid(GridLayoutGroup gridRoot, BoardData data)
        {
            gridRoot.cellSize = data.CellSize;
            gridRoot.constraint = data.Constraint;
            gridRoot.constraintCount = data.Height;
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
                    cellView.SetColor(color);
                    board[y, x] = cellView;
                }
            }
            
            return board;
        }
        
    }
}
