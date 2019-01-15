using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using LittleWarrior.Weapon;
using LittleWarrior.Properties;
using LittleWarrior.Enums;

namespace LittleWarrior.Managers
{
    public class LW_ViveInput : MonoBehaviour
    {
        public SteamVR_Controller.Device device;
        public Controller controller;

        private SteamVR_TrackedObject _TrackedObject = null;
        private LW_Weapon _CurrentWeapon = null;
        private LW_ViveInteraction _InteractableObject;


        private void Awake()
        {
            _TrackedObject = GetComponent<SteamVR_TrackedObject>();
            _InteractableObject = GetComponent<LW_ViveInteraction>();
            _CurrentWeapon = GetComponentInChildren<LW_Weapon>();
        }

        private void Update()
        {
            device = SteamVR_Controller.Input((int)_TrackedObject.index);

            #region Trigger
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                
            }

            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if(controller == Controller.Right)
                {
                    if (_CurrentWeapon == null)
                    {
                        _CurrentWeapon = GetComponentInChildren<LW_Weapon>();
                    }
                    _CurrentWeapon.RightTriggerPressed();
                }
                else if(controller == Controller.Left)
                {
                    return;
                }
            }

            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                
            }

            Vector2 triggerValue = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger);

            //TODO (skn): Can set the VIVE trigger rotation to the actual weapon trigger
            //            just uncomment this section and the region code applyactualtrigger
            //            in weapn class
            //_CurrentWeapon.SetTriggerRotation(triggerValue.x);
            #endregion

            #region Grip
            if (device.GetPress(SteamVR_Controller.ButtonMask.Grip))
            {
                
            }

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            { 
                if(controller == Controller.Right)
                {
                    _InteractableObject.RightGripPressed();
                }
                else if (controller == Controller.Left)
                {
                    return;
                }    
            }

            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {
                _InteractableObject.RightGripReleased(device);

            }
            #endregion

            #region Touchpad
            if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                print("Touchpad");
            }

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                print("Touchpad Down");
            }

            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                print("Touchpad Up");
            }

            Vector2 touchValue = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            #endregion
        }

        public GameObject collidingObject;
        public GameObject objectInHand;

        public void OnTriggerEnter(Collider col)
        {
            if(!col.GetComponent<Rigidbody>() ||
               col.gameObject.layer != 11)
            {
                return;
            }

            collidingObject = col.gameObject;
            
        }

        public void OnTriggerExit(Collider col)
        {
            collidingObject = null;
        }
    }
}


