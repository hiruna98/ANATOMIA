using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchPoint {
    public int touchId;
    public GameObject circle;

    public touchPoint(int newTouchId, GameObject newCircle){
        touchId = newTouchId;
        circle = newCircle;
    }	
}