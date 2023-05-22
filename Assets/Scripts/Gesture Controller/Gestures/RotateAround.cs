using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    private Camera cam;

    private bool rotating = false;

    public GameObject rotatePoint;

    private DataStore dataStore;

    public bool multiUserEnable;

    private int touchesTopCount = 0;
    private int touchesBottomCount = 0;

    // private bool topScaling = false;
    // private bool bottomScaling = false;


    private List<Touch> touchesTop = new List<Touch>();
    private List<Touch> touchesBottom = new List<Touch>();

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        dataStore = DataStore.Instance;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            if (this.multiUserEnable){
                getTouches();
                if(touchesBottomCount == 1){
                    Touch touch = touchesBottom[0];
                    Ray ray = cam.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    if (!dataStore.getIsDrawing())
                    {
                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
                                if (Physics.Raycast(ray, out hit))
                                {
                                    GameObject hitObject = hit.transform.gameObject;
                                    if (hitObject.tag == "Object" || hitObject.tag == "clipping object")
                                    {
                                        rotating = true;
                                        if(!dataStore.getIsRadialTop()){
                                            dataStore.setRotateByBottom(true);
                                            dataStore.setRotateByTop(false);
                                        }else if(!dataStore.getRotateByTop() && !dataStore.getScaleByTop()){
                                            dataStore.setRotateByBottom(true);
                                        }
                                    }
                                }
                                break;
                            case TouchPhase.Moved:
                                if(dataStore.getRotateByTop() || dataStore.getScaleByTop()){
                                    dataStore.setRotateByBottom(false);
                                }
                                if (rotating && dataStore.getRotateByBottom())
                                {
                                    // transform.Rotate(-touch.deltaPosition.y,-touch.deltaPosition.x,0f, Space.Self);
                                    transform.RotateAround(
                                        rotatePoint.transform.position,
                                        Vector3.down,
                                        touch.deltaPosition.x
                                    );
                                    transform.RotateAround(
                                        rotatePoint.transform.position,
                                        Vector3.right,
                                        touch.deltaPosition.y
                                    );
                                }
                                break;
                            case TouchPhase.Ended:
                                rotating = false;
                                //dataStore.setRotateByBottom(false);
                                break;
                        }
                    }
                }else{
                    dataStore.setRotateByBottom(false);
                }
                if(touchesTopCount == 1){
                    Touch touch = touchesTop[0];
                    Ray ray = cam.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    if (!dataStore.getIsDrawing())
                    {
                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
                                if (Physics.Raycast(ray, out hit))
                                {
                                    GameObject hitObject = hit.transform.gameObject;
                                    if (hitObject.tag == "Object" || hitObject.tag == "clipping object")
                                    {
                                        rotating = true;
                                        if(dataStore.getIsRadialTop()){
                                            dataStore.setRotateByBottom(false);
                                            dataStore.setRotateByTop(true);
                                        }else if(!dataStore.getRotateByBottom() && !dataStore.getScaleByBottom()){
                                            dataStore.setRotateByTop(true);
                                        }
                                    }
                                }
                                break;
                            case TouchPhase.Moved:
                                if(dataStore.getRotateByBottom() || dataStore.getScaleByBottom()){
                                    dataStore.setRotateByTop(false);
                                }
                                if (rotating && dataStore.getRotateByTop())
                                {
                                    // transform.Rotate(-touch.deltaPosition.y,-touch.deltaPosition.x,0f, Space.Self);
                                    transform.RotateAround(
                                        rotatePoint.transform.position,
                                        Vector3.down,
                                        touch.deltaPosition.x
                                    );
                                    transform.RotateAround(
                                        rotatePoint.transform.position,
                                        Vector3.right,
                                        touch.deltaPosition.y
                                    );
                                }
                                break;
                            case TouchPhase.Ended:
                                rotating = false;
                                // dataStore.setRotateByTop(false);
                                break;
                        }
                    }
                }else{
                    dataStore.setRotateByTop(false);
                }
            }else{
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);
                    Ray ray = cam.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    if (!dataStore.getIsDrawing())
                    {
                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
                                if (Physics.Raycast(ray, out hit))
                                {
                                    GameObject hitObject = hit.transform.gameObject;
                                    if (hitObject.tag == "Object" || hitObject.tag == "clipping object")
                                    {
                                        rotating = true;
                                    }
                                }
                                break;
                            case TouchPhase.Moved:
                                if (rotating)
                                {
                                    // transform.Rotate(-touch.deltaPosition.y,-touch.deltaPosition.x,0f, Space.Self);
                                    transform.RotateAround(
                                        rotatePoint.transform.position,
                                        Vector3.down,
                                        touch.deltaPosition.x
                                    );
                                    transform.RotateAround(
                                        rotatePoint.transform.position,
                                        Vector3.right,
                                        touch.deltaPosition.y
                                    );
                                }
                                break;
                            case TouchPhase.Ended:
                                rotating = false;
                                break;
                        }
                    }
                }
            }
        }
    }

    void getTouches(){
        touchesTopCount = 0;
        touchesBottomCount = 0;
        touchesTop.Clear();
        touchesBottom.Clear();
        // Loop through all touch inputs
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // Check if touch is within top or bottom half of screen
            if (touch.position.y > Screen.height * dataStore.getDividingLineY())
            {
                touchesTopCount++;
                touchesTop.Add(touch);
            }
            else
            {
                touchesBottomCount++;
                touchesBottom.Add(touch);
            }
        }
    }

}
