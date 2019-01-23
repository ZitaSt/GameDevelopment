using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Weapon;
using LittleWarrior.Enums;


namespace LittleWarrior.Managers
{
    public class LW_PlayerInventory : LW_Singleton<LW_PlayerInventory>
    {
        [SerializeField]
        private Dictionary<string, int> _Bullets;
        private Dictionary<Currency, int> _Currencies;


        private void Awake()
        {
            _Bullets = new Dictionary<string, int>();
            _Currencies = new Dictionary<Currency, int>();
        }

        void Start()
        {
            _Bullets.Add("Handgun", 100);
            _Bullets.Add("Glock17", 0);
            _Bullets.Add("M1911", 0);
            _Currencies.Add(Currency.Dollar, 0);
        }

        public int ConsumeBullets(string key, int amount)
        {
            if(_Bullets.ContainsKey(key) || amount > 0)
            {
                if (_Bullets[key] > 0)
                {
                    int temp = _Bullets[key] - amount;
                    if (temp >= 0)
                    {
                        _Bullets[key] = temp;
                        return amount;
                    }
                    else
                    {
                        _Bullets[key] = 0;
                        return (amount + temp);
                    }
                }
                else
                {
                    // NOTE (skn): Out of _Bullets
                    return 0;
                }
            }
            else
            {
                // NOTE (skn): Fatal wrong setting
                return -1;
            }
        }

        public int StoreBullets(string key, int amount)
        {
            if(_Bullets.ContainsKey(key))
            {
                _Bullets[key] = _Bullets[key] + amount;
                return 1;
            }
            else
            {
                Debug.LogError("XXXX - Storing - Proper bullet type" + key + " is not found.");
                return -1;
            }
        }

        public int GetBulletsCount(string key)
        {
            if (_Bullets.ContainsKey(key))
            {
                return _Bullets[key];
            }
            else
            {
                Debug.LogError("XXXX - Reading - Proper bullet type" + key + " is not found.");
                return -1;
            }
        }

        public int ConsumeCurrency(Currency key, int amount)
        {
            if (_Currencies.ContainsKey(key) || amount > 0)
            {
                if (_Currencies[key] >= amount)
                {
                    _Currencies[key] = _Currencies[key] - amount;
                    return amount;
                }
                else
                {
                    // NOTE (skn): Out of _Bullets
                    return 0;
                }
            }
            else
            {
                // NOTE (skn): Fatal wrong setting
                return -1;
            }
        }

        public int StoreCurrency(Currency key, int amount)
        {
            if(_Currencies.ContainsKey(key))
            {
                _Currencies[key] = _Currencies[key] + amount;
                return 1;
            }
            else
            {
                Debug.LogError("XXXX - Storing - Proper bullet type" + key + " is not found.");
                return -1;
            }
        }

        public int GetCurrencyAmount(Currency key)
        {
            if(_Currencies.ContainsKey(key))
            {
                return _Currencies[key];
            }
            else
            {
                Debug.LogError("XXXX - Reading - Proper resource type" + key + " is not fount.");
                return -1;
            }
        }
    }
}

