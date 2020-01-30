using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash_Screen : BaseScreen {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        elapsedTime += Time.deltaTime;

        //if (elapsedTime < 2.0f)
        {
            transform.Find("click_to_start").GetComponent<UnityEngine.UI.Text>().color = new Color(1, 1, 1, 0);
        }
        //else
        {
            int val = (int)((elapsedTime - Mathf.Floor(elapsedTime)) * 100);
            transform.Find("click_to_start").GetComponent<UnityEngine.UI.Text>().color = new Color(1, 1, 1, (val % 100 > 50) ? 1 : 0);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StressApp.instance.stateMachine.SetState(EnterOrViewState.label);
            }
        }       
    }

    public override void OnBecomeActive()
    {
        elapsedTime = 0;
        transform.Find("click_to_start").GetComponent<UnityEngine.UI.Text>().color = new Color(1, 1, 1, 0);
    }
}
