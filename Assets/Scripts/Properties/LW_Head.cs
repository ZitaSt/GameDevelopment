using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.VFX;

namespace LittleWarrior.Properties
{
    public class LW_Head : LW_Target
    {
        private void Awake()
        {
            _HealthProperty = GetComponentInParent<LW_Health>();
            _DamageFactor = 1.5f;
            _VFXApplier = this.GetComponentInParent<LW_VFX_FlashDamage>();
        }
        public override void TakeDamage(float amount)
        {
            this._HealthProperty.TakeDamage(amount * _DamageFactor);
            _VFXApplier.Flash(1);
        }
    }
}
    

