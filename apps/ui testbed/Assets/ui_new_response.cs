using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class HowamiQuestion
{
    public string title;
    public string detail;

    public HowamiQuestion(string title, string detail)
    {
        this.title = title;
        this.detail = detail;
    }
}

public class ui_new_response : MonoBehaviour
{
    HowamiQuestion[] questions =
    {
          new HowamiQuestion("How am I in my role:", "In the last month, have I been clear about my role in the job?"),
          new HowamiQuestion("How am I with the demands of life:", "In the last month, how demanding has my work and personal life been?"),
          new HowamiQuestion("How am I with the support I receive:", "In the last month, how much support have I received in work and personal life?"),
          new HowamiQuestion("How am I with my relationships:", "In the last month, have the relationships I hav e at work been positive?"),
          new HowamiQuestion("How am I with the control I have:", "In the last month, do I feel I have had enough sat in how I do my work?"),
          new HowamiQuestion("How am I with the change:", "In the last month, have any changes in my work been well communicated with me?"),
    };

    int currentQuestion;
    bool doNarrative;

    public void OnPageSelected()
    {
        currentQuestion = 0;
        doNarrative = false;

        var cq = transform.Find("current-question");

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
