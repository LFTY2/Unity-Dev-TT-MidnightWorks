using System;
using Core.Observer;

namespace Core.UI
{
    public abstract class BehaviourWithModel<T> : BehaviourComponent, IObserver where T : Observable
    {
        [NonSerialized]
        private bool _released = false;
        private bool _isStarted = false;
        private T _model;

        public T Model
        {
            protected get
            {
                return _model;
            }
            set
            {
                if (null != _model)
                {
                    _model.RemoveObserver(this);
                }
                
                if (!_isStarted)
                {
                    _model = value;
                    return;
                }

                OnApplyModel(value);
                
                _model = value;
                
                if (null != _model)
                {
                    _model.AddObserver(this);
                    OnModelChanged(_model);
                }
            }
        }
        

        protected override void OnStart()
        {
            base.OnStart();

            _isStarted = true;

            if (null != _model)
            {
                this.Model = _model;
            }
        }

        protected abstract void OnModelChanged(T model);

        protected virtual void OnApplyModel(T model)
        {
        }
        
        public void OnObjectChanged(Observable observable)
        {
            if (observable is T)
            {
                OnModelChanged((T)observable);
            }
            else
            {
                OnModelChanged(Model);
            }
        }

        protected void OnDestroy()
        {
            if (!_released)
            {
                this.Model = null;
            }

            _released = true;
        }
    }
}
