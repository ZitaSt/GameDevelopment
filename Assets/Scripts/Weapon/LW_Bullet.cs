using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                if (_Speed == int.MinValue &&
                    value != int.MinValue)
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

        private float _Speed;

        void Start()
        {

        }
        void Update()
        {
            //this.transform.position = new Vector3(this)
        }
    }
    

}

