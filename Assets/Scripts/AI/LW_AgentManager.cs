using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using LittleWarrior.Properties;

namespace LittleWarrior.AI
{
    public class LW_AgentManager : MonoBehaviour
    {
        private LW_Health _Health;
        private LW_Health _TargetHealth;
        private NavMeshAgent _Nav;
        private Transform _Target;

        private Animator _Anim;

        private float _AttackingDistance;
        private float _FollowingDistance;
        private float _AttackProbability;
        private float _DamagePoints;

        [Header("Authestics")]
        public AudioClip attackSound;



        void Awake()
        {
            _Nav = GetComponent<NavMeshAgent>();
            _Target = GameObject.FindGameObjectWithTag("Player").transform;
            _TargetHealth = _Target.GetComponent<LW_Health>();
            _Health = this.GetComponent<LW_Health>();
            _Anim = this.GetComponent<Animator>();
        }

        void Start()
        {
            //TODO (skn): Read data from file
            _FollowingDistance = 500.0f;
            _AttackProbability = 80.0f;
            _AttackingDistance = 1.0f;
        }

        void Update()
        {
            if(_Nav.enabled)
            {
                Debug.Log("I am here!");
                float distance = Vector3.Distance(_Target.transform.position,
                                                  this.transform.position);
                bool attack = false;
                bool follow = (distance < _FollowingDistance);

                if(follow)
                {
                    float random = Random.Range(0.0f, 1.0f);
                    if (random > (1 - _AttackProbability) && distance < _AttackingDistance)
                    {
                        attack = true;
                    }
                }

                if(follow)
                {
                    if (_TargetHealth.currentHealth > 0 && _Health.currentHealth > 0)
                    {
                        _Nav.SetDestination(_Target.position);
                    }
                }

                if(!follow || attack)
                {
                    this.transform.LookAt(_Target.transform);
                    _Nav.SetDestination(this.transform.position);
                }

                _Anim.SetBool("Attack", attack);
                _Anim.SetBool("Walk", follow);
            }

            if (_Health.currentHealth == 0)
            {
                Destroy(gameObject);
            }
        }

        private void PlayAttackingSoundEffect()
        {
            //_AudioSource.clip = shootSoundEffect;
            //_AudioSource.Play();
        }
    }
}

