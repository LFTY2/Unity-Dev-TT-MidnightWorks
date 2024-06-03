using System;
using UnityEngine;

namespace Core.UI
{
    public abstract class BehaviourComponent : MonoBehaviour
    {
        protected virtual void OnStart()
        {
        }
        

        private void Start()
        {
            if (Application.isEditor && !Application.isPlaying)
                return;

            OnStart();
        }
    }
}