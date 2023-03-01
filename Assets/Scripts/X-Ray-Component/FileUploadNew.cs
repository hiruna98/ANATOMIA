using UnityEngine;
using UnityEngine.UI;

public class FileUploadNew : MonoBehaviour
{
    public RawImage rawImage;

    public void UploadImage()
    {
        // Open a file browser dialog to allow the user to select an image
        string imagePath = UnityEditor.EditorUtility.OpenFilePanel("Select an image", "", "png,jpg,jpeg");

        if (!string.IsNullOrEmpty(imagePath))
        {
            // Load the selected image into a texture
            Texture2D tex = new Texture2D(2, 2);
            byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
            tex.LoadImage(imageData);

            // Assign the texture to the RawImage component
            rawImage.texture = tex;
        }
    }
}
