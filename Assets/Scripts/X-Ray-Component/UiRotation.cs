using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiRotation : MonoBehaviour
{
    private bool isRotating = false;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
{
    if (Input.touchSupported)
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                if (Input.touches.Length == 2)
                {
                    // Calculate the rotation angle using the DeltaPosition vectors
                    float angle = Mathf.Atan2(touch.deltaPosition.y, touch.deltaPosition.x) * Mathf.Rad2Deg;

                    // Scale down the rotation angle by a factor
                    angle *= 0.01f;

                    // Rotate the image by the scaled down angle
                    

                    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                    eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    List<RaycastResult> results = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
                    
                    foreach (RaycastResult result in results)
                    {
                        Debug.Log("Name: " + result.gameObject.name);
                        
                        result.gameObject.transform.parent.gameObject.transform.Rotate(Vector3.forward, angle);
                    }
                    
                }
            }
        }
    }
}


}
