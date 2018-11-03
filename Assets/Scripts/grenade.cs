using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour {

    public float delay = 2f;
    float countdown;
    bool hasExploded = false;
    public GameObject explosionEffect;
    public float radius = 5f;
    public float force = 1000000000f;
    public Animator zombieAnim;

    // Use this for initialization
    void Start () {
        countdown = delay;
	}
	
	// Update is called once per frame
	void Update () {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
	}

    void Explode()
    {
        // Show effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // Get nearby objects, write them in an array of Collider objects named colliders
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        // for each object called nearbyObject in the array called colliders
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null && nearbyObject.name == "CartoonyBusinessZombie")
            {
                // Stop animation of zombie
                zombieAnim = nearbyObject.GetComponent<Animator>();
                zombieAnim.Play("New State");

                // Add explosion force to zombie
                CapsuleCollider coll = nearbyObject.GetComponent<CapsuleCollider>();

                rb.AddExplosionForce(force, transform.position, radius);
            }
            // if (it is a zombie)
            // { remove the zombie? }
        }
      
        // Remove grenade
        Destroy(gameObject);
    }
}
