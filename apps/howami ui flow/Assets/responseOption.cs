using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class responseOption : MonoBehaviour {

    // Use this for initialization
    public Color selectedColour;
    public Color nonselectedColour;

    void Start ()
    {
        transform.Find("outer_image").GetComponent<UnityEngine.UI.Image>().color = selectedColour;
        SetSelected(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init(string text)
    {
        SetSelected(false);

        transform.Find("text_section").Find("text").GetComponent<UnityEngine.UI.Text>().text = text;
    }

    public void SetSelected(bool selected)
    {
        transform.Find("inner_image").GetComponent<UnityEngine.UI.Image>().color = (selected ? selectedColour : nonselectedColour);
    }
}
