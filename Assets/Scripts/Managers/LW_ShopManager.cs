using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Weapon;
using LittleWarrior.Managers;

namespace LittleWarrior.Managers
{
    public class LW_ShopManager : MonoBehaviour
    {
        public List<GameObject> weaponsList = new List<GameObject>();
        private Transform[] _Containers;
        private LW_GameManager GM;

        private void Awake()
        {
            int childcount = this.transform.childCount;
            _Containers = new Transform[childcount];
            for(int i = 0; i < childcount; i++)
            {
                _Containers[i] = this.transform.GetChild(i).transform;
            }

            GM = LW_GameManager.Instance;
        }

        private void Start()
        {
            int j = 0;
            for(int i = 0; i < weaponsList.Count; i++)
            {
                LW_WeaponIndex wi = weaponsList[i].GetComponent<LW_WeaponIndex>();
                if(wi == null)
                {
                    print("XXXX Weapon without weaponindex profile at index: " + i);
                    Debug.Break();
                }
                if (wi.openInLevel == GM.Level)
                {
                    GameObject go = Instantiate(weaponsList[i], _Containers[j].transform);
                    j++;
                }
            }
        }
    }
}

