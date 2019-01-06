using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Enums;

namespace LittleWarrior.Managers
{
    public delegate void OnStateChangeHandler();

    public class LW_GameManager : LW_Singleton<LW_GameManager>
    {
        protected LW_GameManager()
        {
            
        }
        public OnStateChangeHandler OnStateChange;
        public GameState gameState { get; private set; }

        public void SetGameState(GameState state)
        {
            this.gameState = state;
        }

        public void OnApplicationQuit()
        {
            LW_GameManager.Instance.OnDestroy();
        }
    }
}

