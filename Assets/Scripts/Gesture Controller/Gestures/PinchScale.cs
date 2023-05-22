using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchScale : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;

    public float sensitivity = 1.0f;
    private Camera _camera;

    private DataStore dataStore;

    public bool multiUserEnable = false;

    private int touchesTopCount = 0;
    private int touchesBottomCount = 0;


    private List<Touch> touchesTop = new List<Touch>();
    private List<Touch> touchesBottom = new List<Touch>();


    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        dataStore = DataStore.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.multiUserEnable){
            getTouches();
            if(touchesBottomCount == 2){
                Touch touch1 = touchesBottom[0];
                Touch touch2 = touchesBottom[1];
                if(!dataStore.getIsRadialTop()){
                    dataStore.setScaleByBottom(true);
                    dataStore.setScaleByTop(false);
                }else if(!dataStore.getScaleByTop() && !dataStore.getRotateByTop()){
                    dataStore.setScaleByBottom(true);
                }
                if(dataStore.getScaleByBottom()){
                    // scale(touch1,touch2);
                    if (!dataStore.getIsDrawing())
                    {
                        var touchZero = touch1;
                        var touchOne = touch2;

                        // if any one of touchzero or touchOne is cancelled or maybe ended then do nothing
                        if (
                            touchZero.phase == TouchPhase.Ended
                            || touchZero.phase == TouchPhase.Canceled
                            || touchOne.phase == TouchPhase.Ended
                            || touchOne.phase == TouchPhase.Canceled
                        )
                        {
                            return; // basically do nothing
                        }

                        if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                        {
                            initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                            initialScale = _camera.transform.localPosition;
                        }
                        else if (touchZero.phase == TouchPhase.Moved && touchOne.phase == TouchPhase.Moved) // if touch is moved
                        {
                            if(dataStore.getRotateByTop() || dataStore.getScaleByTop()){
                                dataStore.setScaleByBottom(false);
                            }
                            var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                            //if accidentally touched or pinch movement is very very small
                            if (Mathf.Approximately(initialDistance, 0))
                            {
                                return; // do nothing if it can be ignored where inital distance is very close to zero
                            }

                            var factor = currentDistance - initialDistance;

                            _camera.transform.localPosition = new Vector3(
                                _camera.transform.localPosition.x,
                                _camera.transform.localPosition.y,
                                _camera.transform.localPosition.z + factor * sensitivity
                            ); // scale multiplied by the factor we calculated

                            initialDistance = currentDistance;
                        }
                    }
                }
            }else{
                dataStore.setScaleByBottom(false);
            }
            if(touchesTopCount == 2){
                Touch touch1 = touchesTop[0];
                Touch touch2 = touchesTop[1];
                if(dataStore.getIsRadialTop()){
                    dataStore.setScaleByBottom(false);
                    dataStore.setScaleByTop(true);
                }else if(!dataStore.getScaleByBottom() && !dataStore.getRotateByBottom()){
                    dataStore.setScaleByTop(true);
                }
                if(dataStore.getScaleByTop()){
                    // scale(touch1,touch2);
                    if (!dataStore.getIsDrawing())
                    {
                        var touchZero = touch1;
                        var touchOne = touch2;

                        // if any one of touchzero or touchOne is cancelled or maybe ended then do nothing
                        if (
                            touchZero.phase == TouchPhase.Ended
                            || touchZero.phase == TouchPhase.Canceled
                            || touchOne.phase == TouchPhase.Ended
                            || touchOne.phase == TouchPhase.Canceled
                        )
                        {
                            return; // basically do nothing
                        }

                        if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                        {
                            initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                            initialScale = _camera.transform.localPosition;
                        }
                        else if (touchZero.phase == TouchPhase.Moved && touchOne.phase == TouchPhase.Moved) // if touch is moved
                        {
                            if(dataStore.getRotateByBottom() || dataStore.getScaleByBottom()){
                                dataStore.setScaleByTop(false);
                            }
                            var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                            //if accidentally touched or pinch movement is very very small
                            if (Mathf.Approximately(initialDistance, 0))
                            {
                                return; // do nothing if it can be ignored where inital distance is very close to zero
                            }

                            var factor = currentDistance - initialDistance;

                            _camera.transform.localPosition = new Vector3(
                                _camera.transform.localPosition.x,
                                _camera.transform.localPosition.y,
                                _camera.transform.localPosition.z + factor * sensitivity
                            ); // scale multiplied by the factor we calculated

                            initialDistance = currentDistance;
                        }
                    }
                }
            }else{
                dataStore.setScaleByTop(false);
            }
 
        }else{
            if (Input.touchCount == 2)
            {
                // scale(Input.GetTouch(0),Input.GetTouch(1));
                if (!dataStore.getIsDrawing())
                {
                    var touchZero = Input.GetTouch(0);
                    var touchOne = Input.GetTouch(1);

                    // if any one of touchzero or touchOne is cancelled or maybe ended then do nothing
                    if (
                        touchZero.phase == TouchPhase.Ended
                        || touchZero.phase == TouchPhase.Canceled
                        || touchOne.phase == TouchPhase.Ended
                        || touchOne.phase == TouchPhase.Canceled
                    )
                    {
                        return; // basically do nothing
                    }

                    if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                    {
                        initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                        initialScale = _camera.transform.localPosition;
                    }
                    else if (touchZero.phase == TouchPhase.Moved && touchOne.phase == TouchPhase.Moved) // if touch is moved
                    {
                        var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                        //if accidentally touched or pinch movement is very very small
                        if (Mathf.Approximately(initialDistance, 0))
                        {
                            return; // do nothing if it can be ignored where inital distance is very close to zero
                        }

                        var factor = currentDistance - initialDistance;

                        _camera.transform.localPosition = new Vector3(
                            _camera.transform.localPosition.x,
                            _camera.transform.localPosition.y,
                            _camera.transform.localPosition.z + factor * sensitivity
                        ); // scale multiplied by the factor we calculated

                        initialDistance = currentDistance;
                    }
                }
            }
        }
        
    }

    void scale(Touch touch0, Touch touch1){
        if (!dataStore.getIsDrawing())
        {
            var touchZero = touch0;
            var touchOne = touch1;

            // if any one of touchzero or touchOne is cancelled or maybe ended then do nothing
            if (
                touchZero.phase == TouchPhase.Ended
                || touchZero.phase == TouchPhase.Canceled
                || touchOne.phase == TouchPhase.Ended
                || touchOne.phase == TouchPhase.Canceled
            )
            {
                return; // basically do nothing
            }

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = _camera.transform.localPosition;
            }
            else if (touchZero.phase == TouchPhase.Moved && touchOne.phase == TouchPhase.Moved) // if touch is moved
            {
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                //if accidentally touched or pinch movement is very very small
                if (Mathf.Approximately(initialDistance, 0))
                {
                    return; // do nothing if it can be ignored where inital distance is very close to zero
                }

                var factor = currentDistance - initialDistance;

                _camera.transform.localPosition = new Vector3(
                    _camera.transform.localPosition.x,
                    _camera.transform.localPosition.y,
                    _camera.transform.localPosition.z + factor * sensitivity
                ); // scale multiplied by the factor we calculated

                initialDistance = currentDistance;
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
