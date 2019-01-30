using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LittleWarrior.Managers;

namespace LittleWarrior.Properties
{
    public class LW_POS : MonoBehaviour
    {
        private Text text;
        private LW_PlayerInventory PL;

        private void Start()
        {
            Invoke("Initialize", 1.5f);
        }

        private void Initialize()
        {
            text = GetComponentInChildren<Text>();
            PL = LW_PlayerInventory.Instance;
            text.text = "Your Current Balance:";
            text.text += "\n" + PL.GetCurrencyAmount(Enums.Currency.Dollar);
        }

        public void UpdateBalance()
        {
            text.text = "Your Current Balance:";
            text.text += "\n" + PL.GetCurrencyAmount(Enums.Currency.Dollar);
        }
    }
}

