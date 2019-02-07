using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Weapon;
using LittleWarrior.Slaves;
using LittleWarrior.Properties;
using LittleWarrior.Managers;

namespace LittleWarrior.Managers
{
    public class LW_RightHand : MonoBehaviour
    {
        private List<GameObject> _Weapons = new List<GameObject>();
        private GameObject _TempWeapon = null;
        private bool _HoldsWeapon = false;
        private int _ActiveWeaponIndex = 0;

        private LW_PlayerInventory PI;

        private bool switched;

        private void Start()
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (this.transform.GetChild(i).GetComponent<LW_WeaponIndex>())
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
                    break;
                }
            }
            p.GetComponent<LW_ViveInput>().UpdateCurrentWeapon(_Weapons[_ActiveWeaponIndex]);
        }

        public void DisableWeapon()
        {
            _Weapons[_ActiveWeaponIndex].gameObject.SetActive(false);
            _TempWeapon.transform.parent = null;
            _TempWeapon.SetActive(true);
            Transform p = this.transform.parent;
            _TempWeapon.GetComponent<LW_ObjectReturn>().SetLastInteraction(p.GetComponent<LW_ViveInteraction>());
            
        }

        public LW_Weapon ActiveWeapon()
        {
            return _Weapons[_ActiveWeaponIndex].GetComponent<LW_Weapon>();
        }

        public void SwitchWeapon()
        {
            switched = false;

            for(int i = 0; i < _Weapons.Count; i++)
            {
                PI = LW_PlayerInventory.Instance;
                List<LW_Weapon> pw = PI.purchasedWeapons;
                for(int j = 0; j < pw.Count; j++)
                {
                    if (!switched)
                    {
                       if ( (_Weapons[i].GetComponent<LW_WeaponIndex>().weaponIndex == pw[j].GetComponent<LW_WeaponIndex>().weaponIndex) &&
                            (_ActiveWeaponIndex != i) )
                        {
                            _Weapons[_ActiveWeaponIndex].gameObject.SetActive(false);
                            _ActiveWeaponIndex = i;
                            _Weapons[_ActiveWeaponIndex].gameObject.SetActive(true);
                            switched = true;
                        }
                    }
                }
            }
        }
    }
}

