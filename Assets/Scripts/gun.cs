using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) //left mouse key
        {
            Shoot();
        }
	}

    void Shoot()
    {
        RaycastHit hit; //what we hit with the ray
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //hit.transform is the object you shooted on
            Debug.Log(hit.transform.name);

            enemy zombie = hit.transform.GetComponent<enemy>();

            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }
    }
}
