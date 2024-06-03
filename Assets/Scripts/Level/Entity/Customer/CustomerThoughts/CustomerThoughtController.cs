using System;
using Core;
using Core.Inject;
using UnityEngine;

namespace Level.Entity.Customer.CustomerThoughts
{
    public class CustomerThoughtController : IDisposable
    {
        [Inject] private ProductPool.ProductPool _productPool;
        [Inject] private Timer _timer;
   
        private CustomerThoughtView _view;
        private ThoughtType _currentThoughtType = ThoughtType.None;
        private Transform _transform;
        
        public CustomerThoughtController(CustomerThoughtView view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _timer.TICK += Tick;
            _transform = _view.transform;
        }
        public void ShowThought(ThoughtType thoughtType)
        {
            if (_currentThoughtType != ThoughtType.None)
            {
                _view.Thoughts[(int)_currentThoughtType].SetActive(false);
            }
            
            _currentThoughtType = thoughtType;
            
            if (_currentThoughtType != ThoughtType.None)
            {
                _view.Thoughts[(int)_currentThoughtType].SetActive(true);
            }
        }

        public void SetNeeds(CustomerNeeds customerNeeds)
        {
            _view.NeedImage.sprite = _productPool.GetProductSprite(customerNeeds.ProductType);
            _view.NeedAmountText.text = customerNeeds.Number.ToString();
        }

        private void Tick()
        {
            _transform.rotation = Quaternion.identity;
        }

        public void Dispose()
        {
            ShowThought(ThoughtType.None);
            _timer.TICK -= Tick;
        }
    }
}