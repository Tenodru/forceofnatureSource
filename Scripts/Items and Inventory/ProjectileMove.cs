using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [Header("Projectile Attributes")]
    [Tooltip("This projectile's default speed.")]
    [SerializeField] float defaultSpeed;
    [Tooltip("This projectile's default damage.")]
    [SerializeField] int defaultDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (defaultSpeed == 0)
        {
            Debug.Log("Projectile has no speed.");
        }
        else
        {
            transform.position += transform.forward * (defaultSpeed * Time.deltaTime);
        }

        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Health>().TakeDamage(defaultDamage);
            Debug.Log("Damaged " + other.gameObject.name);

            Destroy(gameObject);
        }
        else if (other.tag == "Obstacle")
        {
            Debug.Log("Hit obstacle.");

            Destroy(gameObject);
        }
    }
}
