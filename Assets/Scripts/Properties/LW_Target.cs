using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.VFX;

namespace LittleWarrior.Properties
{
    public abstract class LW_Target : MonoBehaviour
    {
        protected float _DamageFactor;
        protected LW_Health _HealthProperty;
        protected LW_VFX_FlashDamage _VFXApplier;

        public abstract void TakeDamage(float amount);

    }
}
