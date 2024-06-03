using Audio;
using Core.Inject;
using Core.UI;
using Level;
using Managers;

namespace UI.Hud
{
    public sealed class GamePlayHudMediator : Mediator<GamePlayHudView>
    {
        [Inject] private LevelView _levelView;
        [Inject] private GameManager _gameManager;
        [Inject] private GameConfig _config;
        [Inject] private HudManager _hudManager;
        [Inject] private AudioManager _audioManager;
        private AudioInstance _switchAudio;

        protected override void Show()
        {

            _view.MaxLevels = _levelView.MaxLevels;
            _view.HotelTypeText.text = _config.Farms[_gameManager.Model.Farm - 1].Label;

            UpdateMaxProgress(_gameManager.Model.LoadLvl());


            _view.Model = _gameManager.Model;
            
            _view.OptionsButton.onClick.AddListener(OnOptionsButtonClick);
            _view.HomeButton.onClick.AddListener(Home);

            _gameManager.LEVEL_CHANGED += UpdateMaxProgress;
            
            if (_switchAudio == null)
            {
                _switchAudio = new AudioInstance(_view.SwitchMenuAudio, AudioType.Sound);
                _audioManager.AssignAudioInstance(_switchAudio);
            }
        }
        

        protected override void Hide()
        {
            _view.OptionsButton.onClick.RemoveListener(OnOptionsButtonClick);
            _view.HomeButton.onClick.RemoveListener(Home);

            _gameManager.LEVEL_CHANGED -= UpdateMaxProgress;
        }

        private void UpdateMaxProgress(int lvl)
        {
            _view.MaxProgress = _levelView.MaxProgress(lvl);
        }
        
        
        private void OnOptionsButtonClick()
        {
            _hudManager.ShowAdditional<SettingsHudMediator>();
            _switchAudio.Play();
        }

        private void Home()
        {
            _hudManager.ShowAdditional<StartHudMediator>();
        }
    }
}