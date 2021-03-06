﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class UITestbed : MonoBehaviour
{
    // Start is called before the first frame update

    public Color textHighlight;
    public Color textNormal;
    [FormerlySerializedAs("mouthNoirmal")] public Color mouthNormal;

    [HideInInspector]
    public bool hamburgerMenuActive;
    [HideInInspector]
    public String mode;

    [HideInInspector]
    public UserData userData;
    public XMLFile textDB;

    public bool startWithVideo;
    public bool playMyLovelyVideo;
    public VideoClip properVideo;
    public VideoClip testVideo;

    public string version_data = "version 0.0.1 : 27/07/2020";
    
    [HideInInspector]
    public String[] modes = new string[]
    {
        "new_response",
        "review_current",
        "review_historic",
        "support",
    };

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        hamburgerMenuActive = false;
        
        userData = new UserData();
        userData.Load();
        

        transform.Find("ui_background").Find("video_screen").gameObject.SetActive(false);
        transform.Find("ui_background").Find("splash").gameObject.SetActive(false);
        transform.Find("ui_background").Find("new_response").gameObject.SetActive(false);
        transform.Find("ui_background").Find("review_current").gameObject.SetActive(false);
        transform.Find("ui_background").Find("review_historic").gameObject.SetActive(false);
        transform.Find("ui_background").Find("support").gameObject.SetActive(false);
        transform.Find("ui_hamburger").gameObject.SetActive(false);

        transform.Find("ui_hamburger").Find("version").GetComponent<UnityEngine.UI.Text>().text = version_data;
        
        hamburgerMenuActive = false;

        if (userData.video_watched == false)
        {
            mode = "video_screen";
        }
        else
        {
            mode = "splash";
            transform.Find("ui_hamburger").gameObject.SetActive(true);
        }

        transform.Find("ui_background").Find(mode).gameObject.SetActive(true);
        
        
        LoadText();


    }

    public void LoadText()
    {
        Stream dataStream = null;

        var resourceName = "text";
        var ext = "xml";

        bool redirect_loading = false;

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
                case "video_screen":
                    break;
                
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
            case "video_screen":
                break;

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
                case "video_screen":
                    break;

                case "splash":
                    transform.Find("ui_hamburger").gameObject.SetActive(true);
                    Screen.sleepTimeout = SleepTimeout.SystemSetting;
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