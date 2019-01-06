using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Managers;
using LittleWarrior.Enums;
using LittleWarrior.SceneLoader;

namespace LittleWarrior.Slaves
{
    public class LW_ShopTransactor : MonoBehaviour
    {
        LW_GameManager GM;

        private void Awake()
        {
            GM = LW_GameManager.Instance;
            GM.OnStateChange = HandleOnStateChange;
        }

        private void HandleOnStateChange()
        {
            GM.SetGameState(GameState.Level001);
            Invoke("LoadLevel", 0.1f);
        }

        private void LoadLevel()
        {
            LW_SceneLoaderAsync.Instance.LoadScene("Shop");
        }
    }
}

