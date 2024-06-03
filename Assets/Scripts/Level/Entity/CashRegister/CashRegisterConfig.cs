using System;
using UnityEngine;

namespace Level.Entity.CashRegister
{
    [Serializable]
    [CreateAssetMenu(menuName = "config/CashRegister Config")]
    public sealed class CashRegisterConfig : ScriptableObject
    {
        public CashRegisterLvlConfig[] Lvls;
    }

    [Serializable]
    public sealed class CashRegisterLvlConfig
    {
        public int TargetUpdateProgress;
        public int CashiersCount;
        public int Price;
    }
}

