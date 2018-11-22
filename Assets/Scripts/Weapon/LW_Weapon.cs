using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Enums;
using LittleWarrior.Managers;

namespace LittleWarrior.Weapon
{
    public class LW_Weapon : MonoBehaviour
    {
        private Animator _Anim;
        private AnimatorStateInfo _AnimInfo;
        private AudioSource _AudioSource;
        private float _LastTimeFiredTimer;
        private bool _IsReloading = false;
        private Managers.LW_PlayerInventory MPI;
        private int _BulletsLeft;

        [Header("Weapn Config")]
        public Enums.WeaponTypes weaponType;
        public float shootingRange = 100.0f;
        public int bulletsPerMag = 12;
        public float fireRate = 0.1f;
        public Transform shootPoint;

        [Header("Weapn Authestic")]
        public ParticleSystem flare;
        public GameObject bulletsParticle;
        public GameObject bulletsImpact;
        public AudioClip shootSoundEffect;



        void Start()
        {
            _BulletsLeft = bulletsPerMag;
            _AudioSource = GetComponent<AudioSource>();
            MPI = Managers.LW_PlayerInventory.Instance;
            if(weaponType == null)
            {
                Debug.LogError("XXXX " + this.transform.name + " Missing weapon type.");
            }

            if (shootPoint == null)
            {
                Debug.LogError("XXXX " + this.transform.name + " - Missing Shoot Point.");
            }

            if(flare == null)
            {
                Debug.LogError("XXXX " + this.transform.name + " - Missing Flare Particle.");
            }

            if (shootSoundEffect == null)
            {
                Debug.LogError("XXXX " + this.transform.name + " - Missing Shooting SFX.");
            }

            _Anim = GetComponent<Animator>();
        }


        void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                if(_BulletsLeft > 0 && !_IsReloading)
                {
                    Fire();
                }
                else
                {
                    DoReload();
                }

            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                if (_BulletsLeft < bulletsPerMag)
                {
                    DoReload();
                }
                
            }

            if(_LastTimeFiredTimer < fireRate)
            {
                _LastTimeFiredTimer += Time.deltaTime; 
            }
        }

        void FixedUpdate()
        {
            _AnimInfo = _Anim.GetCurrentAnimatorStateInfo(0);

            _IsReloading = _AnimInfo.IsName("Reload");
        }

        private void Fire()
        {
            if(_LastTimeFiredTimer < fireRate ||
               _BulletsLeft <= 0)
            {
                return;
            }
            RaycastHit hit;
            if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, shootingRange))
            {
                Debug.Log(">>>> " + hit.transform.name + " is hited!");

            }
            _Anim.CrossFadeInFixedTime("Shoot", 0.01f);
            flare.Play();
            PlaySoundEffect();

            _BulletsLeft--;
            _LastTimeFiredTimer = 0.0f;
        }

        public void Reload()
        {
            int requiredBullets = bulletsPerMag - _BulletsLeft;
            int retrunedCode = MPI.ConsumeBullets(weaponType.ToString(),
                                                  requiredBullets);
            switch(retrunedCode)
            {
                case -1:
                    {
                        // NOTE (skn): Fatal wrong setting
                        Debug.LogError("XXXX - Consuming - Proper bullet type" + 
                                       weaponType.ToString()
                                       + " is not found.");
                    }
                    break;
                case 0:
                    {
                        // TODO (skn): Update UI with Out of Bullets
                        Debug.Log("Out of " + weaponType.ToString() + ".");
                    }
                    break;
                default:
                    {
                        _BulletsLeft += retrunedCode;
                    }
                    break;
            }
            return;
        }

        private void DoReload()
        {
            if(_IsReloading)
            {
                if(MPI.GetBulletsCount(weaponType.ToString()) <= 0)
                {
                    Debug.Log("Out of " + weaponType.ToString() + ".");
                    return;
                }
                return;
            }
            _Anim.CrossFadeInFixedTime("Reload", 0.01f);
        }

        private void PlaySoundEffect()
        {
            _AudioSource.clip = shootSoundEffect;
            _AudioSource.Play();
        }
    }
}

