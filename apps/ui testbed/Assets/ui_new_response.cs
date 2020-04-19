using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ui_new_response : UIBase
{    
    int currentQuestion;
    bool doNarrative;
    bool isHeld;
    
    HowamiQuestion[] questions;

    UserResponse[] responses;

    public override void OnPageSelected()
    {
        currentQuestion = 0;
        doNarrative = false;

        responses = new UserResponse[6];

        for (var i = 0; i < responses.Length; i++)
        {
            responses[i] = UserResponse.med;
        }

        var cq = transform.Find("current-question");
        questions = GameObject.Find("Canvas").GetComponent<UITestbed>().userData.questions;

        cq.gameObject.SetActive(true);
        cq.transform.Find("question-title").GetComponent<UnityEngine.UI.Text>().text = questions[currentQuestion].title;
        cq.transform.Find("question-detail").GetComponent<UnityEngine.UI.Text>().text = questions[currentQuestion].detail;
        
        cq.transform.Find("next-prev-buttons").Find("prev").gameObject.SetActive(false);
        cq.transform.Find("next-prev-buttons").Find("next").gameObject.SetActive(false);
        cq.transform.Find("next-prev-buttons").Find("first").gameObject.SetActive(true);
        cq.transform.Find("next-prev-buttons").Find("commit").gameObject.SetActive(false);

        transform.Find("narrative").gameObject.SetActive(false);

        SetMouth(responses[currentQuestion]);

        isHeld = false;
    }

    void SetMouth(UserResponse response)        
    {
        transform.Find("current-question").Find("mouth").Find("mouth-0").gameObject.SetActive(response == UserResponse.worst);
        transform.Find("current-question").Find("mouth").Find("mouth-1").gameObject.SetActive(response == UserResponse.med_worst);
        transform.Find("current-question").Find("mouth").Find("mouth-2").gameObject.SetActive(response == UserResponse.med);
        transform.Find("current-question").Find("mouth").Find("mouth-3").gameObject.SetActive(response == UserResponse.med_good);
        transform.Find("current-question").Find("mouth").Find("mouth-4").gameObject.SetActive(response == UserResponse.good);

        var cq = transform.Find("current-question");
        var userData = GameObject.Find("Canvas").GetComponent<UITestbed>().userData;

        cq.gameObject.SetActive(true);
        cq.transform.Find("question-guidance").GetComponent<UnityEngine.UI.Text>().text = userData.GetQuestionResponse(userData.GetDimensionEnum(currentQuestion), response);
    }

    float scaled_height(float val)
    {
        return (val * Screen.height) / 1280;
    }

    UserResponse GetUserResponse(Vector3 mousePos)
    {
        if ((mousePos.y > scaled_height(430) ) && (mousePos.y < scaled_height(520) ))
        {
            return UserResponse.worst;
        }

        if ((mousePos.y > scaled_height(520) ) && (mousePos.y < scaled_height(600) ))
        {
            return UserResponse.med_worst;
        }

        if ((mousePos.y > scaled_height(600) ) && (mousePos.y < scaled_height(730) ))
        {
            return UserResponse.med;
        }

        if ((mousePos.y > scaled_height(730) ) && (mousePos.y < scaled_height(820) ))
        {
            return UserResponse.med_good;
        }

        if ((mousePos.y > scaled_height(820) ) && (mousePos.y < scaled_height(900) ))
        {
            return UserResponse.good;
        }

        return UserResponse.unselected;
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


            if (currentQuestion == 0)
            {
                cq.transform.Find("next-prev-buttons").Find("first").gameObject.SetActive(true);
                cq.transform.Find("next-prev-buttons").Find("prev").gameObject.SetActive(false);
                cq.transform.Find("next-prev-buttons").Find("next").gameObject.SetActive(false);
                cq.transform.Find("next-prev-buttons").Find("commit").gameObject.SetActive(false);
            }
            else
            {
                cq.transform.Find("next-prev-buttons").Find("first").gameObject.SetActive(false);
                cq.transform.Find("next-prev-buttons").Find("prev").gameObject.SetActive(currentQuestion != 0);

                if ( (currentQuestion+1) < questions.Length)
                {
                    cq.transform.Find("next-prev-buttons").Find("next").gameObject.SetActive(true);
                    cq.transform.Find("next-prev-buttons").Find("commit").gameObject.SetActive(false);
                }
                else
                {
                    cq.transform.Find("next-prev-buttons").Find("next").gameObject.SetActive(false);
                    cq.transform.Find("next-prev-buttons").Find("commit").gameObject.SetActive(true);
                }                
            }

            var pos = Input.mousePosition;

            cq.transform.Find("debug_mouse").gameObject.SetActive(false);
            cq.transform.Find("debug_text").gameObject.SetActive(false);

            cq.transform.Find("debug_mouse").GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(pos.x, pos.y, 0), Quaternion.Euler(0, 0, 0));

            pos.y = Screen.height - pos.y;


            cq.transform.Find("debug_mouse").GetComponent<UnityEngine.UI.Image>().color = Input.GetKey(KeyCode.Mouse0) ? Color.red : Color.green;
            cq.transform.Find("debug_text").GetComponent<UnityEngine.UI.Text>().text = pos.x.ToString("0") + ":" + pos.y.ToString("00")+"\n"+Screen.width + ":"+Screen.height;




            if (isHeld == false)
            {
                if (Input.GetKey(KeyCode.Mouse0) == true)
                {
                    isHeld = (GetUserResponse(pos) == responses[currentQuestion]);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Mouse0) == false)
                {
                    isHeld = false;
                }
                else
                {
                    var response = GetUserResponse(pos);

                    if (response != UserResponse.unselected)
                    {
                        if (response == (responses[currentQuestion] + 1))
                        {
                            responses[currentQuestion] = response;
                        }
                        else if (response == (responses[currentQuestion] - 1))
                        {
                            responses[currentQuestion] = response;
                        }
                    }
                }
            }

            SetMouth(responses[currentQuestion]);
        }
    }

    public void OnNext()
    {
        if(currentQuestion+1 < questions.Length)
        {
            //store question result ....
            currentQuestion++;
            doNarrative = false;
            SetMouth(responses[currentQuestion]);
        }
        else
        {
            OnCommit();
            //doNarrative = true;
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

                //dig out old question response
                SetMouth(responses[currentQuestion]);
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

        var userData = GameObject.Find("Canvas").GetComponent<UITestbed>().userData;

        var record = new UserRecord(DateTime.Now);

        for (var i = 0; i < responses.Length; i++)
        {
            var res = new UserRecord.Response();
            res.narrative = "";
            res.response = responses[i];
            record.AddResponse(userData.GetDimensionEnum(i), res);
        }

        userData.data.Add(record.date,record);
    }
}
