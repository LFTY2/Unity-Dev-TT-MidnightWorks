using UnityEngine;

namespace Level.Entity
{
    public class EntityConfig : ScriptableObject
    {
        [Min(0)] public int Number;
        [Min(1)] public int Area;
    }
}