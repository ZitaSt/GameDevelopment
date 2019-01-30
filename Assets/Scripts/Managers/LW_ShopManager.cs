using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Weapon;
using LittleWarrior.Managers;
using UnityEngine.UI;

namespace LittleWarrior.Managers
{

    public class LW_ShopManager : MonoBehaviour
    {
        public List<GameObject> weaponsList = new List<GameObject>();
        private Transform[] _Containers;
        private LW_GameManager GM;
        private List<GameObject> _ProvidedGoods = new List<GameObject>();
        public Text text;
        private LW_PlayerInventory PL;
        private GameObject _G;
        private int _GI;

        private void Awake()
        {
            int childcount = this.transform.childCount;
            _Containers = new Transform[childcount];
            for(int i = 0; i < childcount; i++)
            {
                _Containers[i] = this.transform.GetChild(i).transform;
            }            
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
                else
                {
                    GameObject go = Instantiate(weaponsList[i], _Containers[j].transform);
                    _ProvidedGoods.Add(go);
                    j++;
                }
            }

            GM = LW_GameManager.Instance;
            PL = LW_PlayerInventory.Instance;
        }

        private void Update()
        {
            foreach(GameObject go in _ProvidedGoods)
            {
                if(go == null)
                {
                    return;
                }
                if(go.transform.parent != null &&
                    go.transform.parent.name == "HoldRight")
                {
                    text.text = "Price: " + go.GetComponent<LW_WeaponIndex>().price.ToString();
                    if(go.transform.tag == "ThrowingWeapon")
                    {
                        _G = go;
                        _GI = _ProvidedGoods.IndexOf(go);
                        InvokeRepeating("GenerateGrenade", 0.0f, 3.0f);
                    }
                }
            }
        }

        public void Sell()
        {
            GameObject ww = null;
            foreach (GameObject go in _ProvidedGoods)
            {
                if (go.transform.parent != null &&
                    go.transform.parent.name == "HoldRight")
                {
                    ww = go;
                }
            }
            if(ww != null)
            {
                LW_Weapon w = ww.GetComponent<LW_Weapon>();
                LW_WeaponIndex wi = ww.GetComponent<LW_WeaponIndex>();
                PL.ConsumeCurrency(Enums.Currency.Dollar, wi.price);
                PL.PurchaseWeapon(w);
            }
        }

        private void GenerateGrenade()
        {
            if(_G == null)
            {
                _ProvidedGoods.RemoveAt(_GI);
                for(int i = 0; i < _Containers.Length; i++)
                {
                    if(_Containers[i].childCount == 0)
                    {
                        for(int j = 0; j < weaponsList.Count; j++)
                        {
                            if(weaponsList[j].tag == "ThrowingWeapon")
                            {
                                GameObject go = Instantiate(weaponsList[j], _Containers[i].transform);
                                _ProvidedGoods.Add(go);
                                CancelInvoke("GenerateGrenade");
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}

