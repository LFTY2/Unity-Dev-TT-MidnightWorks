using Core.Inject;
using Core.UI;
using Domain;
using Managers;
using States;
using UnityEngine;
using Utils;

namespace UI.Hud
{
    public sealed class StartHudMediator : Mediator<StartHudView>
    {
        [Inject] private GameConfig _config;
        [Inject] private HudManager _hudManager;
        [Inject] private GameStateManager _gameStateManager;
        protected override void Show()
        {
            var model = GameModel.Load(_config);
            _view.CashText.text = CashUtil.NiceCash(model.Cash);
            _view.ExitButton.onClick.AddListener(Exit);
            _view.SettingsButton.onClick.AddListener(OpenSettings);
            _view.PlayButton.onClick.AddListener(Play);
        }

        protected override void Hide()
        {
            _view.ExitButton.onClick.RemoveListener(Exit);
            _view.SettingsButton.onClick.RemoveListener(OpenSettings);
            _view.PlayButton.onClick.RemoveListener(Play);
        }

        private void Exit()
        {
            Application.Quit();
        }
        

        private void Play()
        {
            _hudManager.HideAdditional<StartHudMediator>();
            _gameStateManager.SwitchToState<GameLoadLevelState>();
        }

        private void OpenSettings()
        {
            _hudManager.ShowAdditional<SettingsHudMediator>();
        }
    }
}