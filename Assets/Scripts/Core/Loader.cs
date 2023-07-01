#region

using UnityEngine.SceneManagement;

#endregion

namespace Core
{
    public static class Loader
    {
        public enum Scene
        {
            GameScene,
            MainMenu,
            Loading
        }

        private static Scene _targetScene;

        public  static void LoadScene(Scene targetScene)
        {
            _targetScene = targetScene;
            
            SceneManager.LoadScene(Scene.Loading.ToString());
        }

        public static void LoaderCallback()
        {
            SceneManager.LoadScene(_targetScene.ToString());
        }
    }
}