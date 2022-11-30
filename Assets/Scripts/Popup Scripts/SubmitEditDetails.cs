using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmitEditDetails : MonoBehaviour
{
    // public TextMeshProUGUI organName,description;
    public TMP_InputField OrganInputField,DescriptionInputField;

    public void setName(){
        Debug.Log("Text: " + OrganInputField.text);
        Debug.Log("Text: " + DescriptionInputField.text);
    } 
}