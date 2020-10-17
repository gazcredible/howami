using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hyperlinkclicker : MonoBehaviour
{
    // Start is called before the first frame update

    public string label;
    public string hyperlink;

    void Start()
    {
        transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = label;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onclick()
    {
        Application.OpenURL(hyperlink);
    }
}
