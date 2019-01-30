using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleWarrior.Weapon
{
    public class LW_WeaponManager : LW_Singleton<LW_WeaponManager>
    {
        protected LW_WeaponManager() {}

        [SerializeField]
        private List<GameObject> _Weapons;
        [SerializeField]
        private float _SwitchDelay;
        private int _Index;
        private bool _IsSwitching;

        private void Start()
        {
            _Weapons = new List<GameObject>();
            _Index = 0;
            //InitializeWeapons();
        }


        private void Update()
        {
            //if (Input.GetAxis("Mous ScrolWheel") > 0.0f &&
            //   !_IsSwitching)
            //{
            //    _Index++;

            //    if (_Index > _Weapons.Count - 1)
            //    {
            //        _Index = 0;
            //    }
            //    StartCoroutine(SwitchingAfterDelay(_Index));
            //}
            //else if (Input.GetAxis("Mous ScrolWheel") < 0.0f &&
            //         !_IsSwitching)
            //{
            //    _Index--;

            //    if (_Index < 0)
            //    {
            //        _Index = _Weapons.Count - 1;
            //    }
            //    StartCoroutine(SwitchingAfterDelay(_Index));
            //}
        }

        private void InitializeWeapons()
        {
            for(int i = 0; i < _Weapons.Count - 1; i++)
            {
                _Weapons[i].SetActive(false);
            }
            _Weapons[_Index].SetActive(true);
        }

        private IEnumerator SwitchingAfterDelay(int nextWeaponIndex)
        {
            _IsSwitching = true;
            yield return new WaitForSeconds(_SwitchDelay);

            _IsSwitching = false;
            SwitchWeapon(nextWeaponIndex);
        }

        private void SwitchWeapon(int nextWeaponIndex)
        {
            for (int i = 0; i < _Weapons.Count - 1; i++)
            {
                _Weapons[i].SetActive(false);
            }
            _Weapons[nextWeaponIndex].SetActive(true);
        }
    }
}

