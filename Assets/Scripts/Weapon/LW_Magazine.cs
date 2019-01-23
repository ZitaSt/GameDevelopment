using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Properties;

namespace LittleWarrior.Weapon
{
    public class LW_Magazine : LW_ViveInteraction
    {

        public GameObject otherMagazine;

        private LW_ViveInteraction _ThisInteraction = null;
        private MeshRenderer _OtherMagazineRenderer = null;

        private Coroutine _MeasureCheck = null;
        private bool _IsLoader = false;

        private void Awake()
        {
            _OtherMagazineRenderer = otherMagazine.GetComponent<MeshRenderer>();
        }

        public override void OnPickUp(LW_ViveInteraction ni)
        {
            _OtherMagazineRenderer.enabled = true;
            _ThisInteraction = ni;
        }

        public override void OnDrop(LW_ViveInteraction ni)
        {
            _OtherMagazineRenderer.enabled = false;
            _ThisInteraction = null;

            if (_MeasureCheck != null)
            {
                StopCoroutine(_MeasureCheck);

                transform.position = otherMagazine.transform.position;
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if(col.gameObject.GetInstanceID() != otherMagazine.GetInstanceID())
            {
                return;
            }

            RemoveFromController();
        }

        private void RemoveFromController()
        {
            _OtherMagazineRenderer.enabled = false;

            //_ThisInteraction.Detach();

            GetComponent<Collider>().enabled = false;

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;

            transform.parent = otherMagazine.transform;
            transform.localRotation = Quaternion.identity;

            float id = (_ThisInteraction.transform.position -
                        otherMagazine.transform.position).sqrMagnitude;
            _MeasureCheck = StartCoroutine(MeasureInteract(id));

        }

        private IEnumerator MeasureInteract(float id)
        {
            while(id * 4 > (_ThisInteraction.gameObject.transform.position -
                            otherMagazine.transform.position).sqrMagnitude)
            {
                float step = 0.2f * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position,
                                                         otherMagazine.transform.position,
                                                         step);
                yield return null;
            }
            ReturnToController();
        }

        private void ReturnToController()
        {
            _OtherMagazineRenderer.enabled = true;

            transform.parent = null;
            transform.position = _ThisInteraction.gameObject.transform.position;

            GetComponent<Collider>().enabled = true;

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;

            //_ThisInteraction.Attach();
        }

    }
}