using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewScreen : BaseScreen
{
    public override void OnBecomeActive()
    {
        elapsedTime = 0;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StressApp.instance.stateMachine.SetState(EnterOrViewState.label);
        }

        elapsedTime += Time.deltaTime;

        
        int val = (int)((elapsedTime - Mathf.Floor(elapsedTime)) * 100);
        transform.Find("root").Find("click_to_return").GetComponent<UnityEngine.UI.Text>().color = new Color(1, 1, 1, (val % 100 > 50) ? 1 : 0);
    }
}
