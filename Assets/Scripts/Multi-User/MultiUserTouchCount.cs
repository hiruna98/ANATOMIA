using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiUserTouchCount : MonoBehaviour
{
    public float dividingLineY = 0;
    public int touchesTop = 0;
    public int touchesBottom = 0;

    void Update()
    {
        // Counters for touches on top/bottom halves of screen

        touchesTop = 0;
        touchesBottom = 0;
        // Loop through all touch inputs
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // Check if touch is within top or bottom half of screen
            if (touch.position.y < Screen.height * dividingLineY)
            {
                touchesBottom++;
            }
            else
            {
                touchesTop++;
            }
        }

        // Output touch counts for each half of the screen
        Debug.Log("Touches on bottom half: " + touchesBottom);
        Debug.Log("Touches on top half: " + touchesTop);
    }
}
