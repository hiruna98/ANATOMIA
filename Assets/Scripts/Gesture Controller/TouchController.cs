using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Camera))]
public class TouchController : MonoBehaviour
{
    public enum SelMode : int
    {
        OnlyParent = 0,
        AndChildren = 1
    }

    public SelMode SelectionMode = SelMode.AndChildren;

    public Material selectionMat;

    private Vector2 startPos;
    private Vector2 endPos;

    //to identify long press
    private float timePressed = 0.0f;
    private float timeLastPress = 0.0f;
    public float timeDelayThreshold = 1.0f;

    private bool multisilectEnable = false;

    //maximum difference bitween startpos and endpos of single tap
    public float tapThreshold = 2.0f;

    //root of the object hierarchy
    public GameObject root;

    //public GameObject rotationPoint;

    public GameObject infoModel;

    private Animation infoAnim;

    private MultiSelectStore multiSelectStore;

    private MaterialController materialController;

    private ViewController viewController;

    // private List<GameObject> allGameObjects = new List<GameObject>();

    private Camera cam;

    //to capture double tap
    private int tapCount = 0;

    private DataStore dataStore;

    public float doubleTapDelayThershold = 0.5f;
    private float firstTapTime = 0.0f;
    private float secondTimeTap = 0.0f;

    public float singleTapMoveThreshold = 10f;

    private Plane plane;
    private GameObject cutPlane;
    private GameObject clippingObject;
    private GameObject originalObject;

    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 pointC;
    private LineRenderer cutRender;

    Vector3 touch0StartPos;
    Vector3 touch1StartPos;
    Vector3 touch2StartPos;

    Vector3 touch0EndPos;
    Vector3 touch1EndPos;
    Vector3 touch2EndPos;

    void OnEnable()
    {
        Inital();
    }

    void Inital()
    {
        cam = GetComponent<Camera>();
        multiSelectStore = MultiSelectStore.Instance;
        // allGameObjects.AddRange(GameObject.FindGameObjectsWithTag("object"));
        materialController = MaterialController.Instance;
        dataStore = DataStore.Instance;
        viewController = ViewController.Instance;
        cutPlane = GameObject.Find("Clipping Plane");
        viewController.initializeRotation(root);
        cutRender = GetComponent<LineRenderer>();
        clippingObject = root.transform.Find("Clipping Object").gameObject;
        originalObject = root.transform.Find("Original Object").gameObject;
    }

