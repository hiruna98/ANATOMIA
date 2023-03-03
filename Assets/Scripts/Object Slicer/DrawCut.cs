using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Lean.Touch;
using System.Collections;
using System.Collections.Generic;


public class DrawCut : MonoBehaviour
{
   // public Transform boxVis;
    Vector3 pointA;
    Vector3 pointB;
    Vector3 pointC;

    private LineRenderer cutRender;
    private bool animateCut;

    private GameObject rootObject;
    private GameObject clippingObject;
    private GameObject originalObject;

    private Plane plane;

    private bool selectCut = false;

    private DataStore dataStore;

    Camera cam;

    void Start() {
        cam = FindObjectOfType<Camera>();
        cutRender = GetComponent<LineRenderer>();
        cutRender.startWidth = .05f;
        cutRender.endWidth = .05f;
        rootObject = GameObject.Find("root_object");
        clippingObject = rootObject.transform.Find("Clipping Object").gameObject;
        originalObject = rootObject.transform.Find("Original Object").gameObject;
        dataStore = DataStore.Instance;
    }

    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = -cam.transform.position.z;


        //Touch
        if (Input.touchCount == 6 && Input.GetTouch(5).phase == TouchPhase.Began)
        {
            pointA = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(5).position.x,Input.GetTouch(5).position.y,(rootObject.transform.position.z-cam.transform.position.z)));
            // pointA = cam.ScreenToWorldPoint(mouse);
            dataStore.setCutPointA(pointA);
        }

        if (Input.touchCount == 6)
        {
            animateCut = false;
            cutRender.SetPosition(0,pointA);
            cutRender.SetPosition(1,cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(5).position.x,Input.GetTouch(5).position.y,(rootObject.transform.position.z-cam.transform.position.z))));
            //cutRender.SetPosition(1,cam.ScreenToWorldPoint(mouse));
            cutRender.startColor = Color.gray;
            cutRender.endColor = Color.gray;
        }

        if (Input.touchCount == 6 && Input.GetTouch(5).phase == TouchPhase.Ended) {
            pointB = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(5).position.x,Input.GetTouch(5).position.y,(rootObject.transform.position.z-cam.transform.position.z)));
            // pointB = cam.ScreenToWorldPoint(mouse);
            dataStore.setCutPointB(pointB);
            //CreateSlicePlane();
            cutRender.positionCount = 2;
            cutRender.SetPosition(0,pointA);
            cutRender.SetPosition(1,pointB);
            animateCut = true;
            dataStore.setCrossSectionSelection(true);
        }

        // if (animateCut)
        // {
        //     cutRender.SetPosition(0,Vector3.Lerp(pointA,pointB,1f));
        // }
    }
    

}
