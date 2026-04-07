using Items.Api;
using Items.Logic;
using Items.Models;
using Items.UI;
using UnityEngine;
using Zenject;

namespace Items
{
    public class ItemInstaller : MonoInstaller
    {
        [SerializeField] private SpawnItemButtonView _view;
        [SerializeField] private ItemView _itemPrefab;
        [SerializeField] private Transform _dragLayer;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_itemPrefab).AsSingle();
            Container.BindInstance(_view).AsSingle();
            Container.BindInstance(_dragLayer).WhenInjectedInto<DraggedItemLayerService>();
            Container.Bind<ItemGenerator>().AsSingle();
            Container.Bind<ItemViewFactory>().AsSingle();
            Container.Bind<ItemIdProvider>().AsSingle();
            Container.Bind<IItemIdGenerator>().To<ItemIdProvider>().FromResolve();
            Container.Bind<IRandomItemGenerator>().To<ItemGenerator>().FromResolve();
            Container.Bind<DraggedItemLayerService>().AsSingle();
            Container.Bind<InputDragService>().AsSingle();
            Container.Bind<RandomItemSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<ItemSpawnService>().AsSingle();
        }
    }
}
