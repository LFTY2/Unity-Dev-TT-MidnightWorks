using Core;
using Core.Inject;
using UnityEngine;

namespace Level.Cash.States
{
    public abstract class CashFlyToPositionState : CashState
    {
        private const float _flyTime = .3f;

        [Inject] protected Timer _timer;

        protected Vector3 _startPosition;
        protected Vector3 _endPosition;

        private float _timeElapsed;

        public override void Initialize()
        {
            _startPosition = _cash.View.transform.position;

            _timer.TICK += OnTICK;
        }

        public override void Dispose()
        {
            _timer.TICK -= OnTICK;
        }

        private void OnTICK()
        {
            _cash.View.transform.position = Vector3.Lerp(_startPosition, _endPosition, _timeElapsed / _flyTime);
            _timeElapsed += Time.deltaTime;

            float distance = Vector3.Distance(_cash.View.transform.position, _endPosition);
            if (distance > 0.05f) return;

            _cash.View.transform.position = _endPosition;

            OnEnd();
        }

        public abstract void OnEnd();
    }
}