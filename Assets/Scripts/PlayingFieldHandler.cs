using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleARCore;
using GoogleARCore.Examples.Common;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class PlayingFieldHandler : MonoBehaviour
{
    /// <summary>
    /// The first-person camera being used to render the passthrough camera image (i.e. AR background).
    /// </summary>
    public Camera FirstPersonCamera;

    public GameObject BeaconPrefab;

    private int numBeacons = 0; //number of beacons that have been placed

    public Vector3[] PositionArray;

    public delegate void PositionsDetermined(Vector3[] positions);
    public static event PositionsDetermined OnPositionsDetermined;

    GameStateHandler gameStateHandler;

    void Start()
    {
        FirstPersonCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();
        TouchHandler.OnClicked += screenTouched;
        PositionArray = new Vector3[4];
        gameStateHandler = GameObject.Find("GameStateManager").GetComponent<GameStateHandler>();

    }

    void screenTouched()
    {
        if(GameStateHandler.gamestate != GameStateHandler.GameState.PLACINGMARKERS)
        {
            //do not need more than 4 beacons. 
            return;
        }
        Touch touch = Input.GetTouch(0);

        // Raycast against the location the player touched to search for planes.
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                if (hit.Trackable is DetectedPlane)
                {
                    DetectedPlane plane = (DetectedPlane)hit.Trackable;
                    if (plane.PlaneType == DetectedPlaneType.HorizontalUpwardFacing)
                    {
                        //Set target to point
                        Quaternion targetQuaternion = Quaternion.Euler(90,
                            hit.Pose.rotation.eulerAngles.y,
                            hit.Pose.rotation.eulerAngles.z
                        );
                            
                        GameObject beacon = Instantiate(BeaconPrefab, hit.Pose.position, targetQuaternion);

                        // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
                        // world evolves.
                        var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                        // Make target model a child of the anchor.
                        beacon.transform.parent = anchor.transform;
                        beacon.transform.Rotate(90, 180, 0);

                        PositionArray[numBeacons] = hit.Pose.position;
                        numBeacons++;
                        if(numBeacons == 4)
                        {
                            if (OnPositionsDetermined != null)
                                OnPositionsDetermined(PositionArray);
                            gameStateHandler.SetGameState(GameStateHandler.GameState.READYTOPLAY);
                        }
                    }
                    else
                    {
                        //Cannot place beacon on vertical surface
                    }
                }
            }
        }
    }
}
