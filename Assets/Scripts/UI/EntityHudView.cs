using Level.Entity;
using TMPro;
using UI.Hud.Core;
using UnityEngine;
using Utils;

namespace UI
{
    public class EntityHudView : BaseHudWithModel<EntityModel>
    {
        [SerializeField] private GameObject _lockedIcon;
        [SerializeField] private GameObject _purchaseIcon;
        [SerializeField] private GameObject _updateIcon;
        [SerializeField] private TMP_Text _infoText;
        [SerializeField] private TMP_Text _priceText;

        private string _info;
        private string _price;
        
        public void Locked()
        {
            _lockedIcon.SetActive(true);
            _purchaseIcon.SetActive(false);
            _updateIcon.SetActive(false);
        }

        public void ReadyToPuchase()
        {
            _lockedIcon.SetActive(false);
            _purchaseIcon.SetActive(true);
            _updateIcon.SetActive(false);
        }

        public void ReadyToUpdate()
        {
            _lockedIcon.SetActive(false);
            _purchaseIcon.SetActive(false);
            _updateIcon.SetActive(true);
        }

        protected override void OnModelChanged(EntityModel model)
        {
            _info = model.Type.ToString();
            _price = CashUtil.NiceCash(model.PricePurchase);

            if (model.Type == EntityType.Area)
            {
                if (model.IsLocked)
                {
                    _info = "Level " + model.TargetPurchaseValue;
                    _price = "";
                }
                else
                {
                    _info = "New Area";
                }
            }

            if (model.IsPurchased)
            {
                _price = CashUtil.NiceCash(model.PriceNextLvl);

                int lvl = model.LvlNext + 1;
                _info = "Lvl " + lvl;

                if (model.Type == EntityType.CashRegister)
                    _info = "Cashier";
            }

            _infoText.text = _info;
            _priceText.text = _price;
        }
    }
}
