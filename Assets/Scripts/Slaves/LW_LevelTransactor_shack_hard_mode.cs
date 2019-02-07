using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Managers;
using LittleWarrior.Enums;
using LittleWarrior.SceneLoader;

namespace LittleWarrior.Slaves
{
    [RequireComponent(typeof(Rigidbody))]
    public class LW_LevelTransactor_shack_hard_mode : LW_SlavesManager
    {
        LW_GameManager GM;

        private void Awake()
        {
            GM = LW_GameManager.Instance;
        }

        public override void SetSlaveActive()
        {
            GM.OnStateChange = HandleOnStateChange;
            GM.OnStateChange();
        }

        private void HandleOnStateChange()
        {
            GM.SetGameState(GameState.Level, false);
            Invoke("LoadLevel", 2.0f);
        }

        private void LoadLevel()
        {
            switch (GM.Level)
            {
                case 0:
                    {
                        LW_SceneLoaderAsync.Instance.LoadScene("Shack_hard_mode");
                    }
                    break;
                case 1:
                    {
                        LW_SceneLoaderAsync.Instance.LoadScene("City_hard_mode");
                    }
                    break;
                default:
                    {

                    }
                    break;
            }
        }
    }
}

