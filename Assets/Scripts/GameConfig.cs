using System;
using UnityEngine;

[CreateAssetMenu(menuName = "config/gameconfig")]
public sealed class GameConfig : ScriptableObject
{
    public static GameConfig Load()
    {
        var result = Resources.Load<GameConfig>("GameConfig");
        return result;
    }

    [Min(0)] public int DefaultCash;
    [Min(1)] public int DefaultFarm = 1;

    public float CashPileRadius = 1f;
    public float CashRegisterItemRadius = 0.75f;
    public float BuyUpdateRadius = 1f;
    public float PlantItemRadius = 0.5f;
    public float UtilityItemRadius = 0.5f;
    public float EntityRadius = 3f;
    public int CustomersMax = 10;

    [Header("Units")]
    public float PlayerWalkSpeed = 4f;
    public float PlayerRotationSpeed = 10f;
    public float CustomerWalkSpeed = 3f;
        

    [Header("")]
    public FarmConfig[] Farms;

    [Header("Splash Screen")]
    public float SplashScreenDuration = 1f;
}

[Serializable]
public sealed class FarmConfig
{
    [Min(1)] public int SceneIndex;
    public Sprite Icon;
    public string Label;
    public Vector3 PlayerSpawnPosition = new Vector3(0,0,0);
}