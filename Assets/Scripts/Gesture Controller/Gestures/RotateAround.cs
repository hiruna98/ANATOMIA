using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    private Camera cam;

    private bool rotating = false;

    public GameObject rotatePoint;


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = cam.ScreenPointToRay(touch.position);
                RaycastHit hit;

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if(Physics.Raycast(ray, out hit)){
                            GameObject hitObject = hit.transform.gameObject;
                            if(hitObject.tag == "Object"){
                                rotating = true;
                            }
                        }
                        break;
                    case TouchPhase.Moved:
                        if(rotating){
                           // transform.Rotate(-touch.deltaPosition.y,-touch.deltaPosition.x,0f, Space.Self);
                            transform.RotateAround(rotatePoint.transform.position,Vector3.down,touch.deltaPosition.x);
                            transform.RotateAround(rotatePoint.transform.position,Vector3.right,touch.deltaPosition.y);
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
