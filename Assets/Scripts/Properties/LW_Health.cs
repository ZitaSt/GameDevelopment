using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleWarrior.Properties
{
    public abstract class LW_Health : MonoBehaviour
    {
        [SerializeField]
        private const float maxHealth = 100.0f;
        public float currentHealth { get; private set; }

        void Awake()
        {
            currentHealth = maxHealth;
        }

        public abstract void TakeDamage(float amount);
    }
}

