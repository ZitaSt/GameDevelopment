using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Managers;

namespace LittleWarrior.SpawnPoint
{
    // TODO (skn): Need game design requirements to add new functions to
    //             class
    public class LW_SpawnPoint : MonoBehaviour
    {
        public GameObject[] zombieEnemies;

        public int zombiesInSpawnPoint = 0;
        public float intervalBetweenSpawns = 3;
        private float timeSinceLastSpawn = 0f;
        private int spawnedZombies = 0;
        private LW_LevelManager _gameManager;

        public Transform[] spawnPoints;

        void Awake()
        {
            GameObject go = GameObject.FindWithTag("LevelManager");
            if(go)
            {
                _gameManager = go.GetComponent<LW_LevelManager>();
            }
            else
            {
                //TODO (skn): Ask someone to instantiate LevelManager
                Debug.LogError("XXXX - Unable to find level manager.");
            }
            _gameManager.SPRegister(this.gameObject, zombiesInSpawnPoint);
        }
        void Update()
        {
            if (spawnedZombies < zombiesInSpawnPoint)
            {
                if (timeSinceLastSpawn >= intervalBetweenSpawns)
                {
                    SpawnZombie();
                    timeSinceLastSpawn = 0;
                }
                else
                {
                    timeSinceLastSpawn = timeSinceLastSpawn + Time.deltaTime;
                }
            }
            else
            {
                //Debug.Log(">>> " + this.gameObject.name + 
                //          " Spawn cycle is finished!");
            }

        }

        void SpawnZombie()
        {
            Vector3 finalSpawnLocation = spawnPoints[Random.Range(0, 
                                                spawnPoints.Length)].position;
            GameObject go = Instantiate(zombieEnemies[Random.Range(0, 
                                        zombieEnemies.Length)],
                                        finalSpawnLocation, 
                                        Quaternion.identity);
            spawnedZombies++;
            _gameManager.ERegister(go.transform);
            GameObject pgo = GameObject.FindGameObjectWithTag("AliveEnemies");
            go.transform.SetParent(pgo.transform);
        }
    }
}

