using Core.Inject;
using Domain;
using Level;
using Managers;
using UI.Hud;
using UnityEngine.SceneManagement;

namespace States
{
    public class GameLoadLevelState : GameState
    {
        [Inject] protected GameStateManager _gameStateManager;
        [Inject] protected GameConfig _config;
        [Inject] protected Context _context;
        [Inject] protected HudManager _hudManager;

        private int _farm;

        public override void Initialize()
        {
            _hudManager.ShowAdditional<SplashScreenHudMediator>();

            var model = GameModel.Load(_config);
            _farm = model.Farm;

            if (_farm < 1) _farm = 1;
            else if (_farm >= SceneManager.sceneCountInBuildSettings)
            {
                _farm = SceneManager.sceneCountInBuildSettings - 1;
            }

            model.Farm = _farm;
            model.Save();

            SceneManager.sceneLoaded += OnSceneLoaded;

            for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if (SceneManager.GetSceneByBuildIndex(i).isLoaded)
                {
                    SceneManager.UnloadSceneAsync(i);
                }
            }
            LoadScene();
        }

        public override void Dispose()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode arg)
        {
            LevelView level = null;
            var sceneObjects = scene.GetRootGameObjects();
            foreach (var sceneObject in sceneObjects)
            {
                level = sceneObject.GetComponent<LevelView>();

                if (null != level)
                    break;
            }

            _context.Install(level);

            _gameStateManager.SwitchToState<GamePlayState>();
        }

        protected virtual void LoadScene()
        {
            SceneManager.LoadScene(_farm, LoadSceneMode.Additive);
        }
    }
}