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

    private DataStore()
    {
       
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
            return(new DataModel("","","","",""));
        }
        
    }

    public void WriteOrgan(string name){
        
    }


}
