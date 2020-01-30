using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//=======================================================================================
public class SplashScreenState : Library.TemplateState
{
    public static String label = "SplashScreenState";

    public static SplashScreenState instance;

    public override void Init(Object obj = null)
    {
        instance = this;

        StressApp.instance.canvasController.SetScreen("Splash_Screen");        
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}

//=======================================================================================
public class EnterOrViewState : Library.TemplateState
{
    public static String label = "EnterOrViewState";

    public static EnterOrViewState instance;

    public override void Init(Object obj = null)
    {
        instance = this;

        StressApp.instance.canvasController.SetScreen("EnterOrView_Screen");
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}

//=======================================================================================
public class ViewState : Library.TemplateState
{
    public static String label = "ViewState";

    public static ViewState instance;

    public override void Init(Object obj = null)
    {
        instance = this;

        StressApp.instance.canvasController.SetScreen("View_Screen");
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}

//=======================================================================================

public class QuestionState : Library.TemplateState
{
    public static String label = "questionStates";

    public static QuestionState instance;
        
    public override void Init(Object obj = null)
    {
        instance = this;
        
        StressApp.instance.canvasController.SetScreen("TemplateQuestion_Screen");

        Questionaire.Get().OnEnterQuestionaire();

        //StressApp.instance.canvasController.GetScreen("TemplateQuestion_Screen").GetComponent<TemplateQuestion_Screen>().Init();
    }

    public override void Update()
    {        
    }

    public override void Exit()
    {
    }
}