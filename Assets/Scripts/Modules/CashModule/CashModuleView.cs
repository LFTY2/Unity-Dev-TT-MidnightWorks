using UI.Core;
using UnityEngine;

namespace Modules.CashModule
{
    public sealed class CashModuleView : ComponentPoolFactory
    {
        [SerializeField] private AudioSource _cashCollectSound;
        public AudioSource CashCollectSound => _cashCollectSound;
    }
}