using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;


public class ui_historic_overview_month : UIBase
{
    private UserData.HistoricData data;

    private UIBase callingObject;
    public void Set(UIBase callingObject, UserData.HistoricData data)
    {
        this.callingObject = callingObject;
        this.data = data;
        
        transform.Find("month").GetComponent<UnityEngine.UI.Text>().text =  data.time.ToString("MMMM", CultureInfo.InvariantCulture) +" " + data.time.Year;        
    }

    public void OnDetail()
    {
        GameObject.Find("review_historic").GetComponent<ui_review_historic>().OnHistoricDetail(callingObject, data);
    }
}
