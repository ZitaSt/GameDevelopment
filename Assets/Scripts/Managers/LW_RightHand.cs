using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Weapon;
using LittleWarrior.Slaves;
using LittleWarrior.Properties;

namespace LittleWarrior.Managers
{
    public class LW_RightHand : MonoBehaviour
    {
        private List<GameObject> _Weapons = new List<GameObject>();
        private GameObject _TempWeapon = null;
        private bool _HoldsWeapon = false;
        private int _ActiveWeaponIndex = 0;

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
                    if(_HoldsWeapon)
                    {
                        DisableWeapon();
                    }
                    _Weapons[i].gameObject.SetActive(true);
                    _ActiveWeaponIndex = i;
                    _TempWeapon = vi;
                    vi.SetActive(false);
                    _HoldsWeapon = true;
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

        public void DisableWeapon()
        {
            _Weapons[_ActiveWeaponIndex].gameObject.SetActive(false);
            _TempWeapon.transform.parent = null;
            _TempWeapon.SetActive(true);
            Transform p = this.transform.parent;
            _TempWeapon.GetComponent<LW_ObjectReturn>().SetLastInteraction(p.GetComponent<LW_ViveInteraction>());
            
        }
    }
}

