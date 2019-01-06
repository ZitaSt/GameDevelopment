using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleWarrior.VFX
{
    public class LW_VFX_FlashDamage : MonoBehaviour
    {
        //TODO (skn): Find a way to generalize the process of detecting the meshes
        private SkinnedMeshRenderer _Mesh = null;

        private void Awake()
        {
            _Mesh = this.GetComponentInChildren<SkinnedMeshRenderer>();
        }

        public void Flash(int bp)
        {
            if(this.gameObject.active)
            {
                StartCoroutine(FlashDamage(bp));
            }
        }

        private IEnumerator FlashDamage(int bp)
        {
            Material tMat = null;
            switch(bp)
            {
                case 1:
                    {
                        tMat = _Mesh.materials[0];
                    }
                    break;
                case 2:
                    {
                        tMat = _Mesh.materials[1];
                    }
                    break;
                default:
                    break;
            }
            if(tMat == null)
            {
                yield return new WaitForSeconds(0.0f);
            }
            Color bColor = tMat.color;
            tMat.color = Color.red;

            yield return new WaitForSeconds(0.25f);

            tMat.color = bColor;
        }
    }
}

