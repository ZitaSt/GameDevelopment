using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleWarrior.SpawnPoint
{
    // TODO (skn): Need game design requirements to add new functions to
    //             class
    public class LW_SpawnPoint : MonoBehaviour
    {
        public GameObject[] zombieEnemies;

        public int zombiesInSpawnPoint = 5;
        public float intervalBetweenSpawns = 3;
        private float timeSinceLastSpawn = 0f;
        private int spawnedZombies = 0;

        public Transform[] spawnPoints;

        void Update()
        {
            if (spawnedZombies < zombiesInSpawnPoint)
            {
                if (timeSinceLastSpawn < intervalBetweenSpawns)
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
                Debug.Log(">>> " + this.gameObject.name + " Spawn cycle is finished!");
            }

        }

        void SpawnZombie()
        {
            Vector3 finalSpawnLocation = spawnPoints[Random.Range(0, 
                                                     spawnPoints.Length)].position;
            Instantiate(zombieEnemies[Random.Range(0, zombieEnemies.Length)],
                        finalSpawnLocation, Quaternion.identity);
            spawnedZombies++;
        }
    }
}

