using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ui_review_month_responses : UIBase
{
    int currentResponseIndex;

    enum Mode
    {
        Overview,
        Dimension_Summary,
        Summary,
    };

    private Mode currentMode;
    int dimensionDetailID;

    private UIBase callingObject;
    
    void Start()
    {
        
    }

    private UserData.HistoricData data;
    public void SetData(UIBase callingObject,UserData.HistoricData data)
    {
        this.callingObject = callingObject;
        this.data = data;
        currentMode = Mode.Overview;
        
        currentResponseIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var userData = GameObject.Find("Canvas").GetComponent<UITestbed>().userData;
        
        switch (this.currentMode)
        {
            case Mode.Overview:
            {
                transform.Find("current-overview").gameObject.SetActive(true);
                transform.Find("dimension-summary").gameObject.SetActive(false);
                transform.Find("current-summary").gameObject.SetActive(false);

                var root = transform.Find("current-overview");

                if ((data.data != null) && (data.data.Count > 0))
                {
                    root.Find("record").GetComponent<UnityEngine.UI.Text>().text =
                        "Record " + (currentResponseIndex + 1) + " of " + data.data.Count + "\n"
                        + data.data[currentResponseIndex].date.ToString();

                    for (var i = 0; i < 6; i++)
                    {
                        var label = userData.GetDimensionLabel(i);

                        root.Find("current-overview-item (" + i + ")").GetComponent<ui_current_overview_item>()
                            .Setup(this, i, label,
                                userData.GetQuestionResponse(data.data[currentResponseIndex], label));
                    }

                    root.Find("prev").gameObject.SetActive(currentResponseIndex != 0);
                    root.Find("next").gameObject.SetActive(currentResponseIndex + 1 < data.data.Count);
                }
                else
                {
                    root.Find("record").GetComponent<UnityEngine.UI.Text>().text = "No Responses for current month";
                    root.Find("prev").gameObject.SetActive(false);
                    root.Find("next").gameObject.SetActive(false);
                }

            }
                break;

            case Mode.Summary:
            {
                transform.Find("current-overview").gameObject.SetActive(false);
                transform.Find("dimension-summary").gameObject.SetActive(false);
                transform.Find("current-summary").gameObject.SetActive(true);

                var root = transform.Find("current-summary");
                root.Find("average-label").GetComponent<UnityEngine.UI.Text>().text =
                    "My average for " + data.time.ToString("MMMM", CultureInfo.InvariantCulture) + " " + data.time.Year;
            }
                break;

            case Mode.Dimension_Summary:
            {
                transform.Find("current-overview").gameObject.SetActive(false);
                transform.Find("dimension-summary").gameObject.SetActive(true);
                transform.Find("current-summary").gameObject.SetActive(false);


                var root = transform.Find("dimension-summary");
                
                var label = userData.GetDimensionLabel(dimensionDetailID);
                
                root.Find("item").Find("dimension").GetComponent<UnityEngine.UI.Text>().text = label;
                root.Find("item").Find("value").GetComponent<UnityEngine.UI.Text>().text = userData.GetQuestionResponse(data.data[currentResponseIndex], label);
            }
                break;
        }


        
    }

    public void OnPageSelected()
    {
    }

    public void OnNotes(string dimension)
    {
        currentMode = Mode.Dimension_Summary;
    }

    public void OnCurrentSummary()
    {
        currentMode = Mode.Summary;
    }

    public void OnNotesBack()
    {
        currentMode = Mode.Overview;
    }

    public void OnMainMenu()
    {
        //GameObject.Find("Canvas").GetComponent<UITestbed>().OnHamburgerSelect("splash");
    }

    public void OnNext()
    {
        if (currentResponseIndex + 1 < data.data.Count)
        {
            currentResponseIndex++;
            
        }        
    }

    public void OnPrev()
    {
        
        if (currentResponseIndex > 0)
        {
            currentResponseIndex--;
        }        
    }

    public void OnBack(string context)
    {
        //go back where you came from
        if (context == "dimension-summary")
        {
            currentMode = Mode.Overview;
            return;
        }

        if (context == "current-overview")
        {
            Debug.LogWarning(context);
            callingObject.OnBack();
            return;
        }

        throw new Exception(context + " unhandled");
    }

    public void OnDetailedResults()
    {
        
    }
    
    public void OnDimensionDetail(int i)
    {
        currentMode = Mode.Dimension_Summary;
        dimensionDetailID = i;
    }
}
