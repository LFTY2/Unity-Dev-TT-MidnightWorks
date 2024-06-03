using Audio;
using Core.Inject;
using Level;
using Level.Core;
using Managers;
using UI;
using UI.Hud;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace Modules.UISpritesModule
{
    public sealed class AddLevelModule : Module<AddLevelModuleView>
    {
        [Inject] private GameManager _gameManager;
        [Inject] private GameView _gameView;
        [Inject] private LevelView _levelView;
        [Inject] private HudManager _hudManager;
        [Inject] private AudioManager _audioManager;
        private AudioInstance _scoreAddedSound;

        public AddLevelModule(AddLevelModuleView view) : base(view)
        {
        }

        public override void Initialize()
        {
            _gameManager.ADD_GAME_PROGRESS += AddGameProgress;
            _scoreAddedSound = new AudioInstance(_view.ScoreAdded, AudioType.Sound);
            _audioManager.AssignAudioInstance(_scoreAddedSound);
        }

        public override void Dispose()
        {
            _gameManager.ADD_GAME_PROGRESS -= AddGameProgress;
        }

        private void AddGameProgress(Vector3 startPosition, int progressDelta)
        {
            if (progressDelta <= 0) return;
            for (int i = 0; i < progressDelta; i++)
            {
                AddScore();
            }
        }

        private void AddScore()
        {
            int progress = _gameManager.Model.LoadProgress();
            progress++;
            _gameManager.Model.SaveProgress(progress);
            _gameManager.FireProgressChanged(progress);
            
            _scoreAddedSound.Play();

            CheckIfReachNewLvl(progress);

            _gameManager.Model.Save();
            _gameManager.Model.SetChanged();
        }

        void CheckIfReachNewLvl(int progress)
        {
            if (progress >= _levelView.MaxProgress(_gameManager.Model.LoadLvl()))
            {
                int lvl = _gameManager.Model.LoadLvl();
                lvl++;

                if (lvl <= _levelView.MaxLevels)
                {
                    _gameManager.Model.SaveLvl(lvl);
                    _gameManager.FireLevelChanged(_gameManager.Model.LoadLvl());

                    _hudManager.ShowAdditional<LevelUpHudMediator>();
                }
            }
        }

  
    }
}


