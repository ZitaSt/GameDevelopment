using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleWarrior.Weapon
{
    public class LW_WeaponSway : MonoBehaviour
    {
        public float swayAmount;
        public float swaysmoothnes;
        public float swayRange;

        private Vector3 _DefaultPosition;

        private void Start()
        {
            _DefaultPosition = transform.localPosition;
        }

        private void Update()
        {
            float movementX = -Input.GetAxis("Mouse X") * swayAmount;
            float movementY = -Input.GetAxis("Mouse Y") * swayAmount;
            movementX = Mathf.Clamp(movementX, -swayRange, swayRange);
            movementY = Mathf.Clamp(movementY, -swayRange, swayRange);


            Vector3 finlaPosition = new Vector3(movementX, movementY, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition,
                                                   finlaPosition + _DefaultPosition,
                                                   Time.deltaTime * swaysmoothnes); 
        }
    }
}

