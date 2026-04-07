using Items.UI;
using UnityEngine;

namespace Items.Logic
{
    public class DraggedItemLayerService
    {
        private readonly Transform _dragLayer;

        public DraggedItemLayerService(Transform dragLayer)
        {
            _dragLayer = dragLayer;
        }

        public void Elevate(ItemView view)
        {
            if (_dragLayer == null)
            {
                view.transform.SetAsLastSibling();
                return;
            }

            view.transform.SetParent(_dragLayer, true);
            view.transform.SetAsLastSibling();
        }

        public void Restore(ItemView view, Transform parent, int siblingIndex, Vector3 position)
        {
            view.transform.SetParent(parent, true);
            view.transform.SetSiblingIndex(siblingIndex);
            view.transform.position = position;
        }
    }
}
