using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Enums;
using LittleWarrior.Managers;
using LittleWarrior.Properties;

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
        public int Level { get; private set; }

        public GameObject _Player { get; private set; }
        
        private void Awake()
        {
            Level = 0;
        }

        public void SetGameState(GameState state)
        {
            this.gameState = state;

            if(gameState == GameState.MainMenu)
            {
                _Player = GameObject.FindWithTag("Player");
            }

            if(_Player)
            {
                VRTK.VRTK_SDKSetup rc = _Player.GetComponent<VRTK.VRTK_SDKSetup>();
                rc.actualRightController.GetComponent<LW_ViveInteraction>().ResetTheContactRigidBodies();
            }

        }

        public void OnApplicationQuit()
        {
            LW_GameManager.Instance.OnDestroy();
        }

        public void RequestLevelChange()
        {
            this.Level++;
        }
    }
}

