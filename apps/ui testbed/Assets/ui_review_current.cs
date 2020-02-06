using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_review_current : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPageSelected()
    {
        transform.Find("current-overview").gameObject.SetActive(true);
        transform.Find("dimension-summary").gameObject.SetActive(false);
        transform.Find("current-summary").gameObject.SetActive(false);
    }

    public void OnNotes(string dimension)
    {
        transform.Find("current-overview").gameObject.SetActive(false);
        transform.Find("dimension-summary").gameObject.SetActive(true);
        transform.Find("current-summary").gameObject.SetActive(false);
    }

    public void OnCurrentSummary()
    {
        transform.Find("current-overview").gameObject.SetActive(false);
        transform.Find("dimension-summary").gameObject.SetActive(false);
        transform.Find("current-summary").gameObject.SetActive(true);
    }

    public void OnNotesBack()
    {
        OnPageSelected();
    }

    public void OnMainMenu()
    {
        GameObject.Find("Canvas").GetComponent<UITestbed>().OnHamburgerSelect("splash");
    }
}
