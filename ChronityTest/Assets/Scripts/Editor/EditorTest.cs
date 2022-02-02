using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Chronity;

public class EditorTest : EditorWindow
{
    private EditorTimer timer;
    private Vector2 scrollPos;

    [MenuItem("Window/Chronity")]
    public static void ShowWindow()
    {
        GetWindow<EditorTest>("Chronity Test");
    }

    private void OnGUI()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos);

        // Add Image to the window
        Texture2D image = Resources.Load<Texture2D>("Slide");
        // scale it down to width of window
        float scale = Mathf.Min(position.width / image.width, position.height / image.height);
        GUILayout.Label(image, GUILayout.Width(image.width * scale), GUILayout.Height(image.height * scale));

        if (GUILayout.Button("Start"))
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

        GUILayout.EndScrollView();
    }
}
