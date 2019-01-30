using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Weapon;

namespace LittleWarrior.Weapon
{
    public class LW_GrenadeThrower : MonoBehaviour
    {
        public float throwingForce = 10.0f;


        public GameObject Clone()
        {
            return Instantiate(this.gameObject, this.transform.parent);
        }

        public void ThrowTheGrenade(Vector3 fv)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddForce(fv * throwingForce, ForceMode.VelocityChange);
            this.GetComponent<LW_Grenade>().isThrowed = true;
        }
    }
}

