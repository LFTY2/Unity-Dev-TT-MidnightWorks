using System.Collections.Generic;
using Camera;
using Plugins.Joystick.Scripts;
using UI.Core;
using UI.Hud.Core;
using UnityEngine;

namespace UI
{
    public sealed class GameView : MonoBehaviour
    {
        [SerializeField] public AudioSource _backgroundMusic;
        [SerializeField] public BaseHud[] Huds;
        [SerializeField] private List<ComponentPoolFactory> _productViews;
        [SerializeField] public Joystick Joystick;
        [SerializeField] public CameraController CameraController;
        public List<ComponentPoolFactory> ProductViews => _productViews;
        public IEnumerable<IHud> AllHuds()
        {
            foreach (var hud in Huds)
            {
                yield return hud;
            }
        }
    }
}