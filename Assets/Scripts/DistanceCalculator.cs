using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    public GameObject target;
    public float distanceFromTarget = 0;

    // Update is called once per frame
    void Update()
    {
        distanceFromTarget = Vector3.Distance(target.transform.position, this.transform.position);
        GetComponent<TextMesh>().text = "Distance = " + distanceFromTarget;

    }
}
