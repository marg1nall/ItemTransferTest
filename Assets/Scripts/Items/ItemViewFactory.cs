using Items.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class ItemViewFactory
    {
        private readonly Transform _root;
        private readonly ItemView _prefab;

        public ItemViewFactory(GridLayoutGroup root, ItemView prefab)
        {
            _root = root.transform;
            _prefab = prefab;
        }

        public ItemView Create()
        {
            return Object.Instantiate(_prefab, _root);
        }
    }
}
