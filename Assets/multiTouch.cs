using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiTouch : MonoBehaviour
{
    public GameObject circle;
    public Camera cam;
    public List<touchPoint> touches = new List<touchPoint>();

	// Update is called once per frame
	void Update () {
        int i = 0;
        while(i < Input.touchCount){
            Touch t = Input.GetTouch(i);
            if(t.phase == TouchPhase.Began){
                Debug.Log("touch began");
                touches.Add(new touchPoint(t.fingerId, createCircle(t)));
            }else if(t.phase == TouchPhase.Ended){
                Debug.Log("touch ended");
                touchPoint thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                Destroy(thisTouch.circle);
                touches.RemoveAt(touches.IndexOf(thisTouch));
            }else if(t.phase == TouchPhase.Moved){
                Debug.Log("touch is moving");
                touchPoint thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                thisTouch.circle.transform.position = getTouchPosition(t.position);

            }
            ++i;
        }
	}
    Vector2 getTouchPosition(Vector2 touchPosition){
        return new Vector3(touchPosition.x, touchPosition.y, 0);
    }
    GameObject createCircle(Touch t){
        GameObject c = Instantiate(circle) as GameObject;
        c.name = "Touch" + t.fingerId;
        c.transform.position = getTouchPosition(t.position);
        c.transform.SetParent(this.gameObject.transform);
        return c;
    }
}
