using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Slaves;
using LittleWarrior.Managers;
using LittleWarrior.Weapon;
using System;

namespace LittleWarrior.Properties
{
    public class LW_ViveInteraction : MonoBehaviour
    {
        public Transform holdingObjectPosition;
        private FixedJoint _AttachJoint = null;

        private Rigidbody _CurrentRigidBody = null;
        private List<Rigidbody> _ContactRigidBodies = new List<Rigidbody>();


        void Awake()
        {
            _AttachJoint = GetComponent<FixedJoint>();
        }

        void OnTriggerEnter(Collider collider)
        {
            if (!collider.gameObject.CompareTag("Interactable") &&
                !collider.gameObject.CompareTag("ThrowingWeapon"))
            {
                return;
            }
            if(collider.transform.parent == holdingObjectPosition)
            {
                return;
            }
            _ContactRigidBodies.Add(collider.gameObject.GetComponent<Rigidbody>());
        }

        void OnTriggerExit(Collider collider)
        {
            if (!collider.gameObject.CompareTag("Interactable") &&
                !collider.gameObject.CompareTag("ThrowingWeapon"))
            {
                return;
            }

            _ContactRigidBodies.Remove(collider.gameObject.GetComponent<Rigidbody>());
        }

        public GameObject GetCurrentObject()
        {
            if(!_CurrentRigidBody)
            {
                return null;
            }
            else
            {
                return _CurrentRigidBody.gameObject;
            }
        }

        public void RightGripPressed()
        {
            _CurrentRigidBody = GetNearestRigidbody();

            if (!_CurrentRigidBody)
            {
                return;
            }

            if(_CurrentRigidBody.gameObject.layer == 11)
            {
                PickUp();
            }
            else if(_CurrentRigidBody.gameObject.layer == 12)
            {
                _CurrentRigidBody.gameObject.GetComponent<LW_SlavesManager>().SetSlaveActive();

                _CurrentRigidBody = null;
            }
            else if(_CurrentRigidBody.gameObject.layer == 14)
            {
                WeaponPickUp();
            }
            else
            {
                return;
            }
        }

        public void LeftGripPressed()
        {
            return;
        }

        public void RightGripReleased(SteamVR_Controller.Device device)
        {
            if (!_CurrentRigidBody)
            {
                return;
            }

            if (_CurrentRigidBody.gameObject.layer == 11)
            {
                Drop(device);
            }
            else if(_CurrentRigidBody.gameObject.layer == 14)
            {
                Drop(device);
            }
            else
            {
                _CurrentRigidBody = null;
                return;
            }
        }

        public void PickUp()
        {
            //_CurrentRigidBody = GetNearestRigidbody();

            //if (!_CurrentRigidBody)
            //{
            //    return;
            //}

            _CurrentRigidBody.transform.position = holdingObjectPosition.transform.position;
            _CurrentRigidBody.transform.SetParent(holdingObjectPosition.transform);
            _AttachJoint.connectedBody = _CurrentRigidBody;
        }

        public void WeaponPickUp()
        {
            _CurrentRigidBody.transform.position = holdingObjectPosition.transform.position;
            _CurrentRigidBody.transform.SetParent(holdingObjectPosition.transform);
            _AttachJoint.connectedBody = _CurrentRigidBody;
            _CurrentRigidBody.transform.forward = holdingObjectPosition.transform.forward;
            //_CurrentRigidBody.GetComponent<BoxCollider>().enabled = false;
            holdingObjectPosition.GetComponent<LW_RightHand>().EnableWeapon(_CurrentRigidBody.gameObject);
            for (int i = 0; i < _ContactRigidBodies.Count; i++)
            {
                if (_CurrentRigidBody.transform.GetInstanceID() ==
                    _ContactRigidBodies[i].transform.GetInstanceID())
                {
                    _ContactRigidBodies.RemoveAt(i);
                }
            }

            _AttachJoint.connectedBody = null;
            _CurrentRigidBody = null;
            
        }

        public void Drop(SteamVR_Controller.Device device)
        {
            //if (!_CurrentRigidBody)
            //{
            //    return;
            //}

            if(_CurrentRigidBody.transform.CompareTag("Interactable"))
            {
                _CurrentRigidBody.velocity = device.velocity;
                _CurrentRigidBody.angularVelocity = device.angularVelocity;

                LW_ObjectReturn objectReturn = _CurrentRigidBody.gameObject.GetComponent<LW_ObjectReturn>();
                objectReturn.SetLastInteraction(this);
            }
            else if(_CurrentRigidBody.transform.CompareTag("ThrowingWeapon"))
            {
                GameObject go = _CurrentRigidBody.GetComponent<LW_GrenadeThrower>().Clone();
                
                go.GetComponent<Rigidbody>().velocity = device.velocity;
                go.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;

                go.GetComponent<Rigidbody>().GetComponent<LW_GrenadeThrower>().ThrowTheGrenade(holdingObjectPosition.transform.forward);
            }
            else
            {
                return;
            }

            _AttachJoint.connectedBody = null;
            _CurrentRigidBody.transform.parent = null;
            _CurrentRigidBody = null;
        }

        private Rigidbody GetNearestRigidbody()
        {
            Rigidbody nrb = null;

            float minDistance = float.MaxValue;
            float distance = 0.0f;

            foreach (Rigidbody rb in _ContactRigidBodies)
            {
                distance = (rb.gameObject.transform.position - transform.position).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nrb = rb;
                }
            }

            return nrb;
        }

        public virtual void OnPickUp(LW_ViveInteraction newInteraction)
        {
        }

        public virtual void OnDrop(LW_ViveInteraction newInteraction)
        {
        }

        public void ResetTheContactRigidBodies()
        {
            for(int i = 0; i < _ContactRigidBodies.Count; i++)
            {
                _ContactRigidBodies.RemoveAt(i);
            }
        }
    }
}

