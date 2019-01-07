using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using LittleWarrior.Weapon;
using LittleWarrior.Properties;

namespace LittleWarrior.Managers
{
    public class LW_ViveInput : MonoBehaviour
    {
        public SteamVR_Controller.Device device;

        private SteamVR_TrackedObject _TrackedObject = null;
        private LW_Weapon _CurrentWeapon = null;
        private LW_ViveInteraction _InteractableObject = null;

        private void Awake()
        {
            _TrackedObject = GetComponent<SteamVR_TrackedObject>();

            _CurrentWeapon = GetComponentInChildren<LW_Weapon>();
        }

        // Update is called once per frame
        void Update()
        {
            device = SteamVR_Controller.Input((int)_TrackedObject.index);

            #region Trigger
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                print("Trigger");
            }

            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                print("Trigger Down");
                if(_CurrentWeapon == null)
                {
                    _CurrentWeapon = GetComponentInChildren<LW_Weapon>();
                }
                _CurrentWeapon.RightTriggerPressed();
                //_InteractableObject.PickUp();
            }

            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                print("Trigger Up");
                //_InteractableObject.Drop(device);
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
                print("Grip");
            }

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                print("Grip Down");
            }

            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {
                print("Grip Up");
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
    }
}


