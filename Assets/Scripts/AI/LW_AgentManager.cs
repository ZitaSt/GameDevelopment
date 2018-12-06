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


        void Awake()
        {
            _Nav = GetComponent<NavMeshAgent>();
            _Target = GameObject.FindGameObjectWithTag("Player").transform;
            _TargetHealth = _Target.GetComponent<LW_Health>();
            _Health = this.GetComponent<LW_Health>();
        }

        void Update()
        {
            if (_TargetHealth.currentHealth > 0 && _Health.currentHealth > 0)
            {
                _Nav.SetDestination(_Target.position);
            }
            else if (_Health.currentHealth == 0)
            {
                Destroy(gameObject);
            }
            else {
                _Nav.enabled = false;
            }

        }
    }
}

