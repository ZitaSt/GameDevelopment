using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Slaves;

namespace LittleWarrior.Properties
{
    public class LW_ViveInteraction : MonoBehaviour
    {
        private FixedJoint _AttachJoint = null;

        private Rigidbody _CurrentRigidBody = null;
        private List<Rigidbody> _ContactRigidBodies = new List<Rigidbody>();

        void Awake()
        {
            _AttachJoint = GetComponent<FixedJoint>();
        }

        void OnTriggerEnter(Collider collider)
        {
            if (!collider.gameObject.CompareTag("Interactable"))
            {
                return;
            }

            _ContactRigidBodies.Add(collider.gameObject.GetComponent<Rigidbody>());
        }

        void OnTriggerExit(Collider collider)
        {
            if (!collider.gameObject.CompareTag("Interactable"))
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

        public void PickUp()
        {
            _CurrentRigidBody = GetNearestRigidbody();

            if (!_CurrentRigidBody)
            {
                return;
            }

            _CurrentRigidBody.transform.position = transform.position;
            _AttachJoint.connectedBody = _CurrentRigidBody;
        }

        public void Drop(SteamVR_Controller.Device device)
        {
            if (!_CurrentRigidBody)
            {
                return;
            }

            _CurrentRigidBody.velocity = device.velocity;
            _CurrentRigidBody.angularVelocity = device.angularVelocity;

            LW_ObjectReturn objectReturn = _CurrentRigidBody.gameObject.GetComponent<LW_ObjectReturn>();
            objectReturn.SetLastInteraction(this);

            _AttachJoint.connectedBody = null;
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
    }
}

