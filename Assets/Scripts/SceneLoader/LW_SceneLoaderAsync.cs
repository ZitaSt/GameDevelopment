using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using LittleWarrior;
using LittleWarrior.SceneLoader;

namespace LittleWarrior.SceneLoader
{
    public class LW_SceneLoaderAsync : LW_Singleton<LW_SceneLoaderAsync>
    {

        // NOTE (skn): Loading Progress: private setter, public getter
        private float _loadingProgress;
        public float LoadingProgress
        {
            get
            {
                return _loadingProgress;
            }
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadScenesInOrder(sceneName));
        }

        private IEnumerator LoadScenesInOrder(string sceneName)
        {
            // NOTE (skn): LoadSceneAsync() returns an AsyncOperation, 
            //             so will only continue past this point when the 
            //             Operation has finished
            yield return SceneManager.LoadSceneAsync("Loading");

            // NOTE (skn): As soon as we've finished loading the loading screen, 
            //             start loading the game scene
            yield return StartCoroutine(LoadFinalScene(sceneName));
        }

        private IEnumerator LoadFinalScene(string sceneName)
        {
            var asyncScene = SceneManager.LoadSceneAsync(sceneName);

            // NOTE (skn): This value stops the scene from displaying when it's finished 
            //             loading
            asyncScene.allowSceneActivation = false;

            while (!asyncScene.isDone)
            {
                // NOTE (skn): Loading bar progress
                _loadingProgress = Mathf.Clamp01(asyncScene.progress / 0.9f) * 100;

                // NOTE (skn): Scene has loaded as much as possible, the last 10% can't 
                //             be multi-threaded
                if (asyncScene.progress >= 0.9f)
                {
                    // NOTE (skn): Brings up the scene
                    asyncScene.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }

}
