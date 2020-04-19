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
    private KeyValuePair<UserResponse, int>[] historicPieChart;

    protected UnityEngine.GameObject reviewCurrent;

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

        reviewCurrent.transform.SetParent(transform);
        var rt = reviewCurrent.GetComponent<RectTransform>();
        rt.localPosition = UnityEngine.Vector3.zero;

        rt = reviewCurrent.GetComponent<RectTransform>();
        rt.localScale = new Vector3(1, 1, 1);

        reviewCurrent.SetActive(false);
        
    }

    void Awake()
    {
        Init();

        transform.Find("historic-overview").gameObject.SetActive(false);
        transform.Find("historic-summary").gameObject.SetActive(false);
        reviewCurrent.SetActive(false);
    }

    public override void OnPageSelected()
    {
        currentMode = Mode.Overview;

        currentTime = DateTime.Now;
        
        historicResponses = GameObject.Find("Canvas").GetComponent<UITestbed>().userData.GetHistoricData(currentTime);
        historicPieChart = GameObject.Find("Canvas").GetComponent<UITestbed>().userData.SummariseData(historicResponses);


        var i = 0;
        
        for (i = 0; i < 6; i++)
        {
            transform.Find("historic-overview").Find("historic-overview-month (" + i + ")").gameObject.SetActive(false);
        } 
        
        Update();
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
            {
                transform.Find("historic-overview").gameObject.SetActive(false);
                transform.Find("historic-summary").gameObject.SetActive(true);
                reviewCurrent.SetActive(false);

                var root = transform.Find("historic-summary").transform;
                

                    /*
                     * get all the historic data and rank feedback
                     * then choose the biggest N items and display
                     */

                    
                    if (historicPieChart.Length > 3)
                    {
                        //get the first three and scale
                        for (var i = 0; i < 3; i++)
                        {
                            root.Find("average-mouths").transform.Find("mouth_model ("+i+")").GetComponent<ui_mouth_model>().SetMouth((int)historicPieChart[i].Key);
                            root.Find("average-mouths").transform.Find("percentage (" + i + ")").GetComponent<UnityEngine.UI.Text>().text = historicPieChart[i].Value+"%";
                        }

                    }

                    var str = "";

                    foreach (var kvp in historicPieChart)
                    {
                        str += kvp.Key + " " + kvp.Value + "%" + "\n";
                    }

                    root.Find("summary_text").GetComponent<UnityEngine.UI.Text>().text = str;                    
                }


                break;

            case Mode.Overview:
            { 
                transform.Find("historic-overview").gameObject.SetActive(true);
                transform.Find("historic-summary").gameObject.SetActive(false);
                reviewCurrent.SetActive(false);

                if(historicResponses.Count > 0)
                {
                    var i = 0;
                    bool showOverview = false;

                    foreach (var entry in historicResponses)
                    {
                        if (entry.data.Count > 0)
                        {
                            showOverview = true;
                            transform.Find("historic-overview").Find("historic-overview-month (" + i + ")").gameObject.SetActive(true);
                            transform.Find("historic-overview").Find("historic-overview-month (" + i + ")")
                                .GetComponent<ui_historic_overview_month>()
                                .Set(this, entry);

                            i++;
                        }
                    }

                    if (showOverview == true)
                    {
                        transform.Find("historic-overview").Find("historic-overview-month-average").gameObject.SetActive(true);
                        transform.Find("historic-overview").Find("historic-overview-month-average")
                            .GetComponent<ui_historic_overview_month>()
                            .Set(this, historicPieChart);

                        transform.Find("historic-overview").Find("no-entries").gameObject.SetActive(false);
                    }
                    else
                    {
                        transform.Find("historic-overview").Find("historic-overview-month-average").gameObject.SetActive(false);
                        transform.Find("historic-overview").Find("no-entries").gameObject.SetActive(true);
                    }
                }
            }
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
