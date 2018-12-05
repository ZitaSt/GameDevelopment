using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleWarrior.Managers
{
    public class LW_GameManager : LW_Singleton<LW_GameManager>
    {
        protected LW_GameManager()
        {
        
        }

        public List<Transform> AliveEnemis;

        void Awake()
        {
            GameObject go = new GameObject("Alive Enemies");
            go.transform.SetParent(this.transform);
            go.transform.tag = "EnemiesParent";
            AliveEnemis = new List<Transform>();
        }
    }
}

