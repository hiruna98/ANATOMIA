using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersStore
{
    struct Layer {
        public GameObject obj;

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

    public void initializeLayers(List<GameObject> list)
    {
        list.ForEach(obj => {
            Layer l = new Layer();
            l.obj = obj;
            l.isActive = obj.activeSelf;
            l.isEnable = true;
            Debug.Log(obj.name);
            layers.Add(l);
        });
        activeLayers = list.Count;
        enableLayers = activeLayers;
        Debug.Log("Initialized");

    }

    public void addLayer(){
        if(removedLayers.Count > 0){
            Layer l = removedLayers.Pop();
            l.obj.SetActive(true);
            l.isActive = true;
            activeLayers++;
        }
        Debug.Log("Add");
    }

    public void removeLayer(){
        bool removedLayer = false;
        if(activeLayers >0){
            layers.ForEach(obj=>{
                if(obj.obj.activeSelf == true && obj.isEnable==true && removedLayer == false){
                    obj.obj.SetActive(false);
                    obj.isActive = false;
                    removedLayer = true;
                    activeLayers--;
                    removedLayers.Push(obj);
                }
            });
        }
        Debug.Log("Remove" + activeLayers);
    }
}
