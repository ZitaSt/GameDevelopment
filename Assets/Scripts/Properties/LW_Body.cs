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
            _VFXApplier = this.GetComponentInParent<LW_VFX_FlashDamage>();
            //InvokeRepeating("DummyDamage", 1.0f, 3.0f);
        }
        public override void TakeDamage(float amount)
        {
            this._HealthProperty.TakeDamage(amount * _DamageFactor);
            _VFXApplier.Flash(2);

        }

        private void DummyDamage()
        {
            TakeDamage(10);
        }
    }
}

