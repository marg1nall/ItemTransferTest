using System.Threading;
using System.Threading.Tasks;
using SaveLoad.Models;

namespace SaveLoad.Logic
{
    public class GameLoader
    {
        private readonly GameStateRestorer _restorer;
        private readonly SaveLoadStateGuard _guard;
        private readonly GameSnapshotStorage _storage;

        public GameLoader(GameSnapshotStorage storage, GameStateRestorer restorer, SaveLoadStateGuard guard)
        {
            _storage = storage;
            _restorer = restorer;
            _guard = guard;
        }

        public async Task<bool> LoadAsync(CancellationToken cancellationToken)
        {
            if (!_guard.TryBegin())
            {
                return false;
            }

            try
            {
                GameStateSnapshot snapshot = await _storage.LoadAsync(cancellationToken);
                if (snapshot == null)
                {
                    return false;
                }

                _restorer.Restore(snapshot);
                return true;
            }
            finally
            {
                _guard.End();
            }
        }
    }
}
