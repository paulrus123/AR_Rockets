using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using static GoogleARCore.InstantPreviewInput;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class TouchHandler : MonoBehaviour
{
    public Vector2 startTouchPos;

    public delegate void ClickAction();
    public static event ClickAction OnClicked;
    public delegate void SwipeUpAction(float distance);
    public static event SwipeUpAction OnSwipedUp;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPos = touch.position;


                    break;
                case TouchPhase.Ended:
                    if ((touch.position.y - startTouchPos.y) > 1.5)
                    {
                        if (OnSwipedUp != null)
                            OnSwipedUp(touch.position.y - startTouchPos.y);
                    }
                    else
                    {
                        if (OnClicked != null)
                            OnClicked();
                    }
                    break;
            }
        }

    }
}
