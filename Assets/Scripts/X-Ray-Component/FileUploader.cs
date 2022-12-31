using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class FileUploader : MonoBehaviour
{
    
    string path;
    public RawImage rawImage;

    public void OpenFileBrowser(){
        path = EditorUtility.OpenFilePanel("Overwrite with png","","png");
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
    }
    
}
