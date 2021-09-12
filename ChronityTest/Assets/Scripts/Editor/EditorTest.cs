using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Chronity;

public class EditorTest : EditorWindow
{
    private EditorTimer timer;

    [MenuItem("Window/Chronity")]
    public static void ShowWindow()
    {
        GetWindow<EditorTest>("Chronity Test");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Chronity Test"))
        {
            timer = EditorTimer.Register(5, () => Debug.Log("Hello World"), x => Debug.Log(x));
        }
        if (GUILayout.Button("Cancel"))
        {
            timer.Cancel();
        }
        if (GUILayout.Button("Pause"))
        {
            timer.Pause();
        }
        if(GUILayout.Button("Resume"))
        {
            timer.Resume();
        }
        if(GUILayout.Button("Cancel All"))
        {
            EditorTimer.CancelAllTimers();
        }
    }
}
