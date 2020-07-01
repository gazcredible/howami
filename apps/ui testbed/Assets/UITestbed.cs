using System;
using System.IO;
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
    public XMLFile textDB;

    public String[] modes = new string[]
    {
        "new_response",
        "review_current",
        "review_historic",
        "support",
    };

    void Start()
    {
        hamburgerMenuActive = false;
        mode = "splash";

        transform.Find("ui_background").Find("splash").gameObject.SetActive(true);
        transform.Find("ui_background").Find("new_response").gameObject.SetActive(false);
        transform.Find("ui_background").Find("review_current").gameObject.SetActive(false);
        transform.Find("ui_background").Find("review_historic").gameObject.SetActive(false);
        transform.Find("ui_background").Find("support").gameObject.SetActive(false);

        transform.Find("ui_hamburger").gameObject.SetActive(true);

        userData = new UserData();
        //userData.Init();
        //userData.Save();
        userData.Load();
        
        LoadText();


    }

    public void LoadText()
    {
        Stream dataStream = null;

        var resourceName = "text";
        var ext = "xml";

        bool redirect_loading = true;

        if (UnityEngine.Application.isEditor == true)
        {
            dataStream = new MemoryStream();

            var filename = UnityEngine.Application.dataPath + @"/Resources/" + resourceName +"." +ext;

            try
            {
                var stream = File.OpenRead(filename);
                dataStream.SetLength(stream.Length);
                stream.Read((dataStream as MemoryStream).GetBuffer(), 0, (int)stream.Length);
                stream.Close();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogWarning("File not found: " + filename + " " + ex.ToString());
            }                    
        }
        else
        {
            if (redirect_loading == true)
            {
                dataStream = new MemoryStream();

                

                var filename = Application.dataPath+"/../"+resourceName + "." + ext;

                UnityEngine.Debug.LogWarning(filename);

                try
                {
                    var stream = File.OpenRead(filename);
                    dataStream.SetLength(stream.Length);
                    stream.Read((dataStream as MemoryStream).GetBuffer(), 0, (int)stream.Length);
                    stream.Close();
                }
                catch (System.Exception ex)
                {
                    UnityEngine.Debug.LogWarning("File not found: " + filename + " " + ex.ToString());
                }
            }
            else
            {
                UnityEngine.TextAsset asset;
                asset = (UnityEngine.TextAsset)UnityEngine.Resources.Load(resourceName);

                dataStream = new MemoryStream(asset.bytes);
            }
        }
        
        textDB = new XMLLoadFile(dataStream);

        foreach (var mode in modes)
        {
            switch (mode)
            {
                case "splash":
                    break;

                case "new_response":
                    transform.Find("ui_background").Find(mode).GetComponent<ui_new_response>().LoadText();
                    break;

                case "review_current":
                    transform.Find("ui_background").Find(mode).GetComponent<ui_review_current>().LoadText();
                    break;

                case "review_historic":
                    transform.Find("ui_background").Find(mode).GetComponent<ui_review_historic>().LoadText();
                    break;

                case "support":
                    transform.Find("ui_background").Find(mode).GetComponent<ui_support>().LoadText();
                    break;

                default:
                    Debug.LogError(mode + " not supported");
                    return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.L) == true)
        {
            LoadText();
        }

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
       // if(option != mode)
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