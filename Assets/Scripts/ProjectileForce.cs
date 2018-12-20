using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileForce : MonoBehaviour
{
    int shootSpeed = 5;
    Rigidbody rigBody; 

    // Start is called before the first frame update
    void Start()
    {
        rigBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 5);
        rigBody.velocity = transform.forward * shootSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = rigBody.velocity.normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
