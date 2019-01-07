using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Properties;

namespace LittleWarrior.Slaves
{
    public class LW_ObjectReturn : MonoBehaviour
    {
        private float _WaitTime = 3.0f;

        private LW_ViveInteraction _LastInteraction = null;
        private Coroutine _CountDown = null;

        private Vector3 _OriginalPosition = Vector3.zero;
        private Quaternion _OriginalRotation = Quaternion.identity;

        void Awake()
        {
            _OriginalPosition = transform.position;
            _OriginalRotation = transform.rotation;
        }

        public void SetLastInteraction(LW_ViveInteraction newInteraction)
        {
            _LastInteraction = newInteraction;
        }

        void OnCollisionEnter(Collision other)
        {
            if(!_LastInteraction)
            {
                return;
            }

            if(_CountDown != null)
            {
                StopCoroutine(_CountDown);
            }

            _CountDown = StartCoroutine(CountDown());
        }

        private IEnumerator CountDown()
        {
            yield return new WaitForSeconds(_WaitTime);

            GameObject currentObject = _LastInteraction.GetCurrentObject();

            if(currentObject)
            {
                if (gameObject.GetInstanceID() == currentObject.GetInstanceID())
                    yield break;
            }

            transform.position = _OriginalPosition;
            transform.rotation = _OriginalRotation;
        }
    }
}

