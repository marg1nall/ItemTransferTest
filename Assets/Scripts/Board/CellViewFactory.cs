using Board.UI;
using Common.Api;
using Common.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Board
{
    public class CellViewFactory : IViewFactory
    {
        private readonly Transform _root;
        private readonly CellView _prefab;
        
        public CellViewFactory(GridLayoutGroup root, CellView prefab)
        {
            _root = root.transform;
            _prefab = prefab;
        }
        
        public BaseView Create()
        {
            return Object.Instantiate(_prefab, _root);
        }
    }
}
