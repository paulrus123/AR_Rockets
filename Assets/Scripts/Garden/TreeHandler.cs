using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHandler : MonoBehaviour
{
    public GardenHandler gardenHandler;
    // Start is called before the first frame update
    void Start()
    {
        gardenHandler = GameObject.Find("House").GetComponent<GardenHandler>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            gardenHandler.GardenHit(50);
        }
    }
}
