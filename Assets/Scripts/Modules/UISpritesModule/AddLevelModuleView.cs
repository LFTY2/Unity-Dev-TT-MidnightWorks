using UnityEngine;

namespace Modules.UISpritesModule
{
    public class AddLevelModuleView : MonoBehaviour
    {
        [SerializeField] private AudioSource _scoreAddSound;

        public AudioSource ScoreAdded => _scoreAddSound;
    }
}

