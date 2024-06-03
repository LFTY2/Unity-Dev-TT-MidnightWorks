using Core.Observer;
using TMPro;
using UI.Hud.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class HealthBarModel : Observable
    {
        public int Health;
        public int HealthNominal;
        public Color HealthBarColor;
    }

    public sealed class HealthBarView : BaseHudWithModel<HealthBarModel>
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private GameObject _holder;
        
        protected override void OnApplyModel(HealthBarModel model)
        {
            base.OnApplyModel(model);

            _fillImage.color = model.HealthBarColor;
        }

        protected override void OnModelChanged(HealthBarModel model)
        {
            _holder.SetActive(model.Health < model.HealthNominal && model.Health > 0);
            _fillImage.fillAmount = (float)model.Health / (float)model.HealthNominal;
            _healthText.text = model.Health.ToString();
        }
    }
}

