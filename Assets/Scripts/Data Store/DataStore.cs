using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;
using System.Net;
using Newtonsoft.Json;

public class DataStore
{
    private DataModel dataModel;

    private bool crossSectionSelection = false;

    private bool isObjectCut = false;

    private Vector3 cutPointA;
    private Vector3 cutPointB;
    private Vector3 cutPointC;


    private DataStore()
    {
       cutPointA = new Vector3(0,0,0);
       cutPointB = new Vector3(0,0,0);
       cutPointC = new Vector3(0,0,0);
    }

    private static DataStore instance = null;
    public static DataStore Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataStore();
            }
            return instance;
        }
    }

    public bool getCrossSectionSelection(){
        return crossSectionSelection;
    }

    public void setCrossSectionSelection(bool value){
        crossSectionSelection = value;
    }

    public bool getIsObjectCut(){
        return isObjectCut;
    }

    public void setIsObjectCut(bool value){
        isObjectCut = value;
    }

    public DataModel FindOrgan(string name){
        try{
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://localhost:3000/organ/"+name));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            DataModel info = JsonConvert.DeserializeObject<DataModel>(jsonResponse);

            return info;
        }
        catch(Exception e){
            Debug.Log(e);
            return(new DataModel("","","","","",""));
        }
        
    }

    public void WriteOrgan(string name){
        
    }

    public Vector3 getCutPointA(){
        return cutPointA;
    }

    public void setCutPointA(Vector3 v){
        this.cutPointA = v;
    }

    public Vector3 getCutPointB(){
        return cutPointB;
    }

    public void setCutPointB(Vector3 v){
        this.cutPointB = v;
    }

    public Vector3 getCutPointC(){
        return cutPointC;
    }

    public void setCutPointC(Vector3 v){
        this.cutPointC = v;
    }


}
