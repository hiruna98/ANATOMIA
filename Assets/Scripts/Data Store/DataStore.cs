using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;
using Realms;
using System.Linq;

public class DataStore
{
    private Realm realm;
    private DataModel dataModel;

    private bool crossSectionSelection = false;

  private DataStore()
    {
       realm = Realm.GetInstance();
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
        dataModel = realm.All<DataModel>().Where(organ=> organ.Name==name).First();
        if(dataModel == null){
            dataModel = new DataModel("","");
            return dataModel;
        }
        else{
            return dataModel;
        }
    }

    public void WriteOrgan(string name){
        realm.Write(()=> {
            realm.Add(dataModel);
        });

    }


}
