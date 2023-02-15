using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class FileUploader : MonoBehaviour
{
    
    string path;
    public RawImage rawImage;

    public byte alpha = 255;

    public void OpenFileBrowser(){
        #if UNITY_EDITOR
        path = EditorUtility.OpenFilePanel("Overwrite with png","","png");
        #endif
        GetImage();
    }

    void GetImage(){
        if (path != null){
           UpdateImage(); 
        }
    }

    void UpdateImage(){
        WWW www = new WWW("file:///" + path);
        rawImage.texture = www.texture;
        Color color;
        color = new Color32(0,0,0,alpha);
        rawImage.color = new Color(rawImage.color.r,rawImage.color.g,rawImage.color.b,color.a);
    }
    
}
