using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITestbed : MonoBehaviour
{
    // Start is called before the first frame update

    public Color textHighlight;
    public Color textNormal;
    public Color mouthNoirmal;

    public bool hamburgerMenuActive;
    public String mode;

    public UserData userData;

    void Start()
    {
        hamburgerMenuActive = false;
        mode = "splash";

        transform.Find("ui_background").Find("splash").gameObject.SetActive(true);
        transform.Find("ui_background").Find("new_response").gameObject.SetActive(false);
        transform.Find("ui_background").Find("review_current").gameObject.SetActive(false);
        transform.Find("ui_background").Find("review_historic").gameObject.SetActive(false);
        transform.Find("ui_background").Find("support").gameObject.SetActive(false);

        userData = new UserData();
        userData.Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch(mode)
        {
            case "splash":
                break;

            case "new_response":
                break;

            case "review_current":
                break;

            case "review_historic":
                break;

            case "support":
                break;

            default:
                Debug.LogError(mode + " not supported");
                return;
        }
    }

    public void OnHamburgerToggle()
    {
        hamburgerMenuActive = !hamburgerMenuActive;
    }

    public void OnHamburgerSelect(String option)
    {
        if(option != mode)
        {                        
            transform.Find("ui_background").Find(mode).gameObject.SetActive(false);

            mode = option;

            transform.Find("ui_background").Find(mode).gameObject.SetActive(true);

            hamburgerMenuActive = false;

            //on new page
            switch (mode)
            {
                case "splash":
                    break;

                case "new_response":
                    transform.Find("ui_background").Find(mode).GetComponent<ui_new_response>().OnPageSelected();
                    break;

                case "review_current":
                    transform.Find("ui_background").Find(mode).GetComponent<ui_review_current>().OnPageSelected();
                    break;

                case "review_historic":
                    transform.Find("ui_background").Find(mode).GetComponent<ui_review_historic>().OnPageSelected();
                    break;

                case "support":
                    transform.Find("ui_background").Find(mode).GetComponent<ui_support>().OnPageSelected();
                    break;

                default:
                    Debug.LogError(mode + " not supported");
                    return;
            }
        }      
    }
}

/*
Questions:
Role: In the last month, have been clear about your role in the job?
Demand: In the last month, in your work and personal life, how demanding has your situation been?
Support: In the last month, how much support have you received in your work and personal life?
Relationships: Have the relationships at work been positive?
Control: in the last month, do you feel you have had enough say in how you do your work?
Change: In the last month, have any changes to your work been well communicated to you?
 
 */