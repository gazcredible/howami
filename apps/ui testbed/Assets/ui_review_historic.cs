using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_review_historic : MonoBehaviour
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
        transform.Find("historic-detail").gameObject.SetActive(true);        
        transform.Find("historic-overview").gameObject.SetActive(false);
    }

    public void OnGotoHistoricSummary()
    {
        transform.Find("historic-detail").gameObject.SetActive(false);
        transform.Find("historic-overview").gameObject.SetActive(true);
    }

    public void OnMainMenu()
    {
        GameObject.Find("Canvas").GetComponent<UITestbed>().OnHamburgerSelect("splash");
    }
}
