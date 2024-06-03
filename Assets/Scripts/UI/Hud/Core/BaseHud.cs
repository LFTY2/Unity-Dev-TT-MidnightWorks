using Core.Observer;
using UnityEngine;

namespace UI.Hud.Core
{
    public interface IHud
    {
        bool IsActive
        {
            set;
        }
    }

    public abstract class BaseHud : MonoBehaviour, IHud
    {
        public bool IsActive
        {
            set => gameObject.SetActive(value);
        }

        public GameObject GameObject => gameObject;
    }

    public abstract class BaseHudWithModel<T> : BaseHud, IObserver where T : Observable
    {
        private T _model;

        public T Model
        {
            protected get => _model;
            set
            {
                if (null != _model)
                {
                    _model.RemoveObserver(this);
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
    }
}