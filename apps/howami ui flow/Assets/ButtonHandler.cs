using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSelected(bool selected)
    {
        transform.Find("Image").GetComponent<UnityEngine.UI.Image>().color = (selected ? Color.red : Color.white);
    }
}
