using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug_Screen : MonoBehaviour {

    static public debug_Screen instance;
    public string text = "";

    // Use this for initialization
    void Start ()
    {
        instance = this;	
	}
	
	// Update is called once per frame
	void Update ()
    {
#if true
        transform.Find("gfx").gameObject.SetActive(false);        
#else
        var pos = Input.mousePosition;

        transform.Find("debug_mouse").GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(pos.x, pos.y, 0), Quaternion.Euler(0, 0, 0));
        transform.Find("debug_text").GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(pos.x, pos.y, 0), Quaternion.Euler(0, 0, 0));

        //pos.x -= Screen.width / 2;
        pos.y = Screen.height-pos.y;

        transform.Find("debug_text").GetComponent<UnityEngine.UI.Text>().text = pos.x.ToString("0.00") + ":" + pos.y.ToString("0.00");
        transform.Find("text").GetComponent<UnityEngine.UI.Text>().text = text;
        transform.Find("text_bg").GetComponent<UnityEngine.UI.Text>().text = text;
#endif

        text = "";
    }
}
