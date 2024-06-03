using System;
using System.Collections.Generic;
using Core.Inject;

namespace Core.StateManager
{
    public class StateManager<T> : IDisposable where T : State
    {
        protected readonly OneListener<T> _onChangeState = new OneListener<T>();
        public event Action<T> CHANGE_STATE
        {
            add { _onChangeState.Add(value); }
            remove { _onChangeState.Remove(value); }
        }

        [Inject.Inject]
        protected Injector _injector;

        private readonly Dictionary<Type, T> _statesMap;
        protected T _state;

        public StateManager()
        {
            _statesMap = new Dictionary<Type, T>(10);
            _state = null;
        }

        public void Dispose()
        {
            if (null != _state)
            {
                _state.Dispose();
            }

            _state = null;
            _statesMap.Clear();
        }

        public virtual T Current
        {
            get { return _state; }
            protected set
            {
                if (null != _state)
                {
                    _state.Dispose();
                }

                _state = value;

                _state.Initialize();

                _onChangeState.Invoke(_state);
            }
        }

        public void SwitchToState(T state)
        {
            _injector.Inject(state);
            this.Current = state;
        }

        public void SwitchToState<T1>()
        {
            SwitchToState(typeof(T1));
        }

        public void SwitchToState(Type type)
        {
            if (!_statesMap.ContainsKey(type))
            {
                _statesMap[type] = (T)Activator.CreateInstance(type);
            }

            T state = _statesMap[type];
            _injector.Inject(state);
            this.Current = state;
        }
    }
}