    private void Awake()
    {
        Inital();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (!dataStore.getIsDrawing())
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            startPos = touch.position;
                            timePressed = Time.time;
                            break;
                        case TouchPhase.Ended:
                            endPos = touch.position;
                            timeLastPress = Time.time;

                            if (isBelowSingleTapMoveThreshold(startPos, endPos))
                            {
                                tapCount++;
                                if (tapCount == 1)
                                {
                                    firstTapTime = Time.time;
                                }
                                else if (tapCount >= 2)
                                {
                                    secondTimeTap = Time.time;
                                    if ((secondTimeTap - firstTapTime) <= doubleTapDelayThershold)
                                    {
                                        tapCount = 0;
                                        viewController.antRotation(root);
                                    }
                                    else
                                    {
                                        tapCount = 1;
                                        firstTapTime = secondTimeTap;
                                    }
                                }
                                if ((timeLastPress - timePressed) > timeDelayThreshold)
                                {
                                    multisilectEnable = true; //If loag press enable multiple selection option
                                }
                                //Select touched object
                                SelectObject(touch);
                            }
                            break;
                    }
                }
            }
            if (Input.touchCount == 3)
            {
                if (!dataStore.getIsDrawing())
                {
                    Touch touch0 = Input.GetTouch(0);
                    Touch touch1 = Input.GetTouch(1);
                    Touch touch2 = Input.GetTouch(2);
                    // Touch touch3 = Input.GetTouch(3);
                    // Touch touch4 = Input.GetTouch(4);



                    if (
                        touch0.phase == TouchPhase.Began
                        && touch1.phase == TouchPhase.Began
                        && touch2.phase == TouchPhase.Began
                    )
                    {
                        touch0StartPos = touch0.position;
                        touch1StartPos = touch1.position;
                        touch2StartPos = touch2.position;
                    }
                    if (
                        touch0.phase == TouchPhase.Ended
                        && touch1.phase == TouchPhase.Ended
                        && touch2.phase == TouchPhase.Ended
                    )
                    {
                        touch0EndPos = touch0.position;
                        touch1EndPos = touch1.position;
                        touch2EndPos = touch2.position;

                        if (
                            isBelowSingleTapMoveThreshold(touch0StartPos, touch0EndPos)
                            && isBelowSingleTapMoveThreshold(touch1StartPos, touch1EndPos)
                            && isBelowSingleTapMoveThreshold(touch2StartPos, touch2EndPos)
                        )
                        {
                            if (dataStore.getIsObjectCut())
                            {
                                cutPlane.transform.rotation = Quaternion.Euler(0, 0, 0);
                                cutPlane.transform.position = new Vector3(1, 1, 0);
                                originalObject.SetActive(true);
                                clippingObject.SetActive(false);
                                dataStore.setIsObjectCut(false);
                            }
                        }
                    }
                }
            }
        }
    }

    public void SelectObject(Touch touch)
    {
        Ray ray = cam.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            infoModel.SetActive(false);
            if (dataStore.getCrossSectionSelection() == true)
            {
                originalObject.SetActive(false);
                clippingObject.SetActive(true);
                pointC = cam.ScreenToWorldPoint(touch.position);
                pointB = dataStore.getCutPointB();
                pointA = dataStore.getCutPointA();
                cutPlane.transform.position = new Vector3(0, (pointA.y + pointB.y) / 2, 0);
                cutPlane.transform.rotation = Quaternion.Euler(0, 0, 0);
                cutPlane.transform.Rotate(0, 0, calculateAngle(), Space.Self);
                cutRender.SetPosition(0, Vector3.Lerp(pointA, pointB, 1f));
                if (isRightSide(pointC))
                {
                    cutPlane.transform.Rotate(0, 0, 180, Space.Self);
                }
                dataStore.setCrossSectionSelection(false);
                dataStore.setIsObjectCut(true);
            }
            else if (multiSelectStore.findObject(hitObject))
            {
                if (!dataStore.getIsObjectCut())
                {
                    //if touch already selected object un select it
                    multiSelectStore.removeObject(hitObject);
                    materialController.removeMaterial(hitObject);
                    if (SelectionMode == SelMode.AndChildren)
                    {
                        List<GameObject> childrenRenderers = new List<GameObject>();
                        foreach (Transform child in hit.transform)
                        {
                            childrenRenderers.Add(child.gameObject);
                        }
                        childrenRenderers.ForEach(r =>
                        {
                            multiSelectStore.removeObject(r);
                            materialController.removeMaterial(r);
                        });
                    }
                }
            }
            else
            {
                if (!dataStore.getIsObjectCut())
                {
                    if (multisilectEnable)
                    {
                        // if multi select enabled and hit an object -> add it and its children to the multi select store and add selection material
                        multiSelectStore.addObject(hitObject);
                        materialController.addMaterial(hitObject, selectionMat);
                        if (SelectionMode == SelMode.AndChildren)
                        {
                            List<GameObject> childrenRenderers = new List<GameObject>();
                            foreach (Transform child in hit.transform)
                            {
                                childrenRenderers.Add(child.gameObject);
                            }
                            childrenRenderers.ForEach(r =>
                            {
                                multiSelectStore.addObject(r);
                                materialController.addMaterial(r, selectionMat);
                            });
                        }
                    }
                    else
                    {
                        // if multiselect not enabled remove all selected objects from list and add newly touched object
                        multiSelectStore.removeAllObject();
                        materialController.removeMaterialOfAllObjects();
                        multiSelectStore.addObject(hitObject);
                        materialController.addMaterial(hitObject, selectionMat);
                        infoModel.SetActive(true);
                        //infoAnim.Play("IM_Open");
                        infoModel.transform.localScale = new Vector3(1, 1, 1);
                        if (SelectionMode == SelMode.AndChildren)
                        {
                            List<GameObject> childrenRenderers = new List<GameObject>();
                            foreach (Transform child in hit.transform)
                            {
                                childrenRenderers.Add(child.gameObject);
                            }
                            childrenRenderers.ForEach(r =>
                            {
                                multiSelectStore.addObject(r);
                                materialController.addMaterial(r, selectionMat);
                            });
                        }
                    }
                }
            }
        }
        else
        {
            // if hit outside un select all
            if (multisilectEnable)
            {
                materialController.removeMaterialOfAllObjects();
                multiSelectStore.removeAllObject();
                infoModel.SetActive(false);
                multisilectEnable = false;
            }
            else
            {
                materialController.removeMaterialOfAllObjects();
                // infoModel.SetActive(false);
                multiSelectStore.removeAllObject();
            }
        }
    }

    public float calculateAngle()
    {
        float angle = Mathf.Atan2(pointA.y - pointB.y, pointA.x - pointB.x) * 180 / Mathf.PI;
        return angle;
    }

    public bool isRightSide(Vector3 position)
    {
        Vector3 pointInPlane = (pointA + pointB) / 2;

        Vector3 cutPlaneNormal = Vector3
            .Cross((pointA - pointB), (pointA - cam.transform.position))
            .normalized;
        Quaternion orientation = Quaternion.FromToRotation(Vector3.up, cutPlaneNormal);

        Plane plane = new Plane(transform.up, cutPlane.transform.position);

        bool pointRighttSide = plane.GetSide(position);

        return pointRighttSide;
    }

    public bool isBelowSingleTapMoveThreshold(Vector3 startPosition, Vector3 endPosition)
    {
        float difX = Math.Abs(endPos.x - startPos.x);
        float difY = Math.Abs(endPos.y - startPos.y);
        if (difX <= singleTapMoveThreshold && difY <= singleTapMoveThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
