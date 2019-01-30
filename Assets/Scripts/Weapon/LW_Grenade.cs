using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Weapon;
using LittleWarrior.Properties;

namespace LittleWarrior.Weapon
{
    public class LW_Grenade : MonoBehaviour
    {
        public float delayBeforeExplosion = 3.0f;
        public float blastRadius = 3.0f;
        public float explosionForce = 700.0f;
        public float damagePoints = 10.0f;
        public bool isThrowed = false;

        public GameObject explosionEffect;

        public AudioClip explosionSound;

        private float _ExplosionCountdown;
        private bool _HasExploded = false;

        private AudioSource _AudioSource;

        void Awake()
        {
            _AudioSource = this.GetComponent<AudioSource>();
        }

        private void Start()
        {
            _ExplosionCountdown = delayBeforeExplosion;
        }

        private void Update()
        {
            if(isThrowed)
            {
                _ExplosionCountdown -= Time.deltaTime;
                if(_ExplosionCountdown <= 0 &&
                    !_HasExploded)
                {
                    Explde();
                    _HasExploded = true;
                }
            }
        }

        private void Explde()
        {
            if(explosionEffect)
            {
                Instantiate(explosionEffect, transform.position,
                            transform.rotation);
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
                //PlayExplosionSoundEffect();
            }
            else
            {
                Debug.LogError("!!!!: No explosion effect is set!");
            }


            Collider[] col = Physics.OverlapSphere(transform.position, blastRadius);
            foreach(Collider c in col)
            {
                if(c.gameObject.layer == 10)
                {
                    Rigidbody rb = c.GetComponent<Rigidbody>();
                    if (rb)
                    {
                        rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
                    }

                    var hp = c.GetComponent<LW_Target>();
                    if (hp)
                    {
                        Vector3 cpp = c.ClosestPoint(transform.position);
                        float distance = (cpp - transform.position).sqrMagnitude;
                        hp.TakeDamage((distance * damagePoints) / blastRadius);
                    }
                }

            }

            Destroy(gameObject);
        }

        /*private void PlayExplosionSoundEffect()
        {
            _AudioSource.clip = explosionSound;
            _AudioSource.Play();
        }*/

    }
}

