using UI.Hud.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud
{
    public sealed class SettingsHudView : BaseHud
    {
        [SerializeField] private Button _closeButtonBack;
        [SerializeField] private Button _closeButtonX;
        [SerializeField] private ToggleView _toggleSound;
        [SerializeField] private ToggleView _toggleMusic;
        [SerializeField] private AudioSource _switchMenuAudio;

        public Button CloseButtonBack => _closeButtonBack;
        public Button CloseButtonX => _closeButtonX;
        public ToggleView ToggleSound => _toggleSound;
        public ToggleView ToggleMusic => _toggleMusic;
        public AudioSource SwitchMenuAudio => _switchMenuAudio;
        
    }
}