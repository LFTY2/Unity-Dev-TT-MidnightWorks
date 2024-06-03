using Audio;
using Core.Inject;
using Managers;
using UI;
using UI.Hud;

namespace States
{
    public class GameInitializeState : GameState
    {
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private Context _context;
        [Inject] private HudManager _hudManager;
        [Inject] private AudioManager _audioManager;
        [Inject] private GameView _gameView;

        public override void Initialize()
        {
            var config = GameConfig.Load();

            _context.Install(config);
            _context.ApplyInstall();
            
            _hudManager.ShowAdditional<StartHudMediator>();
            
            AudioInstance backgroundMusic = new AudioInstance(_gameView._backgroundMusic, Audio.AudioType.Music);
            _audioManager.AssignAudioInstance(backgroundMusic);
            backgroundMusic.Play();
        }

        public override void Dispose()
        {
        }
    }
}