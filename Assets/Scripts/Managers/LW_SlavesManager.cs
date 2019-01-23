using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleWarrior.Managers
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class LW_SlavesManager : MonoBehaviour
    {
        public abstract void SetSlaveActive();
    }
}

