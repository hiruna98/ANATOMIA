using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class FileUploader : MonoBehaviour
{

    string path;
    public RawImage rawImage;
    public RawImage rawImage2;

    public byte alpha = 255;

    public void OpenFileBrowser()
    {
#if UNITY_EDITOR
        path = EditorUtility.OpenFilePanel("Select an image", "", "png,jpg,jpeg");
#endif

        if (rawImage.texture == null)
        {
            GetImage();
        }
        else
        {
            GetImage2();
        }
    }

    void GetImage()
    {
        if (path != null)
        {
            UpdateImage();
        }

        // if (rawImage.texture != null)
        // {
        //     UpdateImage2();
        // }
    }

    void GetImage2()
    {
        if (path != null)
        {
            UpdateImage2();
        }


    }

    void UpdateImage()
    {
        WWW www = new WWW("file:///" + path);
        rawImage.texture = www.texture;
        Color color;
        color = new Color32(0, 0, 0, alpha);
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, color.a);
    }

    void UpdateImage2()
    {
        WWW www = new WWW("file:///" + path);
        rawImage2.texture = www.texture;
        Color color;
        color = new Color32(0, 0, 0, alpha);
        rawImage2.color = new Color(rawImage2.color.r, rawImage2.color.g, rawImage2.color.b, color.a);
    }

}
