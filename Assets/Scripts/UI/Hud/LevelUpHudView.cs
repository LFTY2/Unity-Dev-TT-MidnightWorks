using TMPro;
using UI.Hud.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud
{
    public sealed class LevelUpHudView : BaseHud
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _lvlText;
        [SerializeField] private TMP_Text _rewardText;

        public Button CloseButton => _closeButton;

        internal void SetLvl(int lvl)
        {
            _lvlText.text = lvl.ToString();
        }

        internal void SetReward(int reward)
        {
            _rewardText.text = reward.ToString();
        }
    }
}

