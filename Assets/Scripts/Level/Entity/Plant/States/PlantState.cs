using Audio;
using Core;
using Core.Inject;
using Core.StateManager;
using Managers;
using UI;

namespace Level.Entity.Plant.States
{
    public abstract class PlantState : State
    {
        [Inject] protected PlantController _plant;
        [Inject] protected Timer _timer;
        [Inject] protected GameManager _gameManager;
        [Inject] protected GameView _gameView;
        [Inject] protected AudioManager _audioManager;

        public override void Dispose()
        {
        }

        public override void Initialize()
        {
        }
    }
}

