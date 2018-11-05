using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using LittleWarrior;

namespace LittleWarrior.SceneLoader
{
    public class LW_SceneLoader : LW_Singleton<LW_SceneLoader>
    {
        protected LW_SceneLoader()  {}
        
        public void LoadScene()
        {
            SceneManager.LoadScene("Loading");

            StartCoroutine(LoadAfterTimer());
        }
        
        private IEnumerator LoadAfterTimer()
        {
            // NOTE (skn): It's safe to do it so cause it would avoid
            //             the flash screen
            yield return new WaitForSeconds(2.0f);

            LoadScene();
        }
        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

