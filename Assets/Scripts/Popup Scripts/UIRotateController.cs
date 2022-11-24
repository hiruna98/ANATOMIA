using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotateController
{

    private float upperY = 10f;
    private float lowerY = -10f;
    private float leftX = -46f;
    private float rightX = 44f;

    private UIRotateController()
    {
        
    }

    private static UIRotateController instance = null;
    public static UIRotateController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIRotateController();
            }
            return instance;
        }
    }

    public void Rotate(Transform root, Transform t){
        string prefName = t.gameObject.name + "DefaultZ";
        float defaultZ = PlayerPrefs.GetInt(prefName, 0); 
        if(root.position.x < leftX){
            t.rotation = Quaternion.Euler(0,0,defaultZ-90);
        }else if(root.position.x > rightX){
            t.rotation = Quaternion.Euler(0,0,defaultZ+90);
        }else{
            if(root.position.y > upperY){
                t.rotation = Quaternion.Euler(0,0,defaultZ+180);
            }else if(root.position.y < lowerY){
                t.rotation = Quaternion.Euler(0,0,defaultZ);
            }else{
                //t.rotation = Quaternion.Euler(0,0,defaultZ);
            }
        }
        if(t.gameObject.name == "RM Background")
        {
            Transform elements = t.Find("Elements");
            foreach (Transform child in elements){
                Transform text = child.Find("Button/Text");
                if(root.position.x < leftX){
                    text.rotation = Quaternion.Euler(0,0,defaultZ-90);
                }else if(root.position.x > rightX){
                    text.rotation = Quaternion.Euler(0,0,defaultZ+90);
                }else{
                    if(root.position.y > upperY){
                        text.rotation = Quaternion.Euler(0,0,defaultZ+180);
                    }else if(root.position.y < lowerY){
                        text.rotation = Quaternion.Euler(0,0,defaultZ);
                    }else{
                        //text.rotation = Quaternion.Euler(0,0,defaultZ);
                    }
                }
            }
        }
        
    }

}
