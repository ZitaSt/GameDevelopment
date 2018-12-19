using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Enums;
using LittleWarrior.Managers;
using LittleWarrior.AI;

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
        public Enums.ShootingMode shootingMode;
        public GameObject bulletPrefab;
        public float shootingRange = 100.0f;
        public int bulletsPerMag = 12;
        public float bulletSpeed;
        public float fireRate = 0.1f;
        public Transform shootPoint;
        public float spreadFactor;
        public int bulletDamage;

        [Header("Weapn Authestic")]
        public ParticleSystem flare;
        public GameObject bulletsParticle;
        public GameObject bulletsImpact;
        public AudioClip shootSoundEffect;
        public AudioClip reloadSoundEffect;

        [Header("Debuging")]
        public Material debugMaterial;

        [Header("Vive Setting")]
        public GameObject controllerRight;
        public GameObject controllerLeft;

        private SteamVR_Controller.Device _RightDevice;
        private SteamVR_TrackedObject _RightTrackedObject;

        private SteamVR_TrackedController _RightController;
        


        void Start()
        {
            _BulletsLeft = bulletsPerMag;
            _AudioSource = GetComponent<AudioSource>();
            MPI = Managers.LW_PlayerInventory.Instance;

            _RightController = controllerRight.GetComponent<SteamVR_TrackedController>();
            _RightController.TriggerClicked += RightTriggerPressed;
            _RightTrackedObject = controllerRight.GetComponent<SteamVR_TrackedObject>();

            if(weaponType == null)
            {
                Debug.LogWarning("XXXX " + this.transform.name + " Missing weapon type.");
            }

            if (shootPoint == null)
            {
                Debug.LogWarning("XXXX " + this.transform.name + " - Missing Shoot Point.");
            }

            if(flare == null)
            {
                Debug.LogWarning("XXXX " + this.transform.name + " - Missing Flare Particle.");
            }

            if (shootSoundEffect == null)
            {
                Debug.LogWarning("XXXX " + this.transform.name + " - Missing Shooting SFX.");
            }

            _Anim = GetComponent<Animator>();
        }


        void Update()
        {
            //NOTE (skn): Fire and reload using keyboard
            //if (Input.GetButton("Fire1"))
            //{
            //    if(_BulletsLeft > 0 && !_IsReloading)
            //    {
            //        Fire();
            //    }
            //    else
            //    {
            //        DoReload();
            //    }

            //}
            //else if(Input.GetKeyDown(KeyCode.R))
            //{
            //    if (_BulletsLeft < bulletsPerMag)
            //    {
            //        DoReload();
            //    }
                
            //}

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

        private void RightTriggerPressed(object sender, ClickedEventArgs e)
        {
            if (_BulletsLeft > 0 && !_IsReloading)
            {
                Fire();
            }
            else
            {
                DoReload();
            }
        }

        private void LeftTriggerPressed(object sender, ClickedEventArgs e)
        {
            if (_BulletsLeft < bulletsPerMag)
            {
                DoReload();
            }
        }

        private void Fire()
        {
            if(_LastTimeFiredTimer < fireRate ||
               _BulletsLeft <= 0)
            {
                return;
            }
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            var bp = bullet.GetComponent<LW_Bullet>();
            bp.Speed = bulletSpeed;
            bp.Damage = bulletDamage;

            _RightDevice = SteamVR_Controller.Input((int)_RightTrackedObject.index);
            _RightDevice.TriggerHapticPulse(750);

            _Anim.CrossFadeInFixedTime("Shoot", 0.01f);
            flare.Play();
            PlayShootingSoundEffect();

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
                        PlayReloadSoundEffect();
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

        private void PlayShootingSoundEffect()
        {
            _AudioSource.clip = shootSoundEffect;
            _AudioSource.Play();
        }

        private void PlayReloadSoundEffect()
        {
            _AudioSource.clip = reloadSoundEffect;
            _AudioSource.Play();
        }
    }
}

