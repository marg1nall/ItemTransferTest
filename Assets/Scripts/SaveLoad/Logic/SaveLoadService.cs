using System;
using System.Threading;
using SaveLoad.UI;
using UnityEngine;
using Zenject;

namespace SaveLoad.Logic
{
    public class SaveLoadService : IInitializable, IDisposable
    {
        private readonly SaveLoadButtonView _saveButton;
        private readonly SaveLoadButtonView _loadButton;
        private readonly GameSaver _gameSaver;
        private readonly GameLoader _gameLoader;
        private readonly GameVisualSyncService _gameVisualSyncService;

        public SaveLoadService(SaveLoadButtonView saveButton, SaveLoadButtonView loadButton,
            GameSaver gameSaver, GameLoader gameLoader, GameVisualSyncService gameVisualSyncService)
        {
            _saveButton = saveButton;
            _loadButton = loadButton;
            _gameSaver = gameSaver;
            _gameLoader = gameLoader;
            _gameVisualSyncService = gameVisualSyncService;
        }

        public void Initialize()
        {
            _saveButton.Clicked += OnSaveClicked;
            _loadButton.Clicked += OnLoadClicked;
        }

        public void Dispose()
        {
            _saveButton.Clicked -= OnSaveClicked;
            _loadButton.Clicked -= OnLoadClicked;
        }

        private async void OnSaveClicked()
        {
            try
            {
                SetButtonsInteractable(false);
                await _gameSaver.SaveAsync(CancellationToken.None);
                SetButtonsInteractable(true);
            }
            catch (Exception e)
            {
                Debug.LogError($"{GetType().Name}: {e.Message}");
            }
        }

        private async void OnLoadClicked()
        {
            try
            {
                SetButtonsInteractable(false);
                bool loaded = await _gameLoader.LoadAsync(CancellationToken.None);
                if (loaded)
                {
                    _gameVisualSyncService.Rebuild();
                }

                SetButtonsInteractable(true);
            }
            catch (Exception e)
            {
                Debug.LogError($"{GetType().Name}: {e.Message}");
            }
        }

        private void SetButtonsInteractable(bool isInteractable)
        {
            _saveButton.SetInteractable(isInteractable);
            _loadButton.SetInteractable(isInteractable);
        }
    }
}
