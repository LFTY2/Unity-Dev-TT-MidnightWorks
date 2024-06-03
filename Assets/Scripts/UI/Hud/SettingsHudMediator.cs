using Audio;
using Core.UI;
using Core.Inject;
using Managers;
using UnityEngine;
using AudioType = Audio.AudioType;

namespace UI.Hud
{
    public sealed class SettingsHudMediator : Mediator<SettingsHudView>
    {
        [Inject] private HudManager _hudManager;
        [Inject] private AudioManager _audioManager;
        private AudioInstance _switchAudio;

        protected override void Show()
        {
            _view.CloseButtonBack.onClick.AddListener(OnCloseButtonClick);
            _view.CloseButtonX.onClick.AddListener(OnCloseButtonClick);
            
            _view.ToggleSound.OnToggleSwitch += OnSwitchSound;
            _view.ToggleMusic.OnToggleSwitch += OnSwitchMusic;
            _view.ToggleSound.Initialize(_audioManager.IsAudioActive[(int)AudioType.Sound]);
            _view.ToggleMusic.Initialize(_audioManager.IsAudioActive[(int)AudioType.Music]);

            _switchAudio ??= new AudioInstance(_view.SwitchMenuAudio, AudioType.Sound);
            _audioManager.AssignAudioInstance(_switchAudio);
        }

        protected override void Hide()
        {
            _view.CloseButtonBack.onClick.RemoveListener(OnCloseButtonClick);
            _view.CloseButtonX.onClick.RemoveListener(OnCloseButtonClick);
            _view.ToggleSound.OnToggleSwitch -= OnSwitchSound;
            _view.ToggleMusic.OnToggleSwitch -= OnSwitchMusic;
            _audioManager.RemoveAudioInstance(_switchAudio);
        }

        private void OnCloseButtonClick()
        {
            _switchAudio.Play();
            _hudManager.HideAdditional<SettingsHudMediator>();
        }
        
        private void OnSwitchSound()
        {
            _switchAudio.Play();
            _audioManager.SwitchAudio(AudioType.Sound);
        }
        
        private void OnSwitchMusic()
        {
            _switchAudio.Play();
            _audioManager.SwitchAudio(AudioType.Music);
        }
    }
}