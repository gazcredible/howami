using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_mouth_model : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMouth(int level)
    {
        if (transform.Find("mouth-0") == null)
        {
            return;
        }

        transform.Find("mouth-0").gameObject.SetActive(level == 0);
        transform.Find("mouth-1").gameObject.SetActive(level == 1);
        transform.Find("mouth-2").gameObject.SetActive(level == 2);
        transform.Find("mouth-3").gameObject.SetActive(level == 3);
        transform.Find("mouth-4").gameObject.SetActive(level == 4);
    }
}
