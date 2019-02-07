using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.Common;

public class TurnOffPlaneGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayingFieldHandler.OnPositionsDetermined += turnOffPlane;
    }

    void turnOffPlane(Vector3[] points)
    {
        DetectedPlaneGenerator plane = GetComponent<DetectedPlaneGenerator>();
        plane.enabled = false;
    }
}
