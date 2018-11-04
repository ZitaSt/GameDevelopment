using UnityEngine;
using UnityEngine.UI;
using LittleWarrior.SceneLoader;

namespace LittleWarrior.SceneLoader
{
    public class LW_LoadingProgress : MonoBehaviour
    {

        private Text LoadingText;

        void Start()
        {
            LoadingText = GetComponent<Text>();

            if (LoadingText == null)
            {
                Debug.Log(">>>> The LW_LoadingProgress is added to a wrong game object.");
                Destroy(gameObject);
            }

        }

        // Update is called once per frame
        void Update()
        {
            LoadingText.text = LW_SceneLoaderAsync.Instance.LoadingProgress.ToString("F0") + "%";
        }
    }
}
