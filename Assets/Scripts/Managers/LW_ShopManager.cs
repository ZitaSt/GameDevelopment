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

        private void Awake()
        {
            int childcount = this.transform.childCount;
            _Containers = new Transform[childcount];
            for(int i = 0; i < childcount; i++)
            {
                _Containers[i] = this.transform.GetChild(i).transform;
            }

            GM = LW_GameManager.Instance;
            PL = LW_PlayerInventory.Instance;
            
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
                    _ProvidedGoods.Add(go);
                    j++;
                }
            }
        }

        private void Update()
        {
            foreach(GameObject go in _ProvidedGoods)
            {
                if(go.transform.parent != null &&
                    go.transform.parent.name == "HoldRight")
                {
                    text.text = go.GetComponent<LW_WeaponIndex>().price.ToString();
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
    }
}

