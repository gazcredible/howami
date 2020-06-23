using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_support : UIBase
{
    List<String> support_text_entries = new List<String>();

    private int current_page = 0;
    
    void Start()
    {
        OnPageSelected();
    }
    
    public override void LoadText()
    {
        support_text_entries = new List<String>();
        var text_data = GameObject.Find("Canvas").GetComponent<UITestbed>().textDB;

        var results = text_data.doc.GetElementsByTagName("text_group");
        
        for (int result_index=0; result_index < results.Count; result_index++)
        {
            if (results[result_index].Attributes.Count > 0)
            {
                var attrib = results[result_index].Attributes["type"];

                if ((attrib != null) && (attrib.Value == "support"))
                {
                    for (int node_index=0; node_index<results[result_index].ChildNodes.Count; node_index++)
                    {
                        attrib = results[result_index].ChildNodes[node_index].Attributes["text"];

                        if (attrib != null)
                        {
                            var entry = attrib.Value;

                            entry = entry.Replace('~', '\n');
                            entry = entry.Replace('[', '<');
                            entry = entry.Replace(']', '>');
                                
                            
                            support_text_entries.Add(entry);
                        }
                    }
                }
            }
        }

    }

    public override void OnPageSelected()
    {
        current_page = 0;
        
        transform.Find("next-prev-buttons").Find("prev").gameObject.SetActive(false);
        transform.Find("next-prev-buttons").Find("next").gameObject.SetActive(false);
        transform.Find("next-prev-buttons").Find("first").gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("text").GetComponent<UnityEngine.UI.Text>().text = support_text_entries[current_page];
        
        if (current_page == 0)
        {
            transform.Find("next-prev-buttons").Find("first").gameObject.SetActive(true);
            transform.Find("next-prev-buttons").Find("last").gameObject.SetActive(false);
            transform.Find("next-prev-buttons").Find("prev").gameObject.SetActive(false);
            transform.Find("next-prev-buttons").Find("next").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("next-prev-buttons").Find("first").gameObject.SetActive(false);
            transform.Find("next-prev-buttons").Find("prev").gameObject.SetActive(current_page != 0);

            if ( (current_page+1) < support_text_entries.Count)
            {
                transform.Find("next-prev-buttons").Find("next").gameObject.SetActive(true);
                transform.Find("next-prev-buttons").Find("prev").gameObject.SetActive(true);
                transform.Find("next-prev-buttons").Find("first").gameObject.SetActive(false);
                transform.Find("next-prev-buttons").Find("last").gameObject.SetActive(false);
            }
            else
            {
                transform.Find("next-prev-buttons").Find("next").gameObject.SetActive(false);
                transform.Find("next-prev-buttons").Find("prev").gameObject.SetActive(false);
                transform.Find("next-prev-buttons").Find("last").gameObject.SetActive(true);
            }                
        }
    }
    
    public void OnLaunchSite()
    {
        Application.OpenURL("http://unity3d.com/");
    }

    public void OnNext()
    {
        if (current_page + 1 < support_text_entries.Count)
        {
            current_page++;
        }
    }

    public void OnPrev()
    {
        if (current_page > 0)
        {
            current_page--;
        }
    }
}
