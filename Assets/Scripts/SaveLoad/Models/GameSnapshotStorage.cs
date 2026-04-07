using System.Threading;
using System.Threading.Tasks;
using SaveLoad.Api;

namespace SaveLoad.Models
{
    public class GameSnapshotStorage
    {
        private readonly ISaveLoadGateway _gateway;

        public GameSnapshotStorage(ISaveLoadGateway gateway)
        {
            _gateway = gateway;
        }

        public Task SaveAsync(GameStateSnapshot snapshot, CancellationToken cancellationToken)
        {
            return _gateway.SaveAsync(snapshot, cancellationToken);
        }

        public Task<GameStateSnapshot> LoadAsync(CancellationToken cancellationToken)
        {
            return _gateway.LoadAsync(cancellationToken);
        }
    }
}
