using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_review_historic : UIBase
{
    //historic-overview -> howami for 6 months
    //historic-summary -> average for 6 months

    protected DateTime currentTime;
    List<UserData.HistoricData> historicResponses;

    public UnityEngine.GameObject reviewCurrent;

    protected enum Mode
    {
        Overview,
        Detail,
        Summary
    };

    protected Mode currentMode;

    public void Init()
    {
        reviewCurrent = Instantiate(Resources.Load("prefabs/review_month_responses")) as GameObject;

        reviewCurrent.transform.parent = transform;
        var rt = reviewCurrent.GetComponent<RectTransform>();
        rt.localPosition = UnityEngine.Vector3.zero;

        reviewCurrent.SetActive(false);
    }

    void Awake()
    {
        Init();
    }
    public override void OnPageSelected()
    {
        currentMode = Mode.Overview;

        currentTime = DateTime.Now;
        
        historicResponses = GameObject.Find("Canvas").GetComponent<UITestbed>().userData.GetHistoricData(currentTime);

        var i = 0;
        
        for (i = 0; i < 6; i++)
        {
            transform.Find("historic-overview").Find("historic-overview-month (" + i + ")").gameObject.SetActive(false);
        }

        i = 0;
        foreach (var entry in historicResponses)
        {
            if (entry.data.Count > 0)
            {
                transform.Find("historic-overview").Find("historic-overview-month (" + i + ")").gameObject
                    .SetActive(true);
                transform.Find("historic-overview").Find("historic-overview-month (" + i + ")")
                    .GetComponent<ui_historic_overview_month>()
                    .Set(this, entry);

                i++;
            }
        }

        transform.Find("historic-overview").Find("historic-overview-month-average")
            .GetComponent<ui_historic_overview_month>()
            .Set(this, historicResponses);
    }

    void Update()
    {
        switch (currentMode)
        {
            case Mode.Detail:
                transform.Find("historic-overview").gameObject.SetActive(false);
                transform.Find("historic-summary").gameObject.SetActive(false);
                reviewCurrent.SetActive(true);
                break;
            
            case Mode.Summary:
                transform.Find("historic-overview").gameObject.SetActive(false);
                transform.Find("historic-summary").gameObject.SetActive(true);
                reviewCurrent.SetActive(false);
                break;
            
            case Mode.Overview:
                transform.Find("historic-overview").gameObject.SetActive(true);        
                transform.Find("historic-summary").gameObject.SetActive(false);
                reviewCurrent.SetActive(false);
                break;
        }
    }

    public void OnGotoHistoricSummary()
    {
        currentMode = Mode.Summary;
    }

    public void OnMainMenu()
    {
        GameObject.Find("Canvas").GetComponent<UITestbed>().OnHamburgerSelect("splash");
    }

    public void OnOverview_SelectDetail(String s)
    {
        currentMode = Mode.Detail;
    }

    public void OnHistoricDetail(UIBase callingObject, UserData.HistoricData data)
    {
        reviewCurrent.GetComponent<ui_review_month_responses>().SetData(callingObject, data);
        reviewCurrent.GetComponent<ui_review_month_responses>().SetWriteData(false);
        currentMode = Mode.Detail;
    }
    
    public override void OnBack()
    {
        currentMode = Mode.Overview;
    }
}
