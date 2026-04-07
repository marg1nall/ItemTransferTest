using System;
using System.Collections.Generic;
using Board.Api;
using Board.Models;
using Items.Api;
using Items.Models;

namespace Items.Logic
{
    public class RandomItemSpawner
    {
        private readonly IBoardStateRepository _boardRepository;
        private readonly IRandomItemGenerator _itemGenerator;
        private readonly IItemIdGenerator _itemIdGenerator;
        private readonly Random _random = new();

        public RandomItemSpawner(IBoardStateRepository boardRepository, IRandomItemGenerator itemGenerator,
            IItemIdGenerator itemIdGenerator)
        {
            _boardRepository = boardRepository;
            _itemGenerator = itemGenerator;
            _itemIdGenerator = itemIdGenerator;
        }

        public bool TrySpawn(out ItemData item, out BoardPosition position)
        {
            item = null;
            position = default;

            BoardState board = _boardRepository.Get();
            if (!TryFindFreePosition(board, out position))
            {
                return false;
            }

            item = _itemGenerator.CreateRandom(_itemIdGenerator.Next());
            return board.TryPlace(item, position);
        }

        private bool TryFindFreePosition(BoardState board, out BoardPosition position)
        {
            List<BoardPosition> freePositions = new List<BoardPosition>();

            for (var y = 0; y < board.Height; y++)
            {
                for (var x = 0; x < board.Width; x++)
                {
                    var candidate = new BoardPosition(x, y);
                    if (board.IsFree(candidate))
                    {
                        freePositions.Add(candidate);
                    }
                }
            }

            if (freePositions.Count > 0)
            {
                position = freePositions[_random.Next(0, freePositions.Count)];
                return true;
            }

            position = default;
            return false;
        }
    }
}
