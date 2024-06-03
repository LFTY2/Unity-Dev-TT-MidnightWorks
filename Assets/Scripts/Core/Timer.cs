using System;
using UnityEngine;

namespace Core
{
    public sealed class Timer
    {
        private readonly OneListener _tickListener = new();
        public event Action TICK
        {
            add => _tickListener.Add(value);
            remove => _tickListener.Remove(value);
        }

        private readonly OneListener _postTickListener = new();

        private readonly OneListener _fixedTickListener = new();

        private readonly OneListener _oneSecondTickListener = new();

        public event Action ONE_SECOND_TICK
        {
            add => _oneSecondTickListener.Add(value);
            remove => _oneSecondTickListener.Remove(value);
        }

        private float _unscaledTime;
        private float _lastTime;
        private float _deltaTime;
        private float _scaleTime;
        private float _time;

        public Timer()
        {
            _lastTime = GetTime();
            _scaleTime = 1f;
            _deltaTime = 0f;
            _time = 0f;
        }

        public float Time => _time;
        public float DeltaTime => _deltaTime;
        public float TimeScale { get => _scaleTime;
            set => _scaleTime = Math.Max(0f, value);
        }

        public void Update()
        {
            var now = GetTime();
            var delta = now - _lastTime;
            _unscaledTime += delta;
            _deltaTime = delta * TimeScale;
            _time += _deltaTime;

            bool isNewSecondTick = Mathf.Floor(now) > Mathf.Floor(_lastTime);

            _lastTime = now;

            _tickListener.Invoke();

            if (isNewSecondTick)
            {
                _oneSecondTickListener.Invoke();
            }
        }

        public void LateUpdate()
        {
            _postTickListener.Invoke();
        }

        public void FixedUpdate()
        {
            _fixedTickListener.Invoke();
        }

        private float GetTime()
        {
            return Environment.TickCount / 1000f;
        }
    }
}