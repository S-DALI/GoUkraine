using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe_Controller : MonoBehaviour
{
    public static bool Tap, SwipeLeft, SwipeRight, SwipeUp, SwipeDown;
    private bool isDragging = false;
    private Vector2 startTouch, SwipeDelta;

    void Update()
    {
        Tap = SwipeDown = SwipeUp = SwipeLeft = SwipeRight = false;
        if(Input.GetMouseButtonDown(0))
        {
            Tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
        }else if(Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }

        if(Input.touches.Length>0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Tap = true;
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }

        SwipeDelta = Vector2.zero;
        if(isDragging) 
        {
            if (Input.touches.Length < 0)
                SwipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                SwipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        if (SwipeDelta.magnitude>100)
        {
            float x = SwipeDelta.x;
            float y = SwipeDelta.y;
            if(Mathf.Abs(x)>Mathf.Abs(y))
            {
                if (x < 0)
                {
                    SwipeLeft = true;
                }
                else if (x > 0)
                {
                    SwipeRight = true;
                }
            }
            else
            {
                if (y < 0)
                    SwipeDown = true;
                else if(y>0)
                {
                    SwipeUp = true;
                }
                    
            }
            //Debug.Log("SwipeLeft " + SwipeLeft + " SwipeRight " + SwipeRight + " SwipeUp " + SwipeUp);
            Reset();
        }
    }

    private void Reset()
    {
        startTouch = SwipeDelta = Vector2.zero;
        isDragging = false;
    }
}
