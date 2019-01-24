using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleWarrior.Properties
{
    public class LW_DontDestroy : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

}
