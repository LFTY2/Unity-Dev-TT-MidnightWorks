using TMPro;
using UI.Hud.Core;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Hud
{
    public sealed class StartHudView : BaseHud
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TMP_Text _cashText;

        public Button PlayButton => _playButton;
        public Button SettingsButton => _settingsButton;
        public Button ExitButton => _exitButton;
        public TMP_Text CashText => _cashText;
    }
}