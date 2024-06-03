using System;
using System.Collections.Generic;
using Core.Inject;
using Domain;
using Level;
using Level.Core;
using Level.Entity.Player.Player;
using Level.Entity.Player.Player.PlayerStates;
using Managers;
using Modules.CashModule;
using Modules.CashRegisterModule;
using Modules.EntityModule;
using Modules.UINotificationModule;
using Modules.UISpritesModule;
using UI;
using UI.Hud;
using UnityEngine;
using Object = UnityEngine.Object;

namespace States
{
    public sealed class GamePlayState : GameState
    {
        [Inject] private Injector _injector;
        [Inject] private Context _context;
        [Inject] private GameView _gameView;
        [Inject] private HudManager _hudManager;
        [Inject] private GameConfig _config;
        [Inject] private LevelView _levelView;

        private GameManager _gameManager;
        private PlayerView _playerView;

        private readonly List<Module> _levelModules;

        public GamePlayState()
        {
            _levelModules = new List<Module>();
        }

        public override void Initialize()
        {
            _gameManager = new GameManager(_config);
            _context.Install(_gameManager);
            
            _playerView = Object.Instantiate(_gameView.Player, _config.Farms[GameModel.Load(_config).Farm - 1].PlayerSpawnPosition, Quaternion.identity).GetComponent<PlayerView>();
            _gameManager.Player = new PlayerController(_playerView, _context);
            _gameManager.Player.View.transform.eulerAngles = new Vector3(0f, 180f, 0f);

            _gameView.CameraController.SetTarget(_gameManager.Player.View.transform);
            _gameView.CameraController.enabled = true;

            InitLevelModules();

            _hudManager.ShowAdditional<GamePlayHudMediator>();

            _gameView.Joystick.enabled = true;

            _gameManager.Player.SwitchToState(new PlayerIdleState());
            
            
        }

        public override void Dispose()
        {
            _gameView.CameraController.enabled = false;

            DisposeLevelModules();

            _hudManager.HideAdditional<GamePlayHudMediator>();

            _gameView.Joystick.enabled = false;

            _gameManager.Player.Dispose();
            _gameManager.Dispose();

            _context.Uninstall(_gameManager);
        }

        private void InitLevelModules()
        {
            AddModule<EntityModule, EntityModuleView>(_levelView);
            AddModule<CashRegisterModule, CashRegisterModuleView>(_levelView);
            AddModule<CashModule, CashModuleView>(_gameView);
            AddModule<AddLevelModule, AddLevelModuleView>(_gameView);
            AddModule<UINotificationModule, UINotificationModuleView>(_gameView);
        }

        private void AddModule<T, T1>(Component component) where T : Module
        {
            var view = component.GetComponent<T1>();
            var result = (T)Activator.CreateInstance(typeof(T), new object[] { view });
            _levelModules.Add(result);
            _injector.Inject(result);
            result.Initialize();
        }

        private void DisposeLevelModules()
        {
            foreach (var levelModule in _levelModules)
            {
                levelModule.Dispose();
            }
            _levelModules.Clear();
        }
    }
}