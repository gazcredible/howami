using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiHamburger : MonoBehaviour
{
    void Awake()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(340, 640, false);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("hamburger_options").gameObject.SetActive(GameObject.Find("Canvas").GetComponent<UITestbed>().hamburgerMenuActive);
    }
}
