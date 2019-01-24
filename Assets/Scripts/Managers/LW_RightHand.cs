using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Weapon;


namespace LittleWarrior.Managers
{
    public class LW_RightHand : MonoBehaviour
    {
        private List<GameObject> _Weapons = new List<GameObject>();
        private GameObject _TempWeapon = null;

        private void Start()
        {
            for(int i = 0; i < this.transform.childCount; i++)
            {
                if(this.transform.GetChild(i).GetComponent<LW_WeaponIndex>())
                {
                    _Weapons.Add(this.transform.GetChild(i).gameObject);
                    this.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        public void EnableWeapon(GameObject vi)
        {
            for (int i = 0; i < _Weapons.Count; i++)
            {
                if (_Weapons[i].GetComponent<LW_WeaponIndex>().weaponIndex == 
                    vi.GetComponent<LW_WeaponIndex>().weaponIndex)
                {
                    _Weapons[i].gameObject.SetActive(true);
                    _TempWeapon = vi;
                    vi.SetActive(false);
                }
            }

            Transform p = this.transform.parent;
            for (int i = 0; i < p.childCount; i++)
            {
                if(p.GetChild(i).GetComponent<SteamVR_RenderModel>())
                {
                    p.GetChild(i).gameObject.SetActive(false);
                    return;
                }
            }
        }

        public void DisableWeapon(LW_WeaponIndex vi)
        {
            for (int i = 0; i < _Weapons.Count; i++)
            {
                if (_Weapons[i].GetComponent<LW_WeaponIndex>().weaponIndex == vi.weaponIndex)
                {
                    _Weapons[i].gameObject.SetActive(false);
                }
            }
        }
    }
}

