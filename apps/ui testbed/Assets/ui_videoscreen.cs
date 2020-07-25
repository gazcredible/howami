using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_videoscreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.Find("playa").GetComponent<playa>().isVideoDone == true)
        {
            //when video is complete
            GameObject.Find("Canvas").GetComponent<UITestbed>().userData.video_watched = true;
            GameObject.Find("Canvas").GetComponent<UITestbed>().userData.Save();
            
            GameObject.Find("Canvas").GetComponent<UITestbed>().OnHamburgerSelect("splash");
        }
    }
}
