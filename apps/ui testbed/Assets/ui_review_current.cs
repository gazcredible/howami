using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ui_review_current : ui_review_historic
{
    /*
     * how am i this month by dimension (current overview) -> notes (dimension-summary)
     * how am i overview / summary (current-summary)
     */

    void Awake()
    {
        Init();

        var rt = reviewCurrent.GetComponent<RectTransform>();
        rt.localScale = new Vector3(1,1,1);
    }

    public override void OnPageSelected()
    {
        currentMode = Mode.Overview;

        currentTime = DateTime.Now;

        reviewCurrent.SetActive(true);

        var historicResponses = GameObject.Find("Canvas").GetComponent<UITestbed>().userData.GetHistoricData(currentTime,1);

        reviewCurrent.GetComponent<ui_review_month_responses>().SetData(this, historicResponses[0]);
        reviewCurrent.GetComponent<ui_review_month_responses>().SetWriteData(true);
    }

    void Update()
    {      
    }
}