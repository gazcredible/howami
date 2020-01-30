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
            //do something
            mode = option;
        }      
    }
}
