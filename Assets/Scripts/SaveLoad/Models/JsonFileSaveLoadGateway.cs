using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SaveLoad.Api;
using UnityEngine;

namespace SaveLoad.Models
{
    public class JsonFileSaveLoadGateway : ISaveLoadGateway
    {
        private readonly string _path;

        public JsonFileSaveLoadGateway(string fileName)
        {
            _path = Path.Combine(Application.persistentDataPath, fileName);
        }

        public async Task SaveAsync(GameStateSnapshot snapshot, CancellationToken cancellationToken)
        {
            string json = JsonUtility.ToJson(snapshot, true);
            await File.WriteAllTextAsync(_path, json, cancellationToken);
        }

        public async Task<GameStateSnapshot> LoadAsync(CancellationToken cancellationToken)
        {
            if (!File.Exists(_path))
            {
                return null;
            }

            string json = await File.ReadAllTextAsync(_path, cancellationToken);
            return JsonUtility.FromJson<GameStateSnapshot>(json);
        }
    }
}
