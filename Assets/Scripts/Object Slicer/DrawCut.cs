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

    private LineRenderer cutRender;
    private bool animateCut;

    private GameObject rootObject;

    private GameObject rotationPoint;

    Camera cam;

    void Start() {
        cam = FindObjectOfType<Camera>();
        cutRender = GetComponent<LineRenderer>();
        cutRender.startWidth = .05f;
        cutRender.endWidth = .05f;
        rootObject = GameObject.Find("root_object");
        rotationPoint = GameObject.Find("Rotation Point");
    }

    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = -cam.transform.position.z;
/*
        if (Input.GetMouseButtonDown(0))
        {
            pointA = cam.ScreenToWorldPoint(mouse);
            Debug.Log(pointA);
        }

        if (Input.GetMouseButton(0))
        {
            animateCut = false;
            cutRender.SetPosition(0,pointA);
            cutRender.SetPosition(1,cam.ScreenToWorldPoint(mouse));
            cutRender.startColor = Color.gray;
            cutRender.endColor = Color.gray;
        }

        if (Input.GetMouseButtonUp(0)) {
            pointB = cam.ScreenToWorldPoint(mouse);
            Debug.Log(pointB);
            CreateSlicePlane();
            cutRender.positionCount = 2;
            cutRender.SetPosition(0,pointA);
            cutRender.SetPosition(1,pointB);
            animateCut = true;
        }
*/
        // if (animateCut)
        // {
        //     cutRender.SetPosition(0,Vector3.Lerp(pointA,pointB,1f));
        // }

        //Touch
        if (Input.touchCount == 6 && Input.GetTouch(5).phase == TouchPhase.Began)
        {
            pointA = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(5).position.x,Input.GetTouch(5).position.y,(rootObject.transform.position.z-cam.transform.position.z)));
            // pointA = cam.ScreenToWorldPoint(mouse);
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
            CreateSlicePlane();
            cutRender.positionCount = 2;
            cutRender.SetPosition(0,pointA);
            cutRender.SetPosition(1,pointB);
            animateCut = true;
        }

        if (animateCut)
        {
            cutRender.SetPosition(0,Vector3.Lerp(pointA,pointB,1f));
        }
    }

    void CreateSlicePlane() 
    {
        Vector3 pointInPlane = (pointA + pointB) / 2;
        
        GameObject slicedParent1 = GameObject.Find("slicedParentLeft");
        GameObject slicedParent2 = GameObject.Find("slicedParentRight");

        Vector3 cutPlaneNormal = Vector3.Cross((pointA-pointB),(pointA-cam.transform.position)).normalized;
        Quaternion orientation = Quaternion.FromToRotation(Vector3.up, cutPlaneNormal);
        //boxVis.rotation = orientation;
       // boxVis.localScale = new Vector3(10, 0.25f, 10);
       // boxVis.position = pointInPlane;

        List<GameObject> allGameObjects = new List<GameObject>();
        foreach (Transform child in rootObject.transform)  
        {  
            if(child.childCount>0){
                foreach (Transform grandChild in child)   
                    allGameObjects.Add(grandChild.gameObject); 
            }else{
                allGameObjects.Add(child.gameObject); 
            }
        }

        var all = Physics.OverlapBox(pointInPlane, new Vector3(100, 0.01f, 100), orientation);
        if(all.Length > 0)
        {

            if(!slicedParent1)
            {
                slicedParent1 = new GameObject();
                slicedParent1.transform.position = rootObject.transform.position;
                slicedParent1.transform.rotation = rootObject.transform.rotation;
                slicedParent1.transform.localScale = rootObject.transform.localScale;
                slicedParent1.name = "slicedParentLeft";
                slicedParent1.AddComponent<LeanDragTranslate>().Use.RequiredFingerCount = 5;
                slicedParent1.AddComponent<PinchScale>().sensitivity = 0.05f;

                GameObject rotationPointLeft = new GameObject();
                rotationPointLeft.transform.position = rotationPoint.transform.position;
                rotationPointLeft.transform.rotation = rotationPoint.transform.rotation;
                rotationPointLeft.transform.localScale = rotationPoint.transform.localScale;
                rotationPointLeft.name = "Rotation Point Left";
                rotationPointLeft.transform.SetParent(slicedParent1.transform);
                slicedParent1.AddComponent<RotateAround>().rotatePoint = rotationPointLeft;

            }

            if(!slicedParent2)
            {
                slicedParent2 = new GameObject();
                slicedParent2.transform.position = rootObject.transform.position;
                slicedParent2.transform.rotation = rootObject.transform.rotation;
                slicedParent2.transform.localScale = rootObject.transform.localScale;
                slicedParent2.name = "slicedParentRight";
                slicedParent2.AddComponent<LeanDragTranslate>().Use.RequiredFingerCount = 5;
                slicedParent2.AddComponent<PinchScale>().sensitivity = 0.05f;

                GameObject rotationPointRight = new GameObject();
                rotationPointRight.transform.position = rotationPoint.transform.position;
                rotationPointRight.transform.rotation = rotationPoint.transform.rotation;
                rotationPointRight.transform.localScale = rotationPoint.transform.localScale;
                rotationPointRight.name = "Rotation Point Left";
                rotationPointRight.transform.SetParent(slicedParent1.transform);
                slicedParent2.AddComponent<RotateAround>().rotatePoint = rotationPointRight;
            }
        }
        
        //Ray ray = new Ray(pointA, (pointB - pointA).normalized);
        //var all = Physics.RaycastAll(ray);
        {
            foreach (var hit in all)
            {
                MeshFilter filter = hit.gameObject.GetComponentInChildren<MeshFilter>();
                allGameObjects.Remove(hit.gameObject);
                if(filter != null)
                    Cutter.Cut(hit.gameObject, pointInPlane, cutPlaneNormal, rootObject.transform.localScale);
            }
        }
        Plane cutPlane = new Plane(rootObject.transform.InverseTransformDirection(-cutPlaneNormal), rootObject.transform.InverseTransformPoint(pointInPlane));

        foreach(GameObject obj in allGameObjects){
            bool objRighttSide = cutPlane.GetSide(obj.transform.position);
            if(objRighttSide){
                obj.transform.SetParent(slicedParent2.transform);
            }else{
                obj.transform.SetParent(slicedParent1.transform);
            }
        }

        rootObject.SetActive(false);
        
    }
}
