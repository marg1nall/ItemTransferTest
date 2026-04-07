using System;
using Common.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class InventoryButtonView : BaseView
    {
        public event Action Clicked;
        
        [SerializeField] private Button _button;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _alertColor;
        
        protected override void Awake()
        {
            base.Awake();
            _button.onClick.AddListener(() => Clicked?.Invoke());
        }

        public void SetAlertState(bool isAlert)
        {
            if (Image != null)
            {
                Image.color = isAlert ? _alertColor : _normalColor;
            }
        }

        public bool ContainsScreenPoint(Vector2 screenPoint)
        {
            var rectTransform = transform as RectTransform;
            if (rectTransform == null)
            {
                return false;
            }

            // todo сейчас применимо только к оверлей канвасу
            return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPoint, null);
        }
    }
}
