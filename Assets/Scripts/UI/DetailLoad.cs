using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class DetailLoad : MonoBehaviour
{

    [SerializeField] 
    private TMP_Text title;

    [SerializeField] 
    private TMP_Text textField;

    [SerializeField] 
    private RawImage image;

    private GameObject activeObject;

    private MultiSelectStore multiSelectStore;

    private DataStore dataStore;

    private GameObject content;

    private int imageCount = 2;

  
    // Start is called before the first frame update
    void OnEnable()
    {
        multiSelectStore = MultiSelectStore.Instance;
        dataStore = DataStore.Instance;
        // content = gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        activeObject = multiSelectStore.getSelectedObjects()[0];
        DataModel organ = dataStore.FindOrgan(activeObject.name);
        title.text = organ.displayName;
        textField.text = organ.description;
        Debug.Log(organ.displayName);
        if(organ.name != ""){
            StartCoroutine(DownloadImage(organ.organImage));
        }
    }

    private void OnDisable() {
        title.text = "";
        textField.text = "";
    }

    IEnumerator DownloadImage(string MediaUrl)
    {   
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if(request.isNetworkError || request.isHttpError) 
            Debug.Log(request.error);
        else
            image.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
