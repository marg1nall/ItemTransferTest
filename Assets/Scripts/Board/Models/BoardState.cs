using Items.Models;
using System.Collections.Generic;

namespace Board.Models
{
    public class BoardState
    {
        private readonly ItemData[,] _items;

        public BoardState(int width, int height)
        {
            Width = width;
            Height = height;
            _items = new ItemData[height, width];
        }

        public int Width { get; }
        public int Height { get; }

        public bool IsFree(BoardPosition position)
        {
            return IsInside(position) && _items[position.Y, position.X] == null;
        }

        public bool TryPlace(ItemData item, BoardPosition position)
        {
            if (item == null || !IsFree(position))
            {
                return false;
            }

            _items[position.Y, position.X] = item;
            return true;
        }

        public bool TryRemove(BoardPosition position, out ItemData item)
        {
            item = null;

            if (!IsInside(position))
            {
                return false;
            }

            item = _items[position.Y, position.X];
            
            if (item == null)
            {
                return false;
            }

            _items[position.Y, position.X] = null;
            return true;
        }

        public bool TryGet(BoardPosition position, out ItemData item)
        {
            item = null;

            if (!IsInside(position))
            {
                return false;
            }

            item = _items[position.Y, position.X];
            return item != null;
        }

        public IEnumerable<(BoardPosition position, ItemData item)> EnumerateItems()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var item = _items[y, x];
                    if (item != null)
                    {
                        yield return (new BoardPosition(x, y), item);
                    }
                }
            }
        }

        public bool TryFindPositionByItemId(int itemId, out BoardPosition position)
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var item = _items[y, x];
                    if (item != null && item.Id == itemId)
                    {
                        position = new BoardPosition(x, y);
                        return true;
                    }
                }
            }

            position = default;
            return false;
        }
        
        private bool IsInside(BoardPosition position)
        {
            return position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;
        }
    }
}
