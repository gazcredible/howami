using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_current_overview_item : UIBase
{
    private ui_review_month_responses command_parent;
    private int myIndex;
    

    public void Setup(ui_review_month_responses obj, int i, String label, String val)
    {
        command_parent = obj;
        myIndex = i;
        
        transform.Find("dimension").GetComponent<UnityEngine.UI.Text>().text = label;
        transform.Find("value").GetComponent<UnityEngine.UI.Text>().text = val;
    }

    public void OnSelected()
    {
        command_parent.OnDimensionDetail(myIndex);
    }
}
