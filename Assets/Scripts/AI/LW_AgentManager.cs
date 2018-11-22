using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace LittleWarrior.AI
{
    public class LW_AgentManager : MonoBehaviour
    {
        NavMeshAgent nav;
        Transform target;
        public float Health = 100.0f;

        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            nav.SetDestination(target.position);
        }
    }
}

