using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class responseController : MonoBehaviour {
    
    GameObject currentButton = null;

    public string question;
    public string red_repsonse;
    public string amber_repsonse;
    public string green_repsonse;

    void Start ()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("response_red").GetComponent<responseOption>().Init(red_repsonse);
        transform.Find("response_amber").GetComponent<responseOption>().Init(amber_repsonse);
        transform.Find("response_green").GetComponent<responseOption>().Init(green_repsonse);

        transform.Find("question").Find("text").GetComponent<UnityEngine.UI.Text>().text = question.Replace("<br>", "\n"); ;


        GameObject selectedButton = null;        

        if (Input.GetKey(KeyCode.Mouse0))
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var ch = transform.GetChild(i);

                if (ch.name.ToLower().Contains("response") == true)
                {
                    if(StressApp.RectTransformToRect(ch).Contains(StressApp.instance.mousePos) == true)                    
                    {
                        selectedButton = ch.gameObject;
                    }
                }                
            }
        }

        debug_Screen.instance.text += "\n";
        debug_Screen.instance.text += StressApp.instance.mousePos.ToString();
        debug_Screen.instance.text += "\n";

        for (var i = 0; i < transform.childCount; i++)
        {
            var ch = transform.GetChild(i);

            if (ch.name.ToLower().Contains("response") == true)
            {
                debug_Screen.instance.text += ch.name + " " + StressApp.RectTransformToString(ch, ch.GetComponent<RectTransform>()) + "\n";

                var r = StressApp.RectTransformToRect(ch);

                debug_Screen.instance.text += r.x + ":" + r.y + "[" + r.width + ":" + r.height + "]"+ r.Contains(StressApp.instance.mousePos);

                debug_Screen.instance.text += "\n";
            }
        }

        if (selectedButton != null)
        {
            if (currentButton != null)
            {
                currentButton.GetComponent<responseOption>().SetSelected(false);
            }

            currentButton = selectedButton;
        }

        if (currentButton != null)
        {
            currentButton.GetComponent<responseOption>().SetSelected(true);
        }
    }

    public void SetActiveOption(Response.Value value)
    {
        if(value == Response.Value.NoResponse)
        {
            currentButton = null;
        }

        if(value == Response.Value.Red)
        {
            currentButton = transform.Find("response_red").gameObject;
        }

        if (value == Response.Value.Amber)
        {
            currentButton = transform.Find("response_amber").gameObject;
        }

        if (value == Response.Value.Green)
        {
            currentButton = transform.Find("response_green").gameObject;
        }
    }
    
    public Response.Value GetActiveOption()
    {
        if(currentButton == null)
        {
            return Response.Value.NoResponse;
        }

        if(currentButton.name == "response_red")
        {
            return Response.Value.Red;
        }

        if (currentButton.name == "response_amber")
        {
            return Response.Value.Amber;
        }

        if (currentButton.name == "response_green")
        {
            return Response.Value.Green;
        }

        throw new System.Exception("");
    }    
}
