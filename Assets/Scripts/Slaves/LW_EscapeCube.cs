using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Managers;
using LittleWarrior.Enums;
using LittleWarrior.SceneLoader;

namespace LittleWarrior.Slaves
{
    [RequireComponent(typeof(Rigidbody))]
    public class LW_EscapeCube : LW_SlavesManager
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
            GM.SetGameState(GameState.ExitApplication);
            Invoke("RunFree", 2.0f);
        }

        private void RunFree()
        {
            Application.Quit();
        }
    }
}

