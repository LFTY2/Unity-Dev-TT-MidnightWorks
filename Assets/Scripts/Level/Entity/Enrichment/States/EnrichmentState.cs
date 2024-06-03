using Audio;
using Core;
using Core.Inject;
using Core.StateManager;
using Level.Entity.Plant;
using Managers;
using UI;

namespace Level.Entity.Enrichment.States
{
    public abstract class EnrichmentState : State
    {
        [Inject] protected EnrichmentController _enrichment;
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

