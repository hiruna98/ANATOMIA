using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailLoad : MonoBehaviour
{

    [SerializeField] 
    private TMP_Text title;

    [SerializeField] 
    private TMP_Text textField;

    [SerializeField] 
    private GameObject image;

    private GameObject activeObject;

    private MultiSelectStore multiSelectStore;

    private GameObject content;

    private int imageCount = 2;

    private string test = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. ";

    private string imageURI = "https://www.texasheart.org/heart-health/heart-information-center/topics/heart-anatomy/";

    // Start is called before the first frame update
    void OnEnable()
    {
        multiSelectStore = MultiSelectStore.Instance;
        // content = gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        activeObject = multiSelectStore.getSelectedObjects()[0];
        title.text = activeObject.name;
        textField.text = test.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
