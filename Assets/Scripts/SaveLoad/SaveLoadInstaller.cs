using SaveLoad.Api;
using SaveLoad.Logic;
using SaveLoad.Models;
using SaveLoad.UI;
using UnityEngine;
using Zenject;

namespace SaveLoad
{
    public class SaveLoadInstaller : MonoInstaller
    {
        private const string SaveFileName = "save.json";

        [SerializeField] private SaveLoadButtonView _saveButton;
        [SerializeField] private SaveLoadButtonView _loadButton;

        public override void InstallBindings()
        {
            Container.Bind<SaveLoadStateGuard>().AsSingle();
            Container.Bind<GameSnapshotStorage>().AsSingle();
            Container.Bind<GameStateSnapshotFactory>().AsSingle();
            Container.Bind<GameStateRestorer>().AsSingle();
            Container.Bind<GameVisualSyncService>().AsSingle();
            Container.Bind<GameSaver>().AsSingle();
            Container.Bind<GameLoader>().AsSingle();
            Container.Bind<ISaveLoadGateway>()
                .To<JsonFileSaveLoadGateway>()
                .AsSingle()
                .WithArguments(SaveFileName);
            Container.BindInterfacesAndSelfTo<SaveLoadService>()
                .AsSingle()
                .WithArguments(_saveButton, _loadButton);
        }
    }
}
