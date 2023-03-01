using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{

    public GameObject drawPrefab;
    GameObject theTrail;
    Plane planeObj;
    Vector3 startPos;

    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        planeObj = new Plane(mainCamera.transform.forward * -1, this.transform.position);
    }

    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            theTrail = (GameObject)Instantiate(drawPrefab, this.transform.position, Quaternion.identity);

            Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            float dist;
            if(planeObj.Raycast(mouseRay, out dist))
            {
                startPos = mouseRay.GetPoint(dist);
            }
        }
        else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
        {
            Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            float dist;
            if(planeObj.Raycast(mouseRay, out dist))
            {
                theTrail.transform.position = mouseRay.GetPoint(dist);
                // Set the Z position of the trail to 0
                // Vector3 trailPos = mouseRay.GetPoint(dist);
                // trailPos.z = 0.0f;
                // theTrail.transform.position = trailPos;
            }
        }
    }
}
