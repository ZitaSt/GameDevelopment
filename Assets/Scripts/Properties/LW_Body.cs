using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Properties;
using System;
using LittleWarrior.VFX;

namespace LittleWarrior.Properties
{
    public class LW_Body : LW_Target
    {
        private void Awake()
        {
            _HealthProperty = GetComponentInParent<LW_Health>();
            _DamageFactor = 1.0f;
            InvokeRepeating("TakeDummyDamage", 1.0f, 1.0f);
            _VFXApplier = this.GetComponentInParent<LW_VFX_FlashDamage>();
        }
        public override void TakeDamage(float amount)
        {
            this._HealthProperty.TakeDamage(amount * _DamageFactor);
            _VFXApplier.Flash(2);

        }

        private void TakeDummyDamage()
        {
            TakeDamage(25.0f);
        }
    }
}

