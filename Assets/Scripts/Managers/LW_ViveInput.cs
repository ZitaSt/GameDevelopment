using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace LittleWarrior.Managers
{
    public class LW_ViveInput : MonoBehaviour
    {
        public SteamVR_Controller.Device device;

        private SteamVR_TrackedObject _TrackedObject = null;

        private void Awake()
        {
            _TrackedObject = GetComponent<SteamVR_TrackedObject>();
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
            }

            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                print("Trigger Up");
            }

            Vector2 triggerValue = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger);
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


