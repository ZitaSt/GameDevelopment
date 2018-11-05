using UnityEngine;
using LittleWarrior.SceneLoader;

namespace LittleWarrior.UI
{
    public class LW_CityMapUI : MonoBehaviour
    {
        public void Level01()
        {
            LW_SceneLoaderAsync.Instance.LoadScene("FirstLevel");
        }

        public void Level02()
        {
            LW_SceneLoaderAsync.Instance.LoadScene("SecondLevel");
        }

        public void Shop()
        {
            LW_SceneLoaderAsync.Instance.LoadScene("Shop");
        }
        public void MainMenu()
        {
            LW_SceneLoaderAsync.Instance.LoadScene("MainMenu");
        }
    }
}
