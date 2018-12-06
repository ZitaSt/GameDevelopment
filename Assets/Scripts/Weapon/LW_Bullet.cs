using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Properties;


namespace LittleWarrior.Weapon
{
    public class LW_Bullet : MonoBehaviour
    {
        public float Speed
        {
            get
            {
                return _Speed;
            }
            set
            {
                if (_Speed == 0)
                {
                    _Speed = value;
                }
                else
                {
                    Debug.Log("XX Not allowed to change" +
                              " speed.");
                }
            }
        }
        public float Damage
        {
            get
            {
                return _Damage;
            }
            set
            {
                if (_Damage == 0)
                {
                    _Damage = value;
                }
                else
                {
                    Debug.Log("XX Not allowed to change" +
                              " damage.");
                }
            }
        }

        private float _Speed;
        private float _Damage;

        public GameObject hitParticle;
        public GameObject bulletImpact;

        private void Start()
        {
            this.GetComponent<Rigidbody>().velocity = transform.forward * _Speed;
        }

        void OnCollisionEnter(Collision col)
        {
            var hit = col.gameObject;
            var health = hit.GetComponent<LW_Health>();
            if(health != null)
            {
                GameObject hpar = Instantiate(hitParticle,
                                        this.transform.position,
                                        Quaternion.FromToRotation(Vector3.up, 
                                        col.transform.position.normalized));
                health.TakeDamage(_Damage);
                Destroy(hpar, 0.4f);
            }
            else
            {
                GameObject hpar = Instantiate(hitParticle,
                                        this.transform.position,
                                        Quaternion.FromToRotation(Vector3.up,
                                        col.transform.position.normalized));
                Destroy(hpar, 0.4f);
                GameObject bimp = Instantiate(bulletImpact,
                                        this.transform.position,
                                        Quaternion.FromToRotation(Vector3.forward,
                                        col.transform.position.normalized));

                Vector3 v = bimp.transform.rotation.eulerAngles;
                bimp.transform.rotation = Quaternion.Euler(0, 0, v.z);
                bimp.transform.position = new Vector3(bimp.transform.position.x,
                                                      bimp.transform.position.y,
                                                      bimp.transform.position.z + 0.06f);

                Destroy(bimp, 10f);
                Debug.Break();
                //TODO (skn): Register particles to a proper manager
            }


            Destroy(gameObject);
        }
    }
    

}

