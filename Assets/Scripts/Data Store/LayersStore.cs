using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersStore
{
    struct Layer {
        public GameObject originalObj;
        public GameObject clippingObj;

        public bool isEnable;

        public bool isActive;
    }

    private List<Layer> layers;

    private Stack<Layer> removedLayers;
    private int activeLayers;

    private int enableLayers;



    private static LayersStore instance = null;

    private LayersStore(){
        layers = new List<Layer>();
        removedLayers = new Stack<Layer>();
    }
    public static LayersStore Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LayersStore();
            }
            return instance;
        }
    }

    public void initializeLayers(List<GameObject> list, GameObject clippingRoot)
    {
        list.ForEach(obj => {
            Layer l = new Layer();
            l.originalObj = obj;
            l.clippingObj = clippingRoot.transform.Find(obj.name).gameObject;
            l.isActive = obj.activeSelf;
            l.isEnable = true;
            layers.Add(l);
        });
        activeLayers = list.Count;
        enableLayers = activeLayers;

    }

    public void addLayer(){
        if(removedLayers.Count > 0){
            Layer l = removedLayers.Pop();
            l.originalObj.SetActive(true);
            l.clippingObj.SetActive(true);
            l.isActive = true;
            activeLayers++;
        }
    }

    public void removeLayer(){
        bool removedLayer = false;
        if(activeLayers >0){
            layers.ForEach(obj=>{
                if(obj.originalObj.activeSelf == true && obj.isEnable==true && removedLayer == false){
                    obj.originalObj.SetActive(false);
                    obj.clippingObj.SetActive(false);
                    obj.isActive = false;
                    removedLayer = true;
                    activeLayers--;
                    removedLayers.Push(obj);
                }
            });
        }
    }
}
