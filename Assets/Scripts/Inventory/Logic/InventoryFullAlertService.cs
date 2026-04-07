using System;
using System.Threading.Tasks;
using Inventory.UI;
using Items.Models;
using UnityEngine;

namespace Inventory.Logic
{
    public class InventoryFullAlertService
    {
        private readonly InventoryDragDropFacade _dragDropFacade;
        private readonly InventoryButtonView _inventoryButtonView;
        private readonly InventoryWindowService _inventoryWindowService;

        public InventoryFullAlertService(InventoryDragDropFacade dragDropFacade,
            InventoryButtonView inventoryButtonView, InventoryWindowService inventoryWindowService)
        {
            _dragDropFacade = dragDropFacade;
            _inventoryButtonView = inventoryButtonView;
            _inventoryWindowService = inventoryWindowService;
        }

        public void Reset()
        {
            _inventoryWindowService.SetInventoryFullAlert(false);
        }

        public bool IsPointerOverButton(Vector2 screenPosition)
        {
            return _inventoryButtonView.ContainsScreenPoint(screenPosition);
        }

        public void UpdateHoverState(Vector2 screenPosition, ItemData item)
        {
            if (!_inventoryButtonView.ContainsScreenPoint(screenPosition) || item == null)
            {
                _inventoryWindowService.SetInventoryFullAlert(false);
                return;
            }

            bool hasSpace = _dragDropFacade.HasSpaceFor(item);
            _inventoryWindowService.SetInventoryFullAlert(!hasSpace);
        }

        public void HandleFailedPlacement(ItemData item)
        {
            if (item == null || _dragDropFacade.HasSpaceFor(item))
            {
                _inventoryWindowService.SetInventoryFullAlert(false);
                return;
            }

            Flash();
        }

        public async void Flash()
        {
            _inventoryWindowService.SetInventoryFullAlert(true);
            await Task.Delay(350);
            _inventoryWindowService.SetInventoryFullAlert(false);
            
        }
    }
}
