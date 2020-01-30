using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressApp : MonoBehaviour
{
    public Library.TemplateStateMachine<Library.TemplateState> stateMachine;

    public CanvasController canvasController;

    public static StressApp instance;

    void Start ()
    {
        instance = this;

        stateMachine = new Library.TemplateStateMachine<Library.TemplateState>();

        stateMachine.AddState(SplashScreenState.label, new SplashScreenState());
        stateMachine.AddState(EnterOrViewState.label, new EnterOrViewState());
        stateMachine.AddState(QuestionState.label, new QuestionState());
        stateMachine.AddState(ViewState.label, new ViewState());

        stateMachine.SetState(SplashScreenState.label);
        //stateMachine.SetState(ViewState.label);

        canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();

        canvasController.OnStartUp();
        Questionaire.Get().OnStartUp();
    }
	
	// Update is called once per frame
	void Update ()
    {

        debug_Screen.instance.text = Screen.width + ":" + Screen.height;
        debug_Screen.instance.text += GameObject.Find("Canvas").GetComponent<RectTransform>().lossyScale.ToString();


        stateMachine.Update();	
	}

    public void SetScreen(string name)
    {
        canvasController.SetScreen(name);
    }

    public Vector2 mousePos
    {
        get
        {
            var pos = Input.mousePosition;

            //pos.x -= Screen.width / 2;
            pos.y = Screen.height - pos.y;

            return pos;
        }
    }

    public static bool IsPointInRT(Vector2 point, RectTransform rt)
    {
        // Get the rectangular bounding box of your UI element
        Rect rect = rt.rect;

        // Get the left, right, top, and bottom boundaries of the rect
        float leftSide = rt.anchoredPosition.x - rect.width / 2;
        float rightSide = rt.anchoredPosition.x + rect.width / 2;
        float topSide = rt.anchoredPosition.y + rect.height / 2;
        float bottomSide = rt.anchoredPosition.y - rect.height / 2;

        //Debug.Log(leftSide + ", " + rightSide + ", " + topSide + ", " + bottomSide);

        // Check to see if the point is in the calculated bounds
        if (point.x >= leftSide &&
            point.x <= rightSide &&
            point.y >= bottomSide &&
            point.y <= topSide)
        {
            return true;
        }
        return false;
    }

    public static Rect RectTransformToRect(Transform xform)
    {
        var rt = xform.GetComponent<RectTransform>();

        Vector3 scale = rt.lossyScale;
        
        Rect rect = rt.rect;

        var xRect = new Rect(rt.anchoredPosition.x * scale.x
                            , rt.anchoredPosition.y * scale.y
                            , rect.width * scale.x
                            , rect.height * scale.y);

        var myRect = new Rect();

        myRect.x = (xRect.x) + (Screen.width / 2) - (xRect.width / 2);
        myRect.y = (Screen.height / 2) - ((xRect.y) + xRect.height / 2);
        myRect.width = xRect.width;
        myRect.height = xRect.height;

        return myRect;
    }

    public static String RectTransformToString(Transform xform, RectTransform rt)
    {
        Vector3 scale = xform.GetComponent<RectTransform>().lossyScale;

        Rect rect = rt.rect;

        var xRect = new Rect(rt.anchoredPosition.x *scale.x
                            ,rt.anchoredPosition.y * scale.y
                            ,rect.width * scale.x
                            ,rect.height * scale.y);

        var myRect = new Rect();

        myRect.x = (xRect.x) + (Screen.width / 2)-(xRect.width / 2) ;
        myRect.y = (Screen.height / 2) - ((xRect.y ) + xRect.height / 2);
        myRect.width = xRect.width;
        myRect.height = xRect.height;

        return myRect.x + ":" + myRect.y + " [" + myRect.width + ":" + myRect.height + "]";

    }

    public static bool isPointInMe(Transform obj, Vector2 pos)
    {
        Vector3 scale = obj.GetComponent<RectTransform>().lossyScale;


        // Get the rectangular bounding box of your UI element
        var rt = obj.GetComponent<RectTransform>();
        Rect rect = rt.rect;

        // Get the left, right, top, and bottom boundaries of the rect
        float leftSide = (rt.anchoredPosition.x - rect.width / 2)*scale.x;
        float rightSide = (rt.anchoredPosition.x + rect.width / 2)*scale.x; ;
        float topSide = (rt.anchoredPosition.y + rect.height / 2)*scale.y; ;
        float bottomSide = (rt.anchoredPosition.y - rect.height / 2)*scale.y;

        //Debug.Log(leftSide + ", " + rightSide + ", " + topSide + ", " + bottomSide);

        // Check to see if the point is in the calculated bounds
        if (pos.x >= leftSide &&
            pos.x <= rightSide &&
            pos.y >= bottomSide &&
            pos.y <= topSide)
        {
            return true;
        }
        return false;
    }

    public static Vector3 getScale(Transform obj, Vector3 scale)
    {        
        if(obj.parent == null)
        {
            return scale;
        }

        scale.x *= obj.GetComponent<RectTransform>().lossyScale.x;
        scale.y *= obj.GetComponent<RectTransform>().lossyScale.y;
        scale.z *= obj.GetComponent<RectTransform>().lossyScale.z;

        return getScale(obj.parent,scale);
    }

}
