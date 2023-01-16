using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEditPopup : MonoBehaviour
{
    public GameObject DetailPopup, EditDetailPopup;

    public void HideDetailPopup(){
        DetailPopup.SetActive(false);
        EditDetailPopup.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
