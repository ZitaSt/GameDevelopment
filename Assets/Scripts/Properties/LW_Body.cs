using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Properties;
using System;

namespace LittleWarrior.Properties
{
    public class LW_Body : LW_Health
    {
        public override void TakeDamage(float amount)
        {
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
        }
    }
}

