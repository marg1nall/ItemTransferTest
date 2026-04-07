using System;
using Common.UI;
using Inventory.Models;
using Items.Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Items.UI
{
    public class ItemView : BaseView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<ItemView> DragStarted;
        public event Action<ItemView, Vector2> Dragged;
        public event Action<ItemView> DragEnded;
        public event Action<ItemView> Destroyed;

        public int ItemId { get; private set; }
        
        public void Initialize(ItemData item)
        {
            ItemId = item.Id;
            Color color = GetColor(item);
            SetColor(color);
        }

        private Color GetColor(ItemData item)
        {
            return item.TargetTab switch
            {
                InventoryTabType.APositive => new Color(1f, 0.78f, 0.2f),
                InventoryTabType.AZero => new Color(0.6f, 0.85f, 0.2f),
                _ => Color.blue
            };
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            DragStarted?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Dragged?.Invoke(this, eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DragEnded?.Invoke(this);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
