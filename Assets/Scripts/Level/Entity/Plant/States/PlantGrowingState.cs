using UnityEngine;

namespace Level.Entity.Plant.States
{
    public sealed class PlantGrowingState : PlantState
    {
        private float _timeToGrow;
        public override void Initialize()
        {
            _plant.View.GrowTransform.gameObject.SetActive(true);
            _plant.View.GrowTransform.localScale = Vector3.zero;
            _timeToGrow = _plant.View.Config.GrowTime;
            _plant.View.FillBar.Holder.SetActive(true);
            _timer.TICK += OnTick;
        }

        private void OnTick()
        {
            _timeToGrow -= Time.deltaTime;
            float growValue = 1 - _timeToGrow / _plant.View.Config.GrowTime;
            _plant.View.FillBar.FillImage.fillAmount = growValue;
            _plant.View.GrowTransform.localScale = Vector3.one * growValue;
            if (_timeToGrow <= 0)
            {
                _plant.SwitchToState(new PlantReadyToCollectState());
            }
        }

        public override void Dispose()
        {
            _plant.View.FillBar.Holder.SetActive(false);
            _plant.View.GrowTransform.gameObject.SetActive(false);
            _timer.TICK -= OnTick;
        }
    }
}