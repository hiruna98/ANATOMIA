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

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        dataStore = DataStore.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
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
                else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved) // if touch is moved
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
