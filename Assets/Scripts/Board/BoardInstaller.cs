using Board.Api;
using Board.Logic;
using Board.Models;
using Board.UI;
using Common.Api;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Board
{
    public class BoardInstaller : MonoInstaller
    {
        [SerializeField] private GridLayoutGroup _gridRoot;
        [SerializeField] private CellView _cellPrefab;
        [SerializeField] private BoardData boardData;
        
        public override void InstallBindings()
        {
            Container.BindInstance(boardData).AsSingle();
            Container.BindInstance(_gridRoot).AsSingle();
            Container.BindInstance(_cellPrefab).AsSingle();
            Container.Bind<BoardState>().AsSingle().WithArguments(boardData.Width, boardData.Height);;
            Container.Bind<IBoardStateRepository>().To<InMemoryBoardStateRepository>().AsSingle();
            Container.Bind<BoardConstructor>().AsSingle();
            Container.BindInterfacesAndSelfTo<BoardPlacementService>().AsSingle();
            Container.Bind<IViewFactory>().To<CellViewFactory>().AsSingle();
        }
    }
}
