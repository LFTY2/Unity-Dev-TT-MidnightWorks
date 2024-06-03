using TMPro;
using UI.Hud.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud
{
    public sealed class SplashScreenHudView : BaseHud
    {
        [SerializeField] private Image _fillBarImage;
        public Image FillBarImage => _fillBarImage;
    }
}