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
        private Dictionary<WeaponTypes, int> _Bullets;
        private Dictionary<Currency, int> _Currencies;
        public List<LW_Weapon> purchasedWeapons = new List<LW_Weapon>();

        public int startingAmountDollar = 500;


        private void Awake()
        {
            _Bullets = new Dictionary<WeaponTypes, int>();
            _Currencies = new Dictionary<Currency, int>();
        }

        void Start()
        {
            _Bullets.Add(WeaponTypes.Handgun01, 100);
            _Bullets.Add(WeaponTypes.Handgun02, 0);
            _Bullets.Add(WeaponTypes.Handgun03, 0);
            _Bullets.Add(WeaponTypes.Rifle, 0);
            _Currencies.Add(Currency.Dollar, startingAmountDollar);
        }

        public int ConsumeBullets(WeaponTypes key, int amount)
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

        public int StoreBullets(WeaponTypes key, int amount)
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

        public int GetBulletsCount(WeaponTypes key)
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

        public void PurchaseWeapon(LW_Weapon wp)
        {
            for(int i = 0; i < purchasedWeapons.Count; i++)
            {
                if(purchasedWeapons[i].GetComponent<LW_WeaponIndex>().weaponIndex == 
                   wp.GetComponent<LW_WeaponIndex>().weaponIndex)
                {
                    StoreBullets(wp.GetComponent<LW_WeaponIndex>().weaponIndex,
                                 wp.bulletsPerMag);
                    return;
                }
            }
            purchasedWeapons.Add(wp);
            _Bullets.Add(wp.GetComponent<LW_WeaponIndex>().weaponIndex, wp.bulletsPerMag);
        }
    }
}

