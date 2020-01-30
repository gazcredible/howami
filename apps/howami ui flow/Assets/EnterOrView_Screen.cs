using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterOrView_Screen : BaseScreen
{
    public override void OnBecomeActive()
    {        
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var ch = transform.GetChild(i);

                if (StressApp.RectTransformToRect(ch).Contains(StressApp.instance.mousePos) == true)
                //if (StressApp.IsPointInRT(pos, ch.GetComponent<RectTransform>()) == true)
                {
                    if(ch.name.ToLower().Contains("enter_data") == true)
                    {
                        StressApp.instance.stateMachine.SetState(QuestionState.label);
                    }

                    if (ch.name.ToLower().Contains("view_data") == true)
                    {
                        StressApp.instance.stateMachine.SetState(ViewState.label);
                    }
                }
            }
        }
    }
}
