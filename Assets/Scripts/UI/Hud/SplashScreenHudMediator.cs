using Core;
using Core.Inject;
using Core.UI;
using Domain;
using UnityEngine;

namespace UI.Hud
{
    public sealed class SplashScreenHudMediator : Mediator<SplashScreenHudView>
    {
        [Inject] private GameConfig _config;
        [Inject] private Timer _timer;

        private float _duration;
        private float _elapsed;
        private int _farm;

        protected override void Show()
        {
            _duration = _config.SplashScreenDuration;
            UpdateBar();

            _timer.TICK += OnTICK;
        }

        protected override void Hide()
        {
            _timer.TICK -= OnTICK;
        }

        private void OnTICK()
        {
            UpdateBar();

            _elapsed += Time.deltaTime;

            if (_elapsed >= _duration)
                InternalHide();
        }

        void UpdateBar()
        {
            float value = _elapsed / _duration;
            _view.FillBarImage.fillAmount = value;
        }
        
    }
}