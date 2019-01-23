using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Weapon;

namespace LittleWarrior.Weapon
{
    public class LW_GrenadeThrower : MonoBehaviour
    {
        public float throwingForce = 40.0f;

        private void ThrowTheGrenade()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwingForce, ForceMode.VelocityChange);
        }
    }
}

