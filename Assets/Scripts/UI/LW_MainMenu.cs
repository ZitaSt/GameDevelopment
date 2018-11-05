using UnityEngine;
using LittleWarrior.SceneLoader;

namespace LittleWarrior.UI
{
    public class LW_MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            // NOTE (skn): To load a scene in Synchronous version
            //SceneLoader.Instance.LoadScene();

            LW_SceneLoaderAsync.Instance.LoadScene("CityMap");
        }

        public void Options()
        {

        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
