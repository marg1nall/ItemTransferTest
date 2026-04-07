using Zenject;
using Board.UI;
using Items.UI;
using Board.Models;
using UnityEngine;
using System.Collections.Generic;

namespace Board.Logic
{
    public class BoardPlacementService : IInitializable
    {
        private CellView[,] _cellViews;
        private readonly BoardConstructor _boardConstructor;
        private readonly Dictionary<int, ItemView> _itemViews = new();

        public BoardPlacementService(BoardConstructor boardConstructor)
        {
            _boardConstructor = boardConstructor;
        }
        
        public void Initialize()
        {
            _cellViews = _boardConstructor.BuildBoard();
        }
        
        public void PlaceItem(ItemView itemView, BoardPosition pos)
        {
            CellView cell = _cellViews[pos.Y, pos.X];
            itemView.gameObject.SetActive(true);
            itemView.transform.SetParent(cell.transform, false);
            _itemViews[itemView.ItemId] = itemView;
        }

        public ItemView DetachItem(int itemId)
        {
            if (_itemViews.TryGetValue(itemId, out ItemView view))
            {
                _itemViews.Remove(itemId);
                return view;
            }

            return null;
        }

        public void ClearItems()
        {
            foreach (KeyValuePair<int, ItemView> pair in _itemViews)
            {
                if (pair.Value != null)
                {
                    Object.Destroy(pair.Value.gameObject);
                }
            }

            _itemViews.Clear();
        }
    }
}
