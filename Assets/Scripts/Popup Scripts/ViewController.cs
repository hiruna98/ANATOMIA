using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController 
{
    
    private Vector3 defaultRotation;

    private Vector3 defaultPosition;

    private Vector3 defaultScale;

    private Vector3 defaultCamPosition;

    private GameObject _camera;
    
    private ViewController()
    {
        
    }

    private static ViewController instance = null;
    public static ViewController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ViewController();
            }
            return instance;
        }
    }

    public void initializeRotation(GameObject obj)
    {
        defaultRotation = obj.transform.rotation.eulerAngles;
        defaultPosition = obj.transform.position;
        defaultScale = obj.transform.localScale;
        _camera = GameObject.Find("Main Camera");
        defaultCamPosition = _camera.transform.localPosition;
    }

    public void antRotation(GameObject obj)
    {
        GameObject rotatePoint = GameObject.FindWithTag("Rotation Point");

        obj.transform.RotateAround(rotatePoint.transform.position,Vector3.down,obj.transform.rotation.eulerAngles.y - defaultRotation.y);
        obj.transform.RotateAround(rotatePoint.transform.position,Vector3.right,obj.transform.rotation.eulerAngles.x - defaultRotation.x);
        obj.transform.RotateAround(rotatePoint.transform.position,Vector3.down,obj.transform.rotation.eulerAngles.y - defaultRotation.y);
        obj.transform.RotateAround(rotatePoint.transform.position,Vector3.right,obj.transform.rotation.eulerAngles.x - defaultRotation.x);
    }

    public void posRotation(GameObject obj)
    {
        antRotation(obj);
        GameObject rotatePoint = GameObject.FindWithTag("Rotation Point");

        obj.transform.RotateAround(rotatePoint.transform.position,Vector3.down,180);
    }

    public void latRotation(GameObject obj)
    {
        antRotation(obj);
        GameObject rotatePoint = GameObject.FindWithTag("Rotation Point");

        obj.transform.RotateAround(rotatePoint.transform.position,Vector3.down,90);
    }

    public void medRotation(GameObject obj)
    {
        antRotation(obj);
        GameObject rotatePoint = GameObject.FindWithTag("Rotation Point");

        obj.transform.RotateAround(rotatePoint.transform.position,Vector3.down,-90);
    }

    public void supRotation(GameObject obj)
    {
        antRotation(obj);
        GameObject rotatePoint = GameObject.FindWithTag("Rotation Point");

        obj.transform.RotateAround(rotatePoint.transform.position,Vector3.right,-90);
    }

    public void infRotation(GameObject obj)
    {
        antRotation(obj);
        GameObject rotatePoint = GameObject.FindWithTag("Rotation Point");

        obj.transform.RotateAround(rotatePoint.transform.position,Vector3.right,90);
    }

    public void centerPos(GameObject obj){
        obj.transform.position = defaultPosition;
        obj.transform.localScale = defaultScale;
        _camera.transform.localPosition = defaultCamPosition;
    }
}
