using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public virtual void OnBack()
    {
        Debug.LogWarning("Implement me!");
    }

    public virtual void OnPageSelected()
    {
    }

    public virtual void LoadText()
    {
        
    }
}