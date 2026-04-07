using System;
using Board.Logic;
using Board.Models;
using Items.Models;
using Items.UI;
using Zenject;

namespace Items.Logic
{
    public class ItemSpawnService : IInitializable, IDisposable
    {
        private readonly SpawnItemButtonView _view;
        private readonly RandomItemSpawner _randomItemSpawner;
        private readonly BoardPlacementService _boardPlacementService;
        private readonly ItemViewFactory _itemFactory;
        private readonly InputDragService _dragService;

        public ItemSpawnService(SpawnItemButtonView view, RandomItemSpawner randomItemSpawner,
            BoardPlacementService boardPlacementService, ItemViewFactory itemFactory, InputDragService dragService)
        {
            _view = view;
            _randomItemSpawner = randomItemSpawner;
            _boardPlacementService = boardPlacementService;
            _itemFactory = itemFactory;
            _dragService = dragService;
        }

        public void Initialize()
        {
            _view.Clicked += OnClicked;
        }

        public void Dispose()
        {
            _view.Clicked -= OnClicked;
        }

        private void OnClicked()
        {
            if (!_randomItemSpawner.TrySpawn(out ItemData item, out BoardPosition position))
            {
                return;
            }

            ItemView view = _itemFactory.Create();
            _dragService.Register(view);
            view.Initialize(item);
            _boardPlacementService.PlaceItem(view, position);
        }
    }
}
