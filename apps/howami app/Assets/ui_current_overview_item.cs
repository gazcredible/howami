using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_current_overview_item : UIBase
{
    private ui_review_month_responses command_parent;
    private int myIndex;

    private ui_mouth_model summaryMouth;

    void Awake()
    {
        /*
        summaryMouth = Instantiate(Resources.Load("prefabs/mouth-model")) as GameObject;

        summaryMouth.transform.parent = transform;
        summaryMouth.transform.SetAsFirstSibling();

        summaryMouth.transform.localScale = new UnityEngine.Vector3(1, 1, 1);
        summaryMouth.transform.position = transform.position - new UnityEngine.Vector3(((1080 / 2) * 1.6f) - 50, ((1920 / 2) * 1.6f) - 20, 0);

        //transform.Find("mouth").gameObject.SetActive(false);

        summaryMouth.GetComponent<ui_mouth_model>().SetMouth(4);
        */

        if (transform.Find("mouth_model") != null)
        {
            summaryMouth = transform.Find("mouth_model").GetComponent<ui_mouth_model>();
        }
    }

    public void Setup(ui_review_month_responses obj, int i, String label, String val, UserResponse response)
    {
        command_parent = obj;
        myIndex = i;
        
        transform.Find("dimension").GetComponent<UnityEngine.UI.Text>().text = label;
        transform.Find("value").GetComponent<UnityEngine.UI.Text>().text = val;

        if (summaryMouth != null)
        {
            summaryMouth.SetMouth((int)response);
        }
    }

    public void OnSelected()
    {
        command_parent.OnDimensionDetail(myIndex);
    }
}
