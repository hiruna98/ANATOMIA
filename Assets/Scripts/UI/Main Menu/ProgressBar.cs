using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int maximum;
    public int current; 
    public Image mask; 

    void Start()
    {
        
    }

    void Update()
    {
        getCurrentFillAmount();
    }

    void getCurrentFillAmount()
    {
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
    }
}
