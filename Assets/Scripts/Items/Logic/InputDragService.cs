using Board.Api;
using Board.Logic;
using Board.Models;
using Inventory.Logic;
using Inventory.UI;
using Items.Models;
using Items.UI;
using Inventory.Models;
using UnityEngine;

namespace Items.Logic
{
    public class InputDragService
    {
        private readonly IBoardStateRepository _boardRepository;
        private readonly InventoryDragDropFacade _dragDropFacade;
        private readonly InventoryFullAlertService _inventoryFullAlertService;
        private readonly InventoryWindowService _inventoryWindowService;
        private readonly InventoryWindowView _inventoryWindowView;
        private readonly BoardPlacementService _boardPlacementService;
        private readonly DraggedItemLayerService _draggedItemLayerService;

        private Transform _startParent;
        private Vector3 _startPosition;
        private int _startSiblingIndex;

        public InputDragService(IBoardStateRepository boardRepository, InventoryDragDropFacade dragDropFacade,
            InventoryFullAlertService inventoryFullAlertService, InventoryWindowService inventoryWindowService,
            InventoryWindowView inventoryWindowView, BoardPlacementService boardPlacementService,
            DraggedItemLayerService draggedItemLayerService)
        {
            _boardRepository = boardRepository;
            _dragDropFacade = dragDropFacade;
            _inventoryFullAlertService = inventoryFullAlertService;
            _inventoryWindowService = inventoryWindowService;
            _inventoryWindowView = inventoryWindowView;
            _boardPlacementService = boardPlacementService;
            _draggedItemLayerService = draggedItemLayerService;
        }

        public void Register(ItemView view)
        {
            view.DragStarted += OnStart;
            view.Dragged += OnDrag;
            view.DragEnded += OnEnd;
            view.Destroyed += OnDestroyed;
        }

        private void OnStart(ItemView view)
        {
            _inventoryFullAlertService.Reset();
            _startParent = view.transform.parent;
            _startPosition = view.transform.position;
            _startSiblingIndex = view.transform.GetSiblingIndex();
            _draggedItemLayerService.Elevate(view);
        }

        private void OnDrag(ItemView view, Vector2 pos)
        {
            view.transform.position = pos;
            UpdateInventoryButtonAlert(view, pos);
        }

        private void OnEnd(ItemView view)
        {
            _inventoryFullAlertService.Reset();

            BoardState board = _boardRepository.Get();
            if (!board.TryFindPositionByItemId(view.ItemId, out BoardPosition sourcePosition) ||
                !board.TryGet(sourcePosition, out ItemData draggedItem))
            {
                ReturnToStart(view);
                return;
            }

            Vector2 screenPosition = view.transform.position;

            if (_inventoryWindowView.IsVisible &&
                _inventoryWindowView.TryGetSlotAtScreenPoint(screenPosition, out InventorySlotView slotView))
            {
                BoardToInventoryTransferResult slotResult = _dragDropFacade.TryMoveFromBoardToSpecificSlot(sourcePosition, slotView.TabType, slotView.SlotIndex);
                if (slotResult.IsSuccess)
                {
                    _inventoryFullAlertService.Reset();
                    _inventoryWindowService.OpenTab(slotResult.TabType);
                    MoveViewToInventory(view, slotResult);
                    return;
                }

                _inventoryFullAlertService.HandleFailedPlacement(draggedItem);
                ReturnToStart(view);
                return;
            }

            if (IsOverInventoryButton(screenPosition))
            {
                BoardToInventoryTransferResult iconResult = _dragDropFacade.TryMoveFromBoardToFirstAvailableSlot(sourcePosition);
                if (iconResult.IsSuccess)
                {
                    _inventoryFullAlertService.Reset();
                    _inventoryWindowService.OpenTab(iconResult.TabType);
                    MoveViewToInventory(view, iconResult);
                    return;
                }

                if (iconResult.IsInventoryFull)
                {
                    _inventoryFullAlertService.Flash();
                }

                ReturnToStart(view);
                return;
            }

            if (_inventoryWindowView.ContainsScreenPoint(screenPosition))
            {
                BoardToInventoryTransferResult windowResult =
                    _dragDropFacade.TryMoveFromBoardToFirstAvailableSlot(sourcePosition);
                if (windowResult.IsSuccess)
                {
                    _inventoryFullAlertService.Reset();
                    _inventoryWindowService.OpenTab(windowResult.TabType);
                    MoveViewToInventory(view, windowResult);
                    return;
                }

                _inventoryFullAlertService.HandleFailedPlacement(draggedItem);
                ReturnToStart(view);
                return;
            }

            _inventoryFullAlertService.Reset();
            ReturnToStart(view);
        }

        private void ReturnToStart(ItemView view)
        {
            _draggedItemLayerService.Restore(view, _startParent, _startSiblingIndex, _startPosition);
        }

        private void MoveViewToInventory(ItemView view, BoardToInventoryTransferResult result)
        {
            ItemView movedView = _boardPlacementService.DetachItem(view.ItemId);
            if (movedView != null)
            {
                _inventoryWindowService.PlaceItemViewInSlot(movedView, result.Item, result.TabType, result.SlotIndex);
            }
        }

        private void UpdateInventoryButtonAlert(ItemView view, Vector2 screenPosition)
        {
            if (!TryGetDraggedBoardItem(view, out ItemData item))
            {
                _inventoryFullAlertService.Reset();
                return;
            }

            _inventoryFullAlertService.UpdateHoverState(screenPosition, item);
        }

        private bool TryGetDraggedBoardItem(ItemView view, out ItemData item)
        {
            item = null;

            BoardState board = _boardRepository.Get();
            if (!board.TryFindPositionByItemId(view.ItemId, out BoardPosition position))
            {
                return false;
            }

            return board.TryGet(position, out item);
        }

        private bool IsOverInventoryButton(Vector2 screenPosition)
        {
            return _inventoryFullAlertService.IsPointerOverButton(screenPosition);
        }
        
        private void UnRegister(ItemView view)
        {
            view.DragStarted -= OnStart;
            view.Dragged -= OnDrag;
            view.DragEnded -= OnEnd;
            view.Destroyed -= OnDestroyed;
        }

        private void OnDestroyed(ItemView view)
        {
            UnRegister(view);
        }
    }
}
