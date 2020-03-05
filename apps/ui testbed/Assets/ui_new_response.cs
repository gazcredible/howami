using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ui_new_response : UIBase
{    
    int currentQuestion;
    bool doNarrative;

    HowamiQuestion[] questions;

    public override void OnPageSelected()
    {
        currentQuestion = 0;
        doNarrative = false;

        var cq = transform.Find("current-question");
        questions = GameObject.Find("Canvas").GetComponent<UITestbed>().userData.questions;

        cq.gameObject.SetActive(true);
        cq.transform.Find("question-title").GetComponent<UnityEngine.UI.Text>().text = questions[currentQuestion].title;
        cq.transform.Find("question-detail").GetComponent<UnityEngine.UI.Text>().text = questions[currentQuestion].detail;
        
        cq.transform.Find("next-prev-buttons").Find("prev").gameObject.SetActive(false);
        cq.transform.Find("next-prev-buttons").Find("next").gameObject.SetActive(true);

        transform.Find("narrative").gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var cq = transform.Find("current-question");

        if (doNarrative == true)
        {
            transform.Find("narrative").gameObject.SetActive(true);
            cq.gameObject.SetActive(false);
        }
        else
        {
            transform.Find("narrative").gameObject.SetActive(false);            

            cq.gameObject.SetActive(true);
            cq.transform.Find("question-title").GetComponent<UnityEngine.UI.Text>().text = questions[currentQuestion].title;
            cq.transform.Find("question-detail").GetComponent<UnityEngine.UI.Text>().text = questions[currentQuestion].detail;

            cq.transform.Find("next-prev-buttons").Find("prev").gameObject.SetActive(currentQuestion != 0);
            cq.transform.Find("next-prev-buttons").Find("next").gameObject.SetActive(currentQuestion < questions.Length);
        }
    }

    public void OnNext()
    {
        if(currentQuestion+1 < questions.Length)
        {
            currentQuestion++;
            doNarrative = false;
        }
        else
        {
            doNarrative = true;
        }
    }

    public void OnPrev()
    {
        if (doNarrative == true)
        {
            doNarrative = false;
        }
        else
        {
            if (currentQuestion > 0)
            {
                currentQuestion--;
            }
        }
    }

    public void OnClear()
    {
        transform.Find("narrative").Find("InputField").GetComponent<UnityEngine.UI.InputField>().text = "";
    }

    public void OnCommit()
    {
        GameObject.Find("Canvas").GetComponent<UITestbed>().OnHamburgerSelect("splash");
    }
}
