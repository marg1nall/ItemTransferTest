using Inventory.Api;
using Inventory.Logic;
using Inventory.Models;
using Inventory.UI;
using UnityEngine;
using Zenject;

namespace Inventory
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private InventoryButtonView _inventoryButtonView;
        [SerializeField] private InventoryWindowView _inventoryWindowView;
        [SerializeField] private InventoryTabCapacitiesData _tabCapacitiesData;

        public override void InstallBindings()
        {
            Container.BindInstance(_inventoryButtonView).AsSingle();
            Container.BindInstance(_inventoryWindowView).AsSingle();
            Container.BindInstance(_tabCapacitiesData).AsSingle();
            Container.Bind<InventoryState>()
                .AsSingle()
                .WithArguments(_tabCapacitiesData.AZeroCapacity, _tabCapacitiesData.APositiveCapacity, _tabCapacitiesData.BCapacity);
            Container.Bind<IInventoryStateRepository>().To<InMemoryInventoryStateRepository>().AsSingle();
            Container.Bind<BoardToInventoryTransfer>().AsSingle();
            Container.Bind<InventoryToBoardTransfer>().AsSingle();
            Container.Bind<InventorySlotAdder>().AsSingle();
            Container.Bind<InventoryFullAlertService>().AsSingle();
            Container.BindInterfacesAndSelfTo<InventoryWindowService>().AsSingle();
            Container.Bind<InventoryDragDropFacade>().AsSingle();
        }
    }
}
