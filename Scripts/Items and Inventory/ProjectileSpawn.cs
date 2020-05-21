using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileSpawn : MonoBehaviour
{
    [Header("Projectile Attributes")]
    [SerializeField] GameObject firePoint;
    [SerializeField] List<GameObject> projectiles = new List<GameObject>();

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnProjectile()
    {
        if (firePoint == null)
        {
            Debug.Log("Projectiles haven't been referenced!");
            return;
        }
    }
}
