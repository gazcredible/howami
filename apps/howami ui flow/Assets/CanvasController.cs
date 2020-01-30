using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{

    Dictionary<string, GameObject> screenLookup;

    string currentScreen = "";

    void Start()
    {

    }

    public void OnStartUp()
    {
        screenLookup = new Dictionary<string, GameObject>();

        for (var i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.ToLower().Contains("screen") == true)
            {
                screenLookup.Add(transform.GetChild(i).name, transform.GetChild(i).gameObject);
            }
        }

        foreach (var kvp in screenLookup)
        {
            kvp.Value.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetScreen(string name)
    {
        foreach (var kvp in screenLookup)
        {
            kvp.Value.SetActive(false);

            if (kvp.Key.ToLower() == name.ToLower())
            {
                if (currentScreen != name)
                {
                    currentScreen = name;
                    kvp.Value.SetActive(true);

                    kvp.Value.GetComponent<BaseScreen>().OnBecomeActive();
                }
            }
        }
    }

    public GameObject GetScreen(string name)
    {
        return screenLookup[name];
    }
}