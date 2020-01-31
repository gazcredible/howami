using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITestbed : MonoBehaviour
{
    // Start is called before the first frame update

    public Color textHighlight;

    public bool hamburgerMenuActive;
    public String mode;

    void Start()
    {
        hamburgerMenuActive = false;
        mode = "splash";

        transform.Find("ui_background").Find("splash").gameObject.SetActive(true);
        transform.Find("ui_background").Find("new_response").gameObject.SetActive(false);
        transform.Find("ui_background").Find("review_current").gameObject.SetActive(false);
        transform.Find("ui_background").Find("review_historic").gameObject.SetActive(false);
        transform.Find("ui_background").Find("support").gameObject.SetActive(false);
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
        }      
    }
}
