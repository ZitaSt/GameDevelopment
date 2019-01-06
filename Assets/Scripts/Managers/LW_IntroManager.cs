using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Managers;
using LittleWarrior.Enums;
using LittleWarrior.SceneLoader;

namespace LittleWarrior.Managers
{
    public class LW_IntroManager : MonoBehaviour
    {
        LW_GameManager GM;

        private void Awake()
        {
            GM = LW_GameManager.Instance;
            GM.OnStateChange = HandleOnStateChange;
        }

        private void Start()
        {
            GM.OnStateChange();
        }

        private void HandleOnStateChange()
        {
            GM.SetGameState(GameState.MainMenu);
            Invoke("LoadLevel", 3.0f);
        }

        private void LoadLevel()
        {
            LW_SceneLoaderAsync.Instance.LoadScene("MainMenu");
        }
    }
}

