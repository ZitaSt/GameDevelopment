using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior;


namespace LittleWarrior.Managers
{
    public class LW_PlayerInventory : LW_Singleton<LW_PlayerInventory>
    {
        [SerializeField]
        private Dictionary<string, int> bullets;

        void Start()
        {
            bullets = new Dictionary<string, int>();
            bullets.Add("Handgun", 24);
        }

        public int ConsumeBullets(string key, int amount)
        {
            if(bullets.ContainsKey(key))
            {
                if (bullets[key] > 0)
                {
                    int temp = bullets[key] - amount;
                    if (temp >= 0)
                    {
                        bullets[key] = temp;
                        return amount;
                    }
                    else
                    {
                        bullets[key] = 0;
                        return (amount + temp);
                    }
                }
                else
                {
                    // NOTE (skn): Out of bullets
                    return 0;
                }
            }
            else
            {
                // NOTE (skn): Fatal wrong setting
                return -1;
            }
        }

        public void StoreBullets(string key, int amount)
        {
            if(bullets.ContainsKey(key))
            {
                bullets[key] = bullets[key] + amount;
            }
            else
            {
                Debug.LogError("XXXX - Storing - Proper bullet type" + key + " is not found.");

            }
        }

        public int GetBulletsCount(string key)
        {
            if (bullets.ContainsKey(key))
            {
                return bullets[key];
            }
            else
            {
                Debug.LogError("XXXX - Reading - Proper bullet type" + key + " is not found.");
                return -1;
            }
        }
    }
}

