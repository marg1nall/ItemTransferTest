using System.Threading;
using System.Threading.Tasks;
using SaveLoad.Models;

namespace SaveLoad.Logic
{
    public class GameSaver
    {
        private readonly GameSnapshotStorage _storage;
        private readonly GameStateSnapshotFactory _snapshotFactory;
        private readonly SaveLoadStateGuard _guard;

        public GameSaver(GameSnapshotStorage storage, GameStateSnapshotFactory snapshotFactory, SaveLoadStateGuard guard)
        {
            _storage = storage;
            _snapshotFactory = snapshotFactory;
            _guard = guard;
        }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken)
        {
            if (!_guard.TryBegin())
            {
                return false;
            }

            try
            {
                await _storage.SaveAsync(_snapshotFactory.Create(), cancellationToken);
                return true;
            }
            finally
            {
                _guard.End();
            }
        }
    }
}
