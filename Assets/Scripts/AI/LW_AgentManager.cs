using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using LittleWarrior.Properties;
using LittleWarrior.Gameplay;

namespace LittleWarrior.AI
{
    public class LW_AgentManager : MonoBehaviour
    {
        [Header("Gameplay: Properties")]
        public int rewardDollar;

        [Header("Movement Stats")]
        public float followingDistance;
        public float stopppingDistance;

        [Header("Gameplay: Fighting Mechanism")]
        public float attackingDistance;
        public float attackProbability;
        public float attackPerMinute;
        public float damagePoints;
        public float difficultyFactor;

        [Header("Authestics")]
        public AudioClip attackSound;
        public AudioClip deathSound;
        public AudioClip idleSound;
        public AudioClip MovementSound;

        private LW_Health _Health;
        private LW_Health _TargetHealth;
        private NavMeshAgent _Nav;
        private Transform _Target;
        private Animator _Anim;
        private AnimatorStateInfo _AnimInfo;
        private AudioSource _AudioSource;

        private float _LastPlayedMovementSoundEffect = 0.0f;
        private float _LastPlayedIdleSoundEffect = 0.0f;

        private float _MovementSoundEffectInterval = 4.0f;
        private float _IdleSoundEffectInterval = 3.0f;
        private float _Distance;

        private bool _IsBehindAWall = false;
        private bool _IsAttack = false;
        private bool _IsFollowing = false;
        private bool _Idle = false;
        private bool _IsAttacking = false;

        private float _DamagePerSecond;
        private float _LastTimeAttacked = 0;

        void Awake()
        {
            _Nav = GetComponent<NavMeshAgent>();
            _Target = GameObject.FindGameObjectWithTag("Player").transform;
            _TargetHealth = _Target.GetComponent<LW_Health>();
            _Health = this.GetComponent<LW_Health>();
            _Anim = this.GetComponent<Animator>();
            _AudioSource = this.GetComponent<AudioSource>();
        }

        void Start()
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            _Nav.SetDestination(this.transform.position);
            _DamagePerSecond = 60.0f / attackPerMinute;
        }

        void Update()
        {
            _IsAttack = false;
            _Idle = false;
            _IsFollowing = false;

            _Distance = Vector3.Distance(_Target.transform.position,
                                  this.transform.position);
            float random = Random.Range(0.0f, 1.0f);

            if (_Nav.enabled && !_Nav.isStopped)
            {
                _IsFollowing = (_Distance < followingDistance &&
                               _Distance > stopppingDistance);

                if (random > (1 - attackProbability) && _Distance <= attackingDistance)
                {
                    _IsFollowing = false;
                    _IsAttack = true;
                }

                if(_IsFollowing)
                {
                    if (_TargetHealth.currentHealth > 0 && _Health.currentHealth > 0)
                    {
                        _Nav.SetDestination(_Target.position);
                        if(_LastPlayedMovementSoundEffect >= _MovementSoundEffectInterval)
                        {
                            PlayMovementSoundEffect();
                            _LastPlayedMovementSoundEffect = 0.0f;
                        }
                    }
                    else
                    {
                        _IsFollowing = false;
                    }
                }

                if(_IsAttack)
                {
                    this.transform.LookAt(_Target.transform);
                    _Nav.SetDestination(this.transform.position);
                    _LastTimeAttacked += Time.deltaTime;
                    if(_LastTimeAttacked > _DamagePerSecond)
                    {
                        _TargetHealth.TakeDamage(damagePoints);
                        _LastTimeAttacked = 0;
                    }
                    
                }

                if(!_IsFollowing && !_IsAttack)
                {
                    _Idle = true;
                    _Nav.SetDestination(this.transform.position);
                    if (_LastPlayedIdleSoundEffect >= _IdleSoundEffectInterval)
                    {
                        PlayIdleSoundEffect();
                        _LastPlayedIdleSoundEffect = 0.0f;
                    }
                    _LastTimeAttacked = 0;
                }

                _Anim.SetBool("Attack", _IsAttack);
                _Anim.SetBool("Walk", _IsFollowing);
                _Anim.SetBool("Idle", _Idle);

                _LastPlayedMovementSoundEffect += Time.deltaTime;
                _LastPlayedIdleSoundEffect += Time.deltaTime;
                if(_LastPlayedIdleSoundEffect >= 30)
                {
                    _LastPlayedIdleSoundEffect = 0;
                }
                if(_LastPlayedMovementSoundEffect >= 30)
                {
                    _LastPlayedMovementSoundEffect = 0;
                }
            }
            else if(_Nav.isStopped && _IsBehindAWall)
            {
                _IsAttack = true;
                _Nav.SetDestination(this.transform.position);
                _Anim.SetBool("Attack", _IsAttack);
                _Anim.SetBool("Walk", _IsFollowing);
                _Anim.SetBool("Idle", _Idle);
            }

            if (_Health.currentHealth == 0)
            {
                _Nav.isStopped = true;
                HandleColliders();
                _Anim.SetTrigger("Die");
                PlayDeathSoundEffet();
                gameObject.SetActive(false);
            }

            //_LastTimeAttacked += Time.deltaTime;
        }

        void FixedUpdate()
        {
            _AnimInfo = _Anim.GetCurrentAnimatorStateInfo(0);

            _IsAttacking = _AnimInfo.IsName("Attack");
        }

        private void OnTriggerStay(Collider col)
        {
            if(col.gameObject.tag == "Board")
            {
                if(col.gameObject.GetComponent<LW_BoardsManager>().GetBoardsCount() > 0)
                {
                    _IsBehindAWall = true;
                    _Nav.isStopped = true;
                    if(_IsAttack)
                    {
                        col.gameObject.GetComponent<LW_BoardsManager>().RemoveBoard();
                    }
                }
                else
                {
                    _Nav.isStopped = false;
                    _IsBehindAWall = false;
                }
            }
            else
            {
                _Nav.isStopped = false;
                _IsBehindAWall = false;
            }
        }

        private void OnTriggerExit(Collider col)
        {
            _Nav.isStopped = false;
            _IsBehindAWall = false;
        }

        void PlayAttackingSoundEffect()
        {
            _AudioSource.PlayOneShot(attackSound);
        }

        private void PlayDeathSoundEffet()
        {
            _AudioSource.clip = deathSound;
            _AudioSource.Play();
        }

        private void PlayMovementSoundEffect()
        {
            _AudioSource.clip = MovementSound;
            _AudioSource.Play();
        }
        
        private void PlayIdleSoundEffect()
        {
            _AudioSource.clip = idleSound;
            _AudioSource.Play();
        }

        private void HandleColliders()
        {
            var scc = this.GetComponent<CapsuleCollider>();
            var hsc = this.GetComponentInChildren<SphereCollider>();
            var bcc = this.GetComponentInChildren<CapsuleCollider>();
            if (scc)
            {
                scc.isTrigger = true;
            }
            if (hsc)
            {
                hsc.isTrigger = true;
            }
            if (bcc)
            {
                bcc.isTrigger = true;
            }
        }

        private void Attack()
        {
            if (_LastTimeAttacked > _DamagePerSecond)
            {
                _IsFollowing = false;
                _IsAttack = true;
                _LastTimeAttacked = 0;
            }
            else
            {
                _IsFollowing = false;
                _IsAttack = false;
            }
        }
    }
}

