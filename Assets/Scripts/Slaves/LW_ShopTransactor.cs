﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Managers;
using LittleWarrior.Enums;
using LittleWarrior.SceneLoader;

namespace LittleWarrior.Slaves
{
    [RequireComponent(typeof(Rigidbody))]
    public class LW_ShopTransactor : LW_SlavesManager
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
            GM.SetGameState(GameState.ShopArea, false);
            Invoke("LoadLevel", 2.0f);
        }

        private void LoadLevel()
        {
            LW_SceneLoaderAsync.Instance.LoadScene("Shop");
        }
    }
}

