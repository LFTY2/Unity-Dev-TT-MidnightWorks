using Audio;
using Core;
using Core.Inject;
using Core.StateManager;
using Level.Entity.Plant;
using Managers;
using UI;

namespace Level.Entity.Shelf.States
{
    public abstract class ShelfState : State
    {
        [Inject] protected ShelfController _shelf;
        [Inject] protected GameManager _gameManager;
        

        public override void Dispose()
        {
        }

        public override void Initialize()
        {
        }
    }
}

