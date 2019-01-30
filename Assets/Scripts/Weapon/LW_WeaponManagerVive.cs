using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Weapon;
using LittleWarrior.Managers;

namespace LittleWarrior.Weapon
{
    public class LW_WeaponManagerVive : MonoBehaviour
    {
        //private LW_PlayerInventory PI;
        //private Transform[] _Containers;
        //private List<LW_Weapon> _AvailableWeapons;
        //private List<GameObject> _OnBoardWeapons;
        
        //private void Awake()
        //{
        //    int childCount = this.transform.childCount;
        //    _Containers = new Transform[childCount];
        //    for(int i = 0; i < childCount; i++)
        //    {
        //        _Containers[i] = this.transform.GetChild(i).transform;
        //    }
        //}

        //void Start()
        //{
        //    _AvailableWeapons = new List<LW_Weapon>();
        //    _OnBoardWeapons = new List<GameObject>();
        //    Invoke("Initialize", 1.0f);
        //}

        //private void Initialize()
        //{
        //    PI = LW_PlayerInventory.Instance;
        //    _AvailableWeapons = PI.purchasedWeapons;
        //    //GameObject[] rgo = Resources.LoadAll("", typeof(GameObject));
        //    for (int i = 0; i < _AvailableWeapons.Count; i++)
        //    {
        //        for(int j = 0; j < rgo.Length; j++)
        //        {
        //            if(_AvailableWeapons[i].GetComponent<LW_Weapon>().weaponType ==
        //      //          rgo[j].get)
        //        }
        //        GameObject go = Instantiate(_AvailableWeapons[i].gameObject, _Containers[i].transform);
        //        go.SetActive(true);
        //        _OnBoardWeapons.Add(go);
        //    }
        //}
    }
}

