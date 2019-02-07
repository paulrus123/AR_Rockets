using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSphereBehavior : MonoBehaviour
{
    public GardenHandler gardenHandler;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //transform.rotation = transform.parent.rotation;
        gardenHandler = GameObject.Find("House").GetComponent<GardenHandler>();

    }
}
