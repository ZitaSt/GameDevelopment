using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Properties;

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

        void Awake()
        {
            GameObject go = new GameObject("Alive Enemies");
            go.transform.SetParent(this.transform);
            go.transform.tag = "EnemiesParent";
            _AliveEnemis = new List<Transform>();
            _LevelSpawnPoints = new List<GameObject>();
        }

        void Start()
        {
            InvokeRepeating("RemoveTheDeads", 2.0f, 5.0f);
        }

        public void Reset()
        {
            _AliveEnemis = new List<Transform>();
            _LevelSpawnPoints = new List<GameObject>();
            _LevelEnemyCounts = 0;
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
                Destroy(_AliveEnemis[i].gameObject);
                _AliveEnemis.RemoveAt(i);
            }
        }
    }
}

