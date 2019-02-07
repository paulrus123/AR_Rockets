using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileForce : MonoBehaviour
{
    float shootSpeed = 5;
    Rigidbody rigBody;
    public GameObject Smoke;
    public GameObject Fire;

    public float radius = 0.01F;
    public float power = 1.0F;

    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, 5);
        rigBody = GetComponent<Rigidbody>();
        rigBody.velocity = transform.forward * shootSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = rigBody.velocity.normalized;
        transform.Rotate(90, 0, 0);
    }

    public void SetShootSpeed(float speed)
    {
        shootSpeed = speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Target")
        {
            Debug.Log("Target hit!");
        }
        else
        {
            Debug.Log("Something else hit");
            Instantiate(Fire, transform.position, collision.gameObject.transform.rotation);
            Instantiate(Smoke, transform.position, collision.gameObject.transform.rotation);

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                RobotBehavior behavior = hit.GetComponent<RobotBehavior>();

                if (rb != null)
                {
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                    if (behavior != null)
                    {
                        behavior.decreaseHealth(50);
                    }
                }
            }
        }
        Destroy(gameObject);
    }
}
