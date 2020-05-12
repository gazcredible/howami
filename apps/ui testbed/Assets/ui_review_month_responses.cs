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

    private ui_mouth_model summaryMouth;

    void Start()
    {
        /*
        summaryMouth = Instantiate(Resources.Load("prefabs/mouth-model")) as GameObject;

        summaryMouth.transform.parent = transform.Find("current-summary");
        var rt = summaryMouth.transform.Find("Canvas").GetComponent<RectTransform>();
        rt.localPosition = new UnityEngine.Vector3(540,960);
        rt.localScale = new UnityEngine.Vector3(10, 10, 1);
        */

        
        summaryMouth = transform.Find("current-summary").transform.Find("mouth_model").GetComponent<ui_mouth_model>();

        transform.Find("current-overview").Find("next").gameObject.SetActive(false);
        transform.Find("current-overview").Find("prev").gameObject.SetActive(false);
    }

    private UserData.HistoricData data;
    public void SetData(UIBase callingObject,UserData.HistoricData data)
    {
        this.callingObject = callingObject;
        this.data = data;
        currentMode = Mode.Overview;
        
        currentResponseIndex = 0;

        transform.Find("current-overview").Find("back").gameObject.SetActive(callingObject.name != "review_current");
    }

    bool writeData;

    public void SetWriteData(bool writeData)
    {
        this.writeData = writeData;
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

                root.Find("results").gameObject.SetActive(false);

                if ((data.data != null) && (data.data.Count > 0))
                {
                    root.Find("record").GetComponent<UnityEngine.UI.Text>().text =
                        "Record " + (currentResponseIndex + 1) + " of " + data.data.Count + "\n"
                        + data.data[currentResponseIndex].date.ToString();

                    var i = 0;

                    foreach (UserData.Dimensions dimension in Enum.GetValues(typeof(UserData.Dimensions)))
                    {
                        var label = userData.GetDimensionLabel(i);

                        root.Find("current-overview-item (" + i + ")").gameObject.SetActive(true);

                        root.Find("current-overview-item (" + i + ")").GetComponent<ui_current_overview_item>()
                            .Setup(this
                                , i
                                , label
                                , userData.GetQuestionResponse(data.data[currentResponseIndex], dimension)
                                , data.data[currentResponseIndex].responses[dimension].response
                                );

                        i++;
                    }

                    root.Find("prev").gameObject.SetActive(currentResponseIndex != 0);
                    root.Find("next").gameObject.SetActive(currentResponseIndex + 1 < data.data.Count);

                    //GARETH disable
                    //root.Find("results").gameObject.SetActive(true);
                    root.Find("results").gameObject.SetActive(false);
                }
                else
                {
                    for (var i = 0; i < 6; i++)
                    {
                        var label = userData.GetDimensionLabel(i);

                        root.Find("current-overview-item (" + i + ")").gameObject.SetActive(false);
                    }

                    root.Find("record").GetComponent<UnityEngine.UI.Text>().text = "No Responses for current month";
                    root.Find("prev").gameObject.SetActive(false);
                    root.Find("next").gameObject.SetActive(false);
                    root.Find("results").gameObject.SetActive(false);
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
                
                root.Find("feedback-to-user").GetComponent<UnityEngine.UI.Text>().text = userData.GetMonthlyFeedback(data.time);

                summaryMouth.SetMouth(userData.GetMonthlyFeedbackAsInt(data.time));
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
                root.Find("item").Find("value").GetComponent<UnityEngine.UI.Text>().text = userData.GetQuestionResponse(data.data[currentResponseIndex], (UserData.Dimensions)dimensionDetailID);

                    //if review historic - only show feedback
                root.Find("enter-details").gameObject.SetActive(writeData);
                root.Find("view-details").gameObject.SetActive(!writeData);

                root.Find("recorded-tab").GetComponent<UnityEngine.UI.Text>().text =
                    "Recorded " + data.data[currentResponseIndex].date;

                if (writeData == true)
                {
                    
                    root.Find("enter-details").Find("InputField").GetComponent<UnityEngine.UI.InputField>().text = userData.GetQuestionResponseNarrative(data.data[currentResponseIndex], (UserData.Dimensions)dimensionDetailID);
                }
                else
                {                
                    root.Find("view-details").Find("Text").GetComponent<UnityEngine.UI.Text>().text = userData.GetQuestionResponseNarrative(data.data[currentResponseIndex], (UserData.Dimensions)dimensionDetailID);
                }
            }
                break;
        }


        
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

        if (context == "current-summary")
        {
            currentMode = Mode.Overview;
            return;
        }

        throw new Exception(context + " unhandled");
    }

    public void OnDetailedResults()
    {
        currentMode = Mode.Summary;
    }
    
    public void OnDimensionDetail(int i)
    {
        currentMode = Mode.Dimension_Summary;
        dimensionDetailID = i;
    }
}
