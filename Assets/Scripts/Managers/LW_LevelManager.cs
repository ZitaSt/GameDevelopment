using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Properties;
using System;
using LittleWarrior.AI;
using LittleWarrior.Managers;

namespace LittleWarrior.Managers
{
    public class LW_LevelManager : LW_Singleton<LW_LevelManager>
    {
        protected LW_LevelManager()
        {
        
        }
        private List<GameObject> _LevelSpawnPoints;
        private List<Transform> _AliveEnemis;
        private int _LevelEnemyCounts = 0;
        private int _CollectedDeaths = 0;
        private GameObject _LevelFinish = null;
        private GameObject _Player;

        void Awake()
        {
            GameObject go = new GameObject("Alive Enemies");
            go.transform.SetParent(this.transform);
            go.transform.tag = "AliveEnemies";
            _AliveEnemis = new List<Transform>();
            _LevelSpawnPoints = new List<GameObject>();
            
        }

        void Start()
        {
            _LevelFinish = GameObject.Find("LevelFinish");
            if(_LevelFinish)
            {
                _LevelFinish.SetActive(false);
            }
            InvokeRepeating("RemoveTheDeads", 2.0f, 5.0f);
            _Player = GameObject.FindGameObjectWithTag("Player");
        }

        public void Reset()
        {
            _AliveEnemis = new List<Transform>();
            _LevelSpawnPoints = new List<GameObject>();
            _LevelEnemyCounts = 0;
            _CollectedDeaths = 0;
        }

        public void SPRegister(GameObject sp, int ec)
        {
            _LevelSpawnPoints.Add(sp);
            _LevelEnemyCounts = +ec;
        }

        public void ERegister(Transform e)
        {
            _AliveEnemis.Add(e);
        }

        private void RemoveTheDeads()
        {
            List<int> tIndexes = new List<int>();
            foreach(Transform t in _AliveEnemis)
            {
                
                if(!t.gameObject.active)
                {
                    tIndexes.Add(_AliveEnemis.IndexOf(t));
                }
            }

            foreach(int i in tIndexes)
            {
                _Player.GetComponent<LW_PlayerInventory>().StoreCurrency(
                    LittleWarrior.Enums.Currency.Dollar,
                    _AliveEnemis[i].gameObject.GetComponent<LW_AgentManager>().rewardDollar);

                Destroy(_AliveEnemis[i].gameObject);
                _AliveEnemis.RemoveAt(i);
                _CollectedDeaths++;

            }

            CheckLevelCondtion();
        }

        private void CheckLevelCondtion()
        {
            if(_CollectedDeaths == _LevelEnemyCounts)
            {
                if (_LevelFinish)
                {
                    _LevelFinish.SetActive(true);
                }
                    
            }
        }
    }
}

