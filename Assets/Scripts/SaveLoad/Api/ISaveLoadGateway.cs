using System.Threading;
using System.Threading.Tasks;
using SaveLoad.Models;

namespace SaveLoad.Api
{
    public interface ISaveLoadGateway
    {
        public Task SaveAsync(GameStateSnapshot snapshot, CancellationToken cancellationToken);
        public Task<GameStateSnapshot> LoadAsync(CancellationToken cancellationToken);
    }
}
