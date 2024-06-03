using Domain;
using TMPro;
using UI.Hud.Core;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Hud
{
    public sealed class GamePlayHudView : BaseHudWithModel<GameModel>
    {
        [SerializeField] private TMP_Text _cashText;
        [SerializeField] private TMP_Text _lvlText;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private TMP_Text _hotelTypeText;
        [SerializeField] private Image _progressFillImage;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _homeButton;
        [SerializeField] private AudioSource _switchMenuAudio;
        
        public Button OptionsButton => _optionsButton;
        public Button HomeButton => _homeButton;
        public TMP_Text HotelTypeText => _hotelTypeText;
        public AudioSource SwitchMenuAudio => _switchMenuAudio;

        public int MaxProgress { get; internal set; }
        public int MaxLevels { get; internal set; }

        protected override void OnModelChanged(GameModel model)
        {
            _cashText.text = CashUtil.NiceCash(model.Cash);

            int lvl = model.LoadLvl();
            _lvlText.text = lvl.ToString();

            int progress = model.LoadProgress();
            string progressText = progress.ToString() + "/" + MaxProgress;
            float fillAmount = (float)progress / MaxProgress;

            if (lvl >= MaxLevels)
            {
                progressText = "Max";
                fillAmount = 1f;
            }

            _progressText.text = progressText;
            _progressFillImage.fillAmount = fillAmount;
        }
    }
}