using System.IO;
using Audio;
using Core;
using Core.Inject;
using Level.Entity.ProductPool;
using Managers;
using States;
using UI;
using UnityEngine;

public sealed class GameStartBehaviour : MonoBehaviour
{
    [SerializeField] private GameView _gameView;
    private Timer _timer;
    private Context Context { get; set; }
    
    private void Start()
    {
        GameConstants.SaveModelFilePath = Path.Combine(Application.persistentDataPath, "model.json");
        GameConstants.SaveGameDataFilePath = Path.Combine(Application.persistentDataPath, "game_data.json");
        
        _timer = new Timer();

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        Application.runInBackground = true;
        
        var context = new Context();

        context.Install(
            new Injector(context),
            new GameStateManager(),
            new HudManager(),
            new AudioManager(),
            new ProductPool(_gameView)
        );

        context.Install(GetComponents<Component>());
        context.Install(_timer);
        context.ApplyInstall();
            
        context.Get<GameStateManager>().SwitchToState(typeof(GameInitializeState));

       

        Context = context;
    }

    public void Reload()
    {
        Context.Get<GameStateManager>().Dispose();
        Context.Dispose();

        Start();
    }

    private void Update()
    {
        _timer.Update();
    }

    private void LateUpdate()
    {
        _timer.LateUpdate();
    }

    private void FixedUpdate()
    {
        _timer.FixedUpdate();
    }
}