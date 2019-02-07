using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroy : MonoBehaviour
{
    public float delay = 0; //number of seconds to wait before destroying

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }
}
