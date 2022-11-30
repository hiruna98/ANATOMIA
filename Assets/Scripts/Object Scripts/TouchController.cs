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

    private int fourTapCount = 0;
    private Vector2 touch0_startpos;
    private Vector2 touch1_startpos;
    private Vector2 touch2_startpos;
    private Vector2 touch3_startpos;

    private Vector2 touch0_endpos;
    private Vector2 touch1_endpos;
    private Vector2 touch2_endpos;
    private Vector2 touch3_endpos;

    //to identify long press
    private float timePressed = 0.0f;
    private float timeLastPress = 0.0f;
    public float timeDelayThreshold = 1.0f;

    private bool multisilectEnable = false;

    //root of the object hierarchy
    public GameObject root;
    //public GameObject rotationPoint;

    private MultiSelectStore multiSelectStore;

    private MaterialController materialController;

    private ViewController viewController;

    
    private List<GameObject> allGameObjects = new List<GameObject>();

    private Camera cam;

    //to capture double tap
    private int tapCount = 0;
    public float doubleTapDelayThershold = 0.5f;

    public Vector2 forfingerTapAreaThreashold = new Vector2(1.0f,1.0f);
    private float firstTapTime = 0.0f;
    private float secondTimeTap = 0.0f;

    private GameObject viewPopup;


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
        viewController = ViewController.Instance;
        viewController.initializeRotation(root);
        viewPopup = GameObject.Find("View Popup");
        viewPopup.SetActive(false);
        PlayerPrefs.SetFloat("defaultScale",root.transform.localScale.x);
        PlayerPrefs.Save();
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

                        if (endPos == startPos)
                        {
                            Debug.Log("Tap");
                            tapCount++;
                            if(tapCount == 1){
                                firstTapTime = Time.time;
                            }else if(tapCount >=2){
                                secondTimeTap = Time.time;
                                if((secondTimeTap - firstTapTime)<= doubleTapDelayThershold){
                                    tapCount = 0;
                                    Debug.Log("Double Tap");
                                    viewController.antRotation(root);
                                }else{
                                    tapCount = 1;
                                    firstTapTime = secondTimeTap;
                                }
                            }
                            if ((timeLastPress - timePressed) > timeDelayThreshold)
                            {
                                multisilectEnable = true;   //If loag press enable multiple selection option
                            }
                            //Select touched object
                            SelectObject(touch);
                        }
                        break;
                }
                
            }
            if(Input.touchCount == 4)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);
                Touch touch2 = Input.GetTouch(2);
                Touch touch3 = Input.GetTouch(3);

                if(touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began || touch3.phase == TouchPhase.Began ){
                    touch0_startpos = touch0.position;
                    touch1_startpos = touch1.position;
                    touch2_startpos = touch2.position;
                    touch3_startpos = touch3.position;
                }
                if(touch0.phase == TouchPhase.Ended && touch1.phase == TouchPhase.Ended && touch2.phase == TouchPhase.Ended || touch3.phase == TouchPhase.Ended){
                    touch0_endpos = touch0.position;
                    touch1_endpos = touch1.position;
                    touch2_endpos = touch2.position;
                    touch3_endpos = touch3.position;
                    Vector2 v0 = new Vector2(Math.Abs(touch0_endpos.x-touch0_startpos.x),Math.Abs(touch0_endpos.y-touch0_startpos.y));
                    Vector2 v1 = new Vector2(Math.Abs(touch1_endpos.x-touch1_startpos.x),Math.Abs(touch1_endpos.y-touch1_startpos.y));
                    Vector2 v2 = new Vector2(Math.Abs(touch2_endpos.x-touch2_startpos.x),Math.Abs(touch2_endpos.y-touch2_startpos.y));
                    Vector2 v3 = new Vector2(Math.Abs(touch3_endpos.x-touch3_startpos.x),Math.Abs(touch3_endpos.y-touch3_startpos.y));
                    //Vector2 v4 = new Vector2(Math.Abs(touch4_endpos.x-touch4_startpos.x),Math.Abs(touch4_endpos.y-touch4_startpos.y));
                    

                    if (v0.x < forfingerTapAreaThreashold.x && v0.y < forfingerTapAreaThreashold.y && v1.x < forfingerTapAreaThreashold.x && v1.y < forfingerTapAreaThreashold.y && v2.x < forfingerTapAreaThreashold.x && v2.y < forfingerTapAreaThreashold.y && v3.x < forfingerTapAreaThreashold.x && v3.y < forfingerTapAreaThreashold.y)
                    {
                        fourTapCount++;
                        if(fourTapCount == 1){
                            firstTapTime = Time.time;
                        }else if(fourTapCount >=2){
                            secondTimeTap = Time.time;
                            int crossSectionEnable = PlayerPrefs.GetInt("crossSectionEnable");
                            if((secondTimeTap - firstTapTime)<= doubleTapDelayThershold && crossSectionEnable == 1){
                                fourTapCount = 0;
                                Debug.Log("Four finget double tap");
                                GameObject slicedObj = GameObject.Find("slicedObjects");
                                Destroy(slicedObj);
                                root.SetActive(true);
                                PlayerPrefs.SetInt("crossSectionEnable",0);
                                PlayerPrefs.Save();
                            }else{
                                fourTapCount = 1;
                                firstTapTime = secondTimeTap;
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
                    int crossSectionEnable = PlayerPrefs.GetInt("crossSectionEnable");
                    int crossSectionSelection = PlayerPrefs.GetInt("crossSectionSelection");
                    Debug.Log(crossSectionEnable);
                    if(crossSectionEnable == 1 && crossSectionSelection == 1){
                        hitObject.SetActive(false);
                        PlayerPrefs.SetInt("crossSectionSelection",0);
                        PlayerPrefs.Save();
                    }else{
                        // if multiselect not enabled remove all selected objects from list and add newly touched object
                        multiSelectStore.removeAllObject();
                        materialController.removeMaterialOfAllObjects();
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
                multisilectEnable = false;
            }
            else
            {
                materialController.removeMaterialOfAllObjects();
                multiSelectStore.removeAllObject();
            }
        }
    }

}

