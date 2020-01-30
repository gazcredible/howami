using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class rbController : MonoBehaviour {

    // Use this for initialization

    GameObject currentButton = null;
	void Start ()
    {	    
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

    void Update()
    {
        if (currentButton == null)
        {
            currentButton = transform.Find("mybutton").gameObject;
        }

        GameObject selectedButton = null;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Debug.LogWarning(Input.mousePosition.ToString());

            var pos = Input.mousePosition;

            pos.x -= Screen.width / 2;
            pos.y -= Screen.height / 2;

            for (var i = 0; i < transform.childCount; i++)
            {
                var ch = transform.GetChild(i);

                if (ch.name.ToLower().Contains("button") == true)
                {
                    if (IsPointInRT(pos, ch.GetComponent<RectTransform>()) == true)
                    {
                        //Debug.LogWarning(ch.name);

                        selectedButton = ch.gameObject;
                    }
                }
            }
        }

        if (selectedButton != null)
        {
            currentButton.GetComponent<ButtonHandler>().SetSelected(false);
            currentButton = selectedButton;
        }

        if (currentButton != null)
        {
            currentButton.GetComponent<ButtonHandler>().SetSelected(true);
        }
    }
}
