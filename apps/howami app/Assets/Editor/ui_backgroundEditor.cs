using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ui_background))]
public class ui_backgroundEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Reload Text"))
        {
            GameObject.Find("Canvas").GetComponent<UITestbed>().LoadText();
        }
    }
}