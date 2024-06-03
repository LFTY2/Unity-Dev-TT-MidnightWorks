using System;

namespace Core.StateManager
{
    public abstract class State : IDisposable
    {
        public abstract void Initialize();
        public abstract void Dispose();
    }
}