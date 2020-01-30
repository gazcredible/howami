using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radiobuttons : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        transform.Find("Button").GetComponent<UnityEngine.UI.Button>().OnSelect(null);
	}

    public void OnButtonPress(GameObject button)
    {
        button.GetComponent<UnityEngine.UI.Button>().OnSelect(null);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
