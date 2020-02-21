using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ui_review_current : UIBase
{
    List<UserRecord> currentResponses;
    int currentResponseIndex;

    DateTime currentTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var root = transform.Find("current-overview");

        if ( (currentResponses != null) && (currentResponses.Count > 0))
        {
            root.Find("record").GetComponent<UnityEngine.UI.Text>().text = "Record " + (currentResponseIndex + 1) + " of " + currentResponses.Count + "\n"
                + currentResponses[currentResponseIndex].date.ToString();


            var userData = GameObject.Find("Canvas").GetComponent<UITestbed>().userData;

            
            for (var i=0;i<6;i++)
            {
                var label = userData.GetDimensionLabel(i);

                root.Find("item (" + i + ")").Find("dimension").GetComponent<UnityEngine.UI.Text>().text = label;
                root.Find("item (" + i + ")").Find("value").GetComponent<UnityEngine.UI.Text>().text = userData.GetQuestionResponse(currentResponses[currentResponseIndex], label );
            }

            root.Find("prev").gameObject.SetActive(currentResponseIndex != 0);
            root.Find("next").gameObject.SetActive(currentResponseIndex + 1 < currentResponses.Count);
        }
        else
        {
            root.Find("record").GetComponent<UnityEngine.UI.Text>().text = "No Responses for current month";
            root.Find("prev").gameObject.SetActive(false);
            root.Find("next").gameObject.SetActive(false);
        }


        root = transform.Find("current-summary");
        root.Find("average-label").GetComponent<UnityEngine.UI.Text>().text = "My average for " + currentTime.ToString("MMMM", CultureInfo.InvariantCulture) +" " + currentTime.Year;
    }

    public void OnPageSelected()
    {
        currentTime = DateTime.Now;

        transform.Find("current-overview").gameObject.SetActive(true);
        transform.Find("dimension-summary").gameObject.SetActive(false);
        transform.Find("current-summary").gameObject.SetActive(false);

        //how many responses are there for the current month
        currentResponses = GameObject.Find("Canvas").GetComponent<UITestbed>().userData.GetCurrentRespones(currentTime);
        currentResponseIndex = 0;
    }

    public void OnNotes(string dimension)
    {
        transform.Find("current-overview").gameObject.SetActive(false);
        transform.Find("dimension-summary").gameObject.SetActive(true);
        transform.Find("current-summary").gameObject.SetActive(false);
    }

    public void OnCurrentSummary()
    {
        transform.Find("current-overview").gameObject.SetActive(false);
        transform.Find("dimension-summary").gameObject.SetActive(false);
        transform.Find("current-summary").gameObject.SetActive(true);
    }

    public void OnNotesBack()
    {
        OnPageSelected();
    }

    public void OnMainMenu()
    {
        GameObject.Find("Canvas").GetComponent<UITestbed>().OnHamburgerSelect("splash");
    }

    public void OnNext()
    {
        if (currentResponseIndex + 1 < currentResponses.Count)
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
}
