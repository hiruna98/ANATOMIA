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

    
    private List<GameObject> allGameObjects = new List<GameObject>();

    private Camera cam;

    //to capture double tap
    private int tapCount = 0;

    private DataStore dataStore;


    void OnEnable()
    {

        Inital();
    }

    void Inital()
    {
        cam = GetComponent<Camera>();
        multiSelectStore = MultiSelectStore.Instance;
        allGameObjects.AddRange(GameObject.FindGameObjectsWithTag("Object"));
        materialController = MaterialController.Instance;
        dataStore = DataStore.Instance;
        viewController = ViewController.Instance;
        viewController.initializeRotation(root);
        Debug.Log(root.transform.rotation.eulerAngles);
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
                
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPos = touch.position;
                        timePressed = Time.time;
                        break;
                    case TouchPhase.Ended:
                        endPos = touch.position;
                        timeLastPress = Time.time;
                        if ((timeLastPress - timePressed) > timeDelayThreshold && touch.tapCount == 1)
                        {
                            Ray ray = cam.ScreenPointToRay(touch.position);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit)){
                                multisilectEnable = true;   //If loag press enable multiple selection option
                            }
                            //Select touched object
                            SelectObject(touch);
                        }else if((timeLastPress - timePressed) < timeDelayThreshold && touch.tapCount == 1){
                            SelectObject(touch);
                        }
                        if((timeLastPress - timePressed) < timeDelayThreshold && touch.tapCount == 2){
                            viewController.antRotation(root);
                        }
                            
                        break;
                }
                
                
            }
            if(Input.touchCount == 5)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);
                Touch touch2 = Input.GetTouch(2);
                Touch touch3 = Input.GetTouch(3);
                Touch touch4 = Input.GetTouch(4);

                if(touch0.phase == TouchPhase.Ended && touch1.phase == TouchPhase.Ended && touch2.phase == TouchPhase.Ended && touch3.phase == TouchPhase.Ended && touch4.phase == TouchPhase.Ended && touch0.tapCount == 1 && touch1.tapCount == 1 && touch2.tapCount == 1 && touch3.tapCount == 1 && touch4.tapCount == 1)
                {
                    GameObject slicedObjLeft = GameObject.Find("slicedParentLeft");
                    GameObject slicedObjRight = GameObject.Find("slicedParentRight");
                    Destroy(slicedObjLeft);
                    Destroy(slicedObjRight);
                    root.SetActive(true);
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
            if (multiSelectStore.findObject(hitObject))
            {
                //if touch already selected object un select it
                multiSelectStore.removeObject(hitObject);
                materialController.removeMaterial(hitObject);
                if (SelectionMode == SelMode.AndChildren)
                {
                    List<GameObject> childrenRenderers = new List<GameObject>();
                    foreach (Transform child in hit.transform){
                        childrenRenderers.Add(child.gameObject);
                    }
                    childrenRenderers.ForEach(r =>
                    {
                        multiSelectStore.removeObject(r);
                        materialController.removeMaterial(r);
                    });
                }
            }
            else
            {
                if (multisilectEnable)
                {
                    // if multi select enabled and hit an object -> add it and its children to the multi select store and add selection material
                    multiSelectStore.addObject(hitObject);
                    materialController.addMaterial(hitObject,selectionMat);
                    if (SelectionMode == SelMode.AndChildren)
                    {
                        List<GameObject> childrenRenderers = new List<GameObject>();
                        foreach (Transform child in hit.transform){
                            childrenRenderers.Add(child.gameObject);
                        }
                        childrenRenderers.ForEach(r =>
                        {
                            multiSelectStore.addObject(r);
                            materialController.addMaterial(r,selectionMat);
                        });
                    }
                }
                else
                {
                    if(dataStore.getCrossSectionSelection() == true){
                        hitObject.transform.parent.gameObject.SetActive(false);
                        dataStore.setCrossSectionSelection(false);
                    }else{
                        // if multiselect not enabled remove all selected objects from list and add newly touched object
                        multiSelectStore.removeAllObject();
                        materialController.removeMaterialOfAllObjects();
                        multiSelectStore.addObject(hitObject);
                        materialController.addMaterial(hitObject,selectionMat);
                        infoModel.SetActive(true);
                        //infoAnim.Play("IM_Open");
                        infoModel.transform.localScale = new Vector3(1,1,1);
                        if (SelectionMode == SelMode.AndChildren)
                        {
                            List<GameObject> childrenRenderers = new List<GameObject>();
                            foreach (Transform child in hit.transform){
                                childrenRenderers.Add(child.gameObject);
                            }
                            childrenRenderers.ForEach(r =>
                            {
                                multiSelectStore.addObject(r);
                                materialController.addMaterial(r,selectionMat);
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
            // else
            // {
            //     materialController.removeMaterialOfAllObjects();
            //     // infoModel.SetActive(false);
            //     multiSelectStore.removeAllObject();
            // }
        }
    }

}

