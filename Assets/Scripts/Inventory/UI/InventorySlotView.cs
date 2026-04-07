using System;
using Inventory.Models;
using Items.Models;
using Items.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class InventorySlotView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Transform _itemRoot;
        private ItemData _item;
        private ItemView _itemView;
        private bool HasItem => _item != null;
        
        public event Action<InventoryTabType, int> Clicked;
        
        public InventoryTabType TabType { get; private set; }
        public int SlotIndex { get; private set; }

        public void Bind(InventoryTabType tabType, int slotIndex, ItemData item)
        {
            TabType = tabType;
            SlotIndex = slotIndex;
            _item = item;
        }

        public void AttachItemView(ItemView itemView, ItemData item)
        {
            _itemView = itemView;
            _item = item;

            if (_itemView == null || _itemRoot == null)
            {
                return;
            }

            _itemView.gameObject.SetActive(true);
            _itemView.transform.SetParent(_itemRoot, false);
            _itemView.transform.localPosition = Vector3.zero;
            _itemView.transform.localScale = Vector3.one;
        }

        public ItemView DetachItemView()
        {
            ItemView itemView = _itemView;
            _itemView = null;
            _item = null;
            return itemView;
        }

        public void ClearItemView()
        {
            if (_itemView != null)
            {
                Destroy(_itemView.gameObject);
            }

            _itemView = null;
            _item = null;
        }

        public bool ContainsScreenPoint(Vector2 screenPoint)
        {
            RectTransform rectTransform = transform as RectTransform;
            if (rectTransform == null || !gameObject.activeInHierarchy)
            {
                return false;
            }

            // todo сейчас применимо только к оверлей канвасу 
            return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPoint, null);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!HasItem)
            {
                return;
            }

            Clicked?.Invoke(TabType, SlotIndex);
        }
    }
}
