using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Camera mainCamera;
    Camera uiCamera;

    void Start()
    {
        mainCamera = Camera.main;
        // uiCamera = GameObject.Find("UI Camera").GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 10f;
        this.transform.position = mainCamera.ScreenToWorldPoint(temp);
    }
    
}
