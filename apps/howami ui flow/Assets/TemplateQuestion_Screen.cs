using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateQuestion_Screen : BaseScreen {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {        
        var rc = transform.Find("response_widget").GetComponent<responseController>();

        Questionaire.Get().responses[Questionaire.Get().currentQuestion].value = rc.GetActiveOption();
        Questionaire.Get().responses[Questionaire.Get().currentQuestion].response = GetActiveText();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var ch = transform.GetChild(i);

                if (StressApp.RectTransformToRect(ch).Contains(StressApp.instance.mousePos) == true)
                {
                    if(ch.name == "next_button")
                    {                                                
                        if (Questionaire.Get().currentQuestion+1 == Questionaire.Get().questions.Length)
                        {
                            StressApp.instance.stateMachine.SetState(EnterOrViewState.label);
                        }
                        else
                        {
                            Questionaire.Get().currentQuestion++;
                            Init();
                        }
                    }

                    if (ch.name == "prev_button")
                    {
                        if (Questionaire.Get().currentQuestion == 0)
                        {
                            StressApp.instance.stateMachine.SetState(EnterOrViewState.label);
                        }
                        else
                        {
                            Questionaire.Get().currentQuestion--;
                            Init();
                        }
                    }
                }                
            }
        }
    }

    public void Init()
    {
        var rc = transform.Find("response_widget").GetComponent<responseController>();

        var question = Questionaire.Get().questions[Questionaire.Get().currentQuestion];

        rc.question = question.question;
        rc.red_repsonse = question.red;
        rc.amber_repsonse = question.amber;
        rc.green_repsonse = question.green;

        rc.SetActiveOption(Questionaire.Get().responses[Questionaire.Get().currentQuestion].value);
        SetActiveText(Questionaire.Get().responses[Questionaire.Get().currentQuestion].response);
    }

    public void SetActiveText(string response)
    {
        transform.Find("user_input").GetComponent<UnityEngine.UI.InputField>().text = response;
    }

    public String GetActiveText()
    {
        return transform.Find("user_input").GetComponent<UnityEngine.UI.InputField>().text;
    }


    public override void OnBecomeActive()
    {
        Init();
    }
}
