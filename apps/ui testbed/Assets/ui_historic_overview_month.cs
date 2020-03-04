﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;


public class ui_historic_overview_month : UIBase
{
    private UserData.HistoricData data;

    private GameObject summaryMouth;

    void Awake()
    {
        summaryMouth = Instantiate(Resources.Load("prefabs/mouth-model")) as GameObject;

        summaryMouth.transform.parent = transform;

        summaryMouth.transform.localScale = new UnityEngine.Vector3(1, 1, 1);
        summaryMouth.transform.position = transform.position - new UnityEngine.Vector3(((1080/2)*1.6f)-50, ((1920/2)*1.6f)+50, 0);                              

        //transform.Find("mouth").gameObject.SetActive(false);
    }


    private UIBase callingObject;
    public void Set(UIBase callingObject, UserData.HistoricData data)
    {
        this.callingObject = callingObject;
        this.data = data;

        if (summaryMouth == null)
        {
            return;
        }

        if (data != null)
        {
            SetMouth(data.GetScore());
        }
        else
        {
            SetMouth(-1);
        }

        transform.Find("month").GetComponent<UnityEngine.UI.Text>().text =  data.time.ToString("MMMM", CultureInfo.InvariantCulture) +" " + data.time.Year;        
    }

    void SetMouth(int i)
    {
        if (summaryMouth == null)
        {
            return;
        }

        summaryMouth.GetComponent<ui_mouth_model>().SetMouth(i);
    }

    public void Set(UIBase callingObject, List<UserData.HistoricData> data)
    {
        var userData = GameObject.Find("Canvas").GetComponent<UITestbed>().userData;

        SetMouth(userData.GetHistoricFeedbackAsInt(data));
    }

    public void OnDetail()
    {
        GameObject.Find("review_historic").GetComponent<ui_review_historic>().OnHistoricDetail(callingObject, data);
    }
}